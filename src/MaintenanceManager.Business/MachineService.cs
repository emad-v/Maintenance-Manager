using MaintenanceManager.DomainModel.Entities;
using MaintenanceManager.DomainModel.Enums;
using MaintenanceManager.DomainModel.Helpers;
using MaintenanceManager.DomainModel.Models.Machine;
using MaintenanceManager.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.Business
{
    public class MachineService
    {

        private readonly IMachineRepository _machineRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IComponentRepository  _componentRepository;
        private readonly IUsageCounterRepository _usageCounterRepository;

        public MachineService(IMachineRepository machineRepository, ICustomerRepository customerRepository, IComponentRepository componentRepository, IUsageCounterRepository usageCounterRepository)
        {
            _machineRepository = machineRepository;
            _customerRepository = customerRepository;
            _componentRepository = componentRepository;
            _usageCounterRepository = usageCounterRepository;
        }

        public async Task<MachineResponseDto> CreateMachine(AddMachineDto addMachineRequest)
        {
            await _customerRepository.GetCustomerByReference(addMachineRequest.CustomerReference);//an exception will be thrown in case a customer doest exist 

            if (await _machineRepository.ReferenceExistAsync(addMachineRequest.Reference))
            {
                throw new InvalidOperationException($"Machine with reference {addMachineRequest.Reference} already exist");//409 error
            }

            Machine machine = new Machine()
            {
                Reference = addMachineRequest.Reference,
                Name = addMachineRequest.Name,
                Model = addMachineRequest.Model,
                Type = addMachineRequest.Type,
                CustomerReference = addMachineRequest.CustomerReference
            };

            Machine createdMachine = await _machineRepository.AddMachine(machine);

            Component component = new Component()
            {
                Reference = ReferenceGenerator.GenerateComponentReference(),
                MachineReference = createdMachine.Reference,
                Name = createdMachine.Name,
                Type = ComponentType.MACHINE_ASSEMBLY,
            };
               await _componentRepository.AddComponent(component);

            UsageCounter counter = new UsageCounter()
            {
                Reference = ReferenceGenerator.GenerateCounterReference(),
                ComponentReference = component.Reference,
                CounterType = CounterType.WORKING_HOURS,
                Value = 0,
                UpdatedAt = DateTime.UtcNow,
            };
            await _usageCounterRepository.AddCounter(counter);

            return new MachineResponseDto()
            {
                Reference = createdMachine.Reference,
                Name = createdMachine.Name,
                Model = createdMachine.Model,
                Type = createdMachine.Type,
                CustomerReference = createdMachine.CustomerReference,
                CreatedDate = createdMachine.CreatedDate,
                
            };

        }


        public async Task<MachineResponseDto> GetMachineByReference(string reference)
        {
            Machine machine = await _machineRepository.GetMachineByReference(reference);

            return new MachineResponseDto()
            {
                Reference = machine.Reference,
                Name = machine.Name,
                Model = machine.Model,
                Type = machine.Type,
                CustomerReference = machine.CustomerReference
            };


        }

        public async Task<IEnumerable<MachineResponseDto>> GetMachinesByCustomerReference(string reference)
        {

            var machines = await _machineRepository.GetMachinesByCustomerReference(reference);

            var result = machines.Select(machine => new MachineResponseDto
            {
                Reference = machine.Reference,
                Name = machine.Name,
                Model = machine.Model,
                Type = machine.Type,
                CustomerReference = machine.CustomerReference

            });
            return result;

        }


    }
}


