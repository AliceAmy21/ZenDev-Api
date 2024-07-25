using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Text.Json;
using ZenDev.Api.ApiModels.Strava;
using ZenDev.BusinessLogic.Services.Interfaces;
using ZenDev.Common.Models;

namespace ZenDev.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class StravaController: ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;
        private readonly ILogger<StravaController> _logger;
        private readonly IPointsService _pointsService;

        public StravaController(
            IHttpClientFactory httpClientFactory,
            IMapper mapper,
            IPointsService pointsService,
            ILogger<StravaController> logger
            )
        {
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
            _pointsService = pointsService;
            _logger = logger;
        }

        [HttpPost(nameof(SyncStravaData))]
        public async Task <ActionResult<DateTimeOffset>> SyncStravaData([FromHeader] string accessToken, long userId)
        {
            var httpClient = _httpClientFactory.CreateClient("strava");

            // The Strava API requires an Authorization header (user access token)
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var lastSyncedDate = await _pointsService.GetLastSyncedDateAsync(userId);
            HttpResponseMessage httpResponseMessage;

            if (lastSyncedDate.HasValue)
            {
                long epochTime = lastSyncedDate.Value.ToUnixTimeSeconds();
                httpResponseMessage = await httpClient.GetAsync($"athlete/activities?after={epochTime}&page=1&per_page=200");
            }
            else
            {
                var firstOfJune = new DateTimeOffset(2024, 6, 1, 0, 0, 0, TimeSpan.Zero);
                long epochTime = firstOfJune.ToUnixTimeSeconds();
                httpResponseMessage = await httpClient.GetAsync($"athlete/activities?after={epochTime}&page=1&per_page=200");
            }

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var stream = await httpResponseMessage.Content.ReadAsStringAsync();
                _logger.LogInformation("Raw JSON Response: {JsonResponse}", stream);

                var activities = JsonSerializer.Deserialize<List<ActivitySummaryResponse>>(stream);
                var activitiesApiModel = _mapper.Map<List<ActivitySummaryApiModel>>(activities);

                var pointsModels = _mapper.Map<List<ActivityPointsApiModel>>(activitiesApiModel);
                await _pointsService.UpdateTotalPoints(userId,pointsModels);

                await _pointsService.UpdatePointsGroups(userId,pointsModels);
                await _pointsService.UpdateAmountCompleteChallenges(userId,pointsModels);
                await _pointsService.UpdateGoalCompletion(userId,pointsModels);
                await _pointsService.UpdateTournamentPoints(userId,pointsModels);
                await _pointsService.UpdateActivitiesForUser(userId,pointsModels);
                
                // Only update the last synced date after the calculations have completed successfully
                var newSyncDate = await _pointsService.SetLastSyncedDateAsync(userId);
                return Ok(newSyncDate);

            }
            return BadRequest();
        }
    }
}
