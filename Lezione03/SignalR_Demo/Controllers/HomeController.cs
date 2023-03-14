using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalR_Demo.Models;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;

namespace SignalR_Demo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHubContext<ChatHub> _hubContext;

        public HomeController(ILogger<HomeController> logger, IHubContext<ChatHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }

        //[HttpGet]
        //public async Task Index()
        //{
        //    await _hubContext.Clients.All.SendAsync("ReceiveMessage", "System", $"Sono le ore {DateTime.Now}");
        //}

        [HttpPost]
        public async Task BroadcastMessage([FromBody] UserMessage userMessage)
        {
            var claims = HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti);
            var testvar = _hubContext.Clients.User(claims.Value);
            //await _hubContext.Clients.GroupExcept("test", testvar.ToString()).SendAsync("ReceiveMessage", userMessage.Name + " " + userMessage.Surname, userMessage.Message);
            //await _hubContext.Clients.AllExcept(testvar.ToString()).SendAsync("ReceiveMessage", userMessage.Name + " " + userMessage.Surname, userMessage.Message);
            await _hubContext.Clients.Groups("Admin").SendAsync("ReceiveMessage", userMessage.Name + " " + userMessage.Surname, userMessage.Message);

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            //}
        }
    }
}
