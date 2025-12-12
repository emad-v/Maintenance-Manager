using MaintenanceManager.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.Interfaces.Repositories
{
    public interface IMachineRepository
    {
        Task<Machine> AddMachine(Machine machine);
        Task<IEnumerable<Machine>> GetMachinesByCustomerReference(string customerReference);
        Task<Machine> GetMachineByReference(string reference);
        Task<bool> ReferenceExistAsync(string reference);
    }
}
