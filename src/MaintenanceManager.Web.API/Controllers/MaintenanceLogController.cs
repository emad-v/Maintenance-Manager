using MaintenanceManager.Business;
using MaintenanceManager.DomainModel.Models.MaintenanceLog;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.Web.API.Controllers
{
    [ApiController]
    [Route("/api/maintenancelog")]
    public class MaintenanceLogController : ControllerBase
    {
        private readonly MaintenanceLogService maintenanceLogService;

        public MaintenanceLogController(MaintenanceLogService maintenanceLogService)
        {
            this.maintenanceLogService = maintenanceLogService;
        }

        [HttpPost("{statusRef}/complete")]
        public async Task<IActionResult> CompleteMaintenanceTask(string statusRef, [FromBody] CompleteMaintenanceDto completeTaskRequest)
        {
            try
            {
                var result = await maintenanceLogService.CompleteMaintenance(statusRef,completeTaskRequest);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("statuses/{statusRef}")]
        public async Task<IActionResult> GetMaintenanceByStatus(string statusRef)
        {
            var result = await maintenanceLogService.GetMaintenanceByStatus(statusRef);
            return Ok(result);
        }
    }
}