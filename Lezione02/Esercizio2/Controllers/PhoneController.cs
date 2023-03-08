using Esercizio2.Models;
using Esercizio2.Services;
using Esercizio2.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Esercizio2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneController : ControllerBase
    {
        private readonly PhoneService _phoneService;

        public PhoneController(
            PhoneService phoneService)
        {
            _phoneService = phoneService;
        }

        [HttpGet("getValue")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("getPhone/{id}")]
        public Phone GetPhone(int id)
        {
            Phone phone = StoreWarehouse.Phones.Find(x => x.Id == id);
            return phone;
        }

        [HttpGet("getAllPhones")]
        public List<Phone> GetAllPhones()
        {
            return StoreWarehouse.Phones;
        }

        [HttpGet("getPhone/service/{id}")]
        public Phone GetPhoneWithService(int id, [FromServices] PhoneService service)
        {
            return service.GetPhoneById(id);
        }

        [HttpGet("deletePhone")]
        public void Delete(int id)
        {
            Phone deletePhone = StoreWarehouse.Phones.Find(x => x.Id == id);
            StoreWarehouse.Phones.Remove(deletePhone);
        }

        [HttpGet("deletePhone/service")]
        public void DeleteWithService(int id)
        {
            _phoneService.DeletePhoneById(id);
        }

        [HttpPost("addPhone")]
        public ActionResult AddPhone([FromBody] Phone p)
        {
            try
            {
                StoreWarehouse.Phones.Add(p);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet("getPhoneByPrice")]
        public ActionResult GetPhoneByPrice([FromQuery] int price)
        {
            try
            {
                return Ok(StoreWarehouse.Phones.Select(x => x.Price < price).ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }


        [HttpPut("updateOrInsertPhone")]
        public ActionResult UpdateOrInsertPhone([FromQuery] bool b, [FromBody] Phone p)
        {
            try
            {
                Phone ph = StoreWarehouse.Phones.FirstOrDefault(x => x.Name == p.Name);

                if (ph != null && b == true)
                {
                    StoreWarehouse.Phones.Remove(ph);
                    StoreWarehouse.Phones.Add(p);

                    return Ok();
                }
                else if (ph == null)
                {
                    StoreWarehouse.Phones.Add(p);

                    return Ok();
                }

                return StatusCode(500, "Phone not found!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}
