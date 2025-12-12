using MaintenanceManager.Business;
using MaintenanceManager.DomainModel.Models.ComponentRuleStatus;
using MaintenanceManager.DomainModel.Models.UsageCounter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.Web.API.Controllers
{

    [ApiController]
    [Route("api/components")]
    public class ComponentController : ControllerBase
    {
        private readonly ComponentRuleStatusService _componentRuleStatusService;

        public ComponentController(ComponentRuleStatusService componentRuleStatusService)
        {
            _componentRuleStatusService = componentRuleStatusService;
        }

        [HttpPost("{componentReference}/rules")]
        public async Task<IActionResult> AssignRuleToComponent(string componentReference, [FromBody] AssignRuleDto assignRuleRequest)
        {
            try
            {
                var result = await _componentRuleStatusService.AssignRule(componentReference, assignRuleRequest.MaintenanceRuleReference);
                return CreatedAtAction(nameof(GetSingleStatus),
                    new { componentREference = result.ComponentReference, statusReference = result.Reference }, result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);//404
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);//409
            }
            catch (InvalidDataException ex)
            {
                return Conflict(ex.Message);//409
            }
        }


        [HttpGet("{componentReference}/rules")]
        public async Task<IActionResult> GetComponentStatuses(string componentReference)
        {
            var result = await _componentRuleStatusService.ListComponentStatuses(componentReference);
            return Ok(result);
        }


        [HttpGet("{componentReference}/rules/{statusReference}")]
        public async Task<IActionResult> GetSingleStatus(string componentReference, string statusReference)
        {
            try
            {
                var result = await _componentRuleStatusService.GetStatusByReference(componentReference, statusReference);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, ex.Message);//Internal server error 
            }

        }




    }
}
