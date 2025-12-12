using MaintenanceManager.Business;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.Web.API.Controllers
{
    [ApiController]
    [Route("api/UsageCounters")]
    public class CounterController : ControllerBase
    {
        private readonly UsageCounterService _service;

        public CounterController(UsageCounterService service)
        {
            _service = service;
        }

        [HttpPost("{counterReference}/increment")]
        public async Task<IActionResult> IncrementCounter(string counterReference, [FromBody] int incrementValue)
        {
            try
            {
                var result = await _service.IncrementCounter(counterReference, incrementValue);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);// returns 409
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);// returns 400 - validation errors 
            }
        }


    }


}
