using Esercizio3.Services;
using Microsoft.AspNetCore.Mvc;

namespace Esercizio3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TypedHttpClientController : ControllerBase
    {
        private readonly ExternalService _externalSvc;

        public TypedHttpClientController(
            ExternalService externalSvc)
        {
            _externalSvc = externalSvc;
        }

        [HttpGet("getTyped/{id}")]
        public async Task<string> Get(int id)
        {
            return await _externalSvc.GetTodosById(id);
        }
    }
}