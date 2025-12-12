using MaintenanceManager.Business;
using MaintenanceManager.DomainModel.Models.MaintenanceRule;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.Web.API.Controllers
{

    [ApiController]
    [Route("api/MaintenanceRules")]
    public class MaintenanceRuleController : ControllerBase
    {
        private readonly MaintenanceRuleService _service;

        public MaintenanceRuleController(MaintenanceRuleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetMaintenanceRules()
        {
            var maintenanceRules = await _service.GetMaintenanceRules();
            return Ok(maintenanceRules);
        }

        [HttpGet("{reference}")]
        public async Task<ActionResult> GetMaintenanceRuleByReference(string reference)
        {
            try
            {
                var maintenanceRule = await _service.GetMaintenanceRuleByReference(reference);
                return Ok(maintenanceRule);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpPost]
        public async Task<ActionResult> CreateMaintenanceRule(AddToMaintenanceRuleDto addMaintenanceRuleRequest)
        {
            try
            {
                var result = await _service.CreateMaintenanceRule(addMaintenanceRuleRequest);

                return CreatedAtAction(
                    nameof(GetMaintenanceRuleByReference),
                    new { Reference = result.Reference }, result);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);// returns 409
            }


        }
    }
}
