using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Text.Json;
using ZenDev.Api.ApiModels.Strava;

namespace ZenDev.Api.Controllers
{
    public class StravaController
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<StravaController> _logger;

        public StravaController(IHttpClientFactory httpClientFactory, ILogger<StravaController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        [HttpPost(nameof(SyncStravaData))]
        public async Task <ActionResult<ActivitySummaryApiModel>> SyncStravaData([FromBody] string accessToken)
        {
            var httpClient = _httpClientFactory.CreateClient("strava");

            // The Strava API requires an Authorization header (user access token)
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var httpResponseMessage = await httpClient.GetAsync("athlete/activities");
            _logger.LogInformation("Activities: {Activities}", httpResponseMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var stream = await httpResponseMessage.Content.ReadAsStringAsync();
                _logger.LogInformation("Raw JSON Response: {JsonResponse}", stream);

                var activities = JsonSerializer.Deserialize<List<ActivitySummaryResponse>>(stream);
                _logger.LogInformation("Activities: {Activities}", activities);
            }


            return null;
        }
    }
}
