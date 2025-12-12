using MaintenanceManager.Data.Models;
using MaintenanceManager.DomainModel.Entities;
using MaintenanceManager.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.Data.Repositories
{
    public class MachineRepository : IMachineRepository
    {


        private readonly MaintenanceManagerDbContext _context;

        public MachineRepository(MaintenanceManagerDbContext context)
        {
            _context = context;
        }

        public async Task<Machine> AddMachine(Machine machine)
        {
            machine.CreatedDate = DateTime.UtcNow;
            MachineDb machineDb = MapToMachineDb(machine);

            _context.Machines.Add(machineDb);  //is a synchronous change tracker operation (no I/O).
            await _context.SaveChangesAsync();//call that does database I/O, 
            return MapToMachine(machineDb);
        }

        public async Task<Machine> GetMachineByReference(string reference)
        {
            MachineDb? machineDb = await _context.Machines.SingleOrDefaultAsync(m => m.Reference == reference);
            if (machineDb == null)
            {
                throw new KeyNotFoundException($"Machine with reference {reference} was not found");
            }
            return MapToMachine(machineDb);
        }

        public async Task<IEnumerable<Machine>> GetMachinesByCustomerReference(string customerReference)
        {
            var machinesDb = await _context.Machines
                 .Where(m => m.CustomerReference == customerReference)
                 .ToListAsync();
            return machinesDb.Select(machinesDb => MapToMachine(machinesDb));
        }

        public async Task<bool> ReferenceExistAsync(string reference)
        {
            return await _context.MaintenanceRules.AnyAsync(m => m.Reference == reference);
        }


        private static MachineDb MapToMachineDb(Machine machine)
        {
            return new MachineDb
            {
                Reference = machine.Reference,
                Name = machine.Name,
                Model = machine.Model,
                Type = machine.Type,
                CustomerReference = machine.CustomerReference,
                CreatedDate = machine.CreatedDate
            };
        }

        private static Machine MapToMachine(MachineDb machineDb)
        {
            return new Machine
            {
                Reference = machineDb.Reference,
                Name = machineDb.Name,
                Model = machineDb.Model,
                Type = machineDb.Type,
                CustomerReference = machineDb.CustomerReference,
                CreatedDate = machineDb.CreatedDate
            };
        }

       
    }
}
