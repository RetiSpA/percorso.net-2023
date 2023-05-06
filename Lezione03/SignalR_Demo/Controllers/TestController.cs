using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalR_Demo.Models;

namespace SignalR_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class TestController : ControllerBase
    {
        public TestController(IHubContext<ChatHub> hub)
        {
            Hub = hub;
        }

        public IHubContext<ChatHub> Hub { get; }

        [HttpPost("sendToGroup/{group}")]
        public async Task<IActionResult> WriteToGroup([FromRoute] string group, [FromBody] string text)
        {
            await Hub.Clients.Group(group).SendAsync("WritedFromGrouo", text);
            return Ok();
        }
    }
}
