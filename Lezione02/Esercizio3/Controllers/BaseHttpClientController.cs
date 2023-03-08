using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Esercizio3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseHttpClientController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;

        public BaseHttpClientController(
            IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet("getBase")]
        public async Task<string> Get()
        {
            var req = new HttpRequestMessage(
                HttpMethod.Get, 
                "https://jsonplaceholder.typicode.com/todos/1");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(req);

            string json = await response.Content.ReadAsStringAsync();

            return json;
        }
    }
}