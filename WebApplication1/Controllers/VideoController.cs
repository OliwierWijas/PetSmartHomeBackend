using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Domain;

namespace WebApplication1.Controllers;

[ApiController]
[Route("/video")]
public class VideoController : ControllerBase
{
    private readonly IHubContext<VideoHub> _hubContext;

    public VideoController(IHubContext<VideoHub> hubContext)
    {
        _hubContext = hubContext;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadVideo([FromBody] string base64Image)
    {
        if (string.IsNullOrEmpty(base64Image))
        {
            return BadRequest("No video data received");
        }
        
        // Ensure base64 does not contain redundant "data:image/*;base64,"
        string cleanBase64 = base64Image.Contains(",") ? base64Image.Split(',')[1] : base64Image;

        // Send image to React frontend
        await _hubContext.Clients.All.SendAsync("ReceiveFrame", "data:image/jpeg;base64," + cleanBase64);
        Console.WriteLine($"Received frame: {cleanBase64.Substring(0, 50)}..."); // Log first 50 chars for debugging

        return Ok(new { message = "Frame received" });
    }
}