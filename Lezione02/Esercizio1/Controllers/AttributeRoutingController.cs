using Esercizio1.Models;
using Esercizio1.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Esercizio1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttributeRoutingController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> GetValues()
        {
            return new List<string>()
            {
                "prova 1",
                "prova 2"
            };
        }

        [HttpGet]
        [Route("list")]
        public IEnumerable<Phone> GetPhoneAllPhones()
        {
            return StoreWarehouse.Phones;
        }
    }
}
