using MaintenanceManager.Business;
using MaintenanceManager.DomainModel.Models.Machine;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MaintenanceManager.Web.Service
{
    [ApiController]
    [Route("api/machines")]
    public class MachineController : ControllerBase
    {
        private readonly MachineService _machineService;

        public MachineController(MachineService machineService)
        {
            _machineService = machineService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllMachinesByCustomerReference([FromQuery] string customerReference)
        {
            var machines = await _machineService.GetMachinesByCustomerReference(customerReference);
            return Ok(machines);
        }

        [HttpGet("reference")]
        public async Task<ActionResult> GetMachineByReference(string machineReference)
        {
            try
            {
                var machine = await _machineService.GetMachineByReference(machineReference);
                return Ok(machine);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }
        [HttpPost]
        public async Task<ActionResult> CreateMachine(AddMachineDto addMachineRequest)
        {
            try
            {
                var machine = await _machineService.CreateMachine(addMachineRequest);
                return CreatedAtAction(nameof(GetMachineByReference), new { machineId = machine.Reference }, machine);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }
    }
}
