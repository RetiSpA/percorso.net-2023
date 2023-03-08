using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Esercizio3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NamedHttpClientController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;

        public NamedHttpClientController(
            IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet("getNamed")]
        public async Task<string> Get()
        {
            var req = new HttpRequestMessage(HttpMethod.Get, "todos/1");

            var client = _clientFactory.CreateClient("namedHttpClient");

            var response = await client.SendAsync(req);

            string json = await response.Content.ReadAsStringAsync();

            return json;
        }
    }
}