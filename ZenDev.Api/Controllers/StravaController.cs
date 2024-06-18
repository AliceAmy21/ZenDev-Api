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
        public async Task <ActionResult<List<ActivitySummaryApiModel>>> SyncStravaData([FromHeader] string accessToken, [FromBody]StravaUserSyncRequest syncData)
        {
            var httpClient = _httpClientFactory.CreateClient("strava");

            // The Strava API requires an Authorization header (user access token)
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var lastSyncedDate = await _pointsService.SetLastSyncedDateAsync(syncData.UserId);
            HttpResponseMessage httpResponseMessage;

            if (lastSyncedDate.HasValue)
            {
                long epochTime = lastSyncedDate.Value.ToUnixTimeSeconds();
                httpResponseMessage = await httpClient.GetAsync($"athlete/activities?after={epochTime}");
            }
            else
            {
                 httpResponseMessage = await httpClient.GetAsync("athlete/activities");
            }

            //var httpResponseMessage = await httpClient.GetAsync("athlete/activities?before=1718103013&after=1718102013&page=1&per_page=30");


            _logger.LogInformation("Activities: {Activities}", httpResponseMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var stream = await httpResponseMessage.Content.ReadAsStringAsync();
                _logger.LogInformation("Raw JSON Response: {JsonResponse}", stream);

                var activities = JsonSerializer.Deserialize<List<ActivitySummaryResponse>>(stream);
                _logger.LogInformation("Activities: {Activities}", activities);

                var activitiesApiModel = _mapper.Map<List<ActivitySummaryApiModel>>(activities);
                _logger.LogInformation("Mapped Activities: {MappedActivities}", activitiesApiModel);

                var pointsModels = _mapper.Map<List<ActivityPointsApiModel>>(activitiesApiModel);

                int points = _pointsService.CalculatePoints(pointsModels);
                _logger.LogInformation("Points: {MappedPoints}", points);

                return Ok(activitiesApiModel);

            }

            return BadRequest();
        }
    }
}
