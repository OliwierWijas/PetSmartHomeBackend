using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("/feed")]
    public class FeedController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FeedController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task<IActionResult> FeedAsync()
        {
            var client = _httpClientFactory.CreateClient();

            var iotDeviceUrl = "http://localhost:5000/feed";
            
            try
            {
                var response = await client.PostAsync(iotDeviceUrl, null);

                if (response.IsSuccessStatusCode)
                {
                    return Ok("Feed command sent successfully.");
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Failed to execute the feed command.");
                }
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Error communicating with the IoT device: {ex.Message}");
            }
        }
    }
}
