using Esercizio5.DAL.Interfaces;
using Esercizio5.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Esercizio5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhoneController : ControllerBase
    {
        private readonly IPhoneService _phoneService;

        public PhoneController(IPhoneService phoneService)
        {
            _phoneService = phoneService;
        }

        [Authorize(Policy = "CheckAge21")]
        [HttpGet("PhoneById/{id}")]
        public ActionResult<Phone> GetPhone([FromRoute] int id)
        {
            return _phoneService.GetPhoneById(id);
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public ActionResult DeletePhone(int id)
        {
            Phone deletePhone = _phoneService.GetPhoneById(id);
            _phoneService.DeletePhone(deletePhone);

            return Ok();
        }

        [Authorize]
        [HttpPost("add")]
        public ActionResult AddPhone([FromBody] Phone p)
        {
            try
            {
                _phoneService.AddPhone(p);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpGet("getByPrice")]
        public ActionResult<IEnumerable<Phone>> GetPhoneByPrice([FromQuery] int price)
        {
            try
            {
                return Ok(_phoneService.GetPhones(x => x.Price < price));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Policy = "PhoneController")]
        [HttpPut("upsert")]
        public ActionResult UpdateOrInsertPhone([FromQuery] bool b, [FromBody] Phone p)
        {
            try
            {
                var ph = _phoneService.GetPhones(x => x.Name == p.Name).FirstOrDefault();

                if (ph is not null && b)
                {
                    _phoneService.DeletePhone(ph);
                    _phoneService.AddPhone(p);

                    return Ok();
                }
                else if (ph is null)
                {
                    _phoneService.AddPhone(p);

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