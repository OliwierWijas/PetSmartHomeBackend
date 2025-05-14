using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApplication1.Domain;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("/notify")]
    public class NotificationController : ControllerBase
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationController(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string message)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", message);
            return Ok(new { status = "Message sent" });
        }
    }
}
