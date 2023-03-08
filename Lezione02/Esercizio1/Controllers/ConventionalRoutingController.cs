using Esercizio1.Models;
using Esercizio1.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Esercizio1.Controllers
{
    public class ConventionalRoutingController : Controller
    {
        public IEnumerable<string> GetValues()
        {
            return new List<string>()
            {
                "prova 1",
                "prova 2"
            };
        }

        public IEnumerable<Phone> GetPhoneAllPhones()
        {
            return StoreWarehouse.Phones;
        }

        public IEnumerable<string> Test()
        {
            return new List<string>()
            {
                "prova 1",
                "prova 2"
            };
        }
    }
}
