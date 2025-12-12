using MaintenanceManager.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.Interfaces.Repositories
{
    public interface IComponentRepository
    {
        Task<Component> AddComponent(Component component);
        Task<Component> GetComponentByReference(string reference);
        Task<bool> ReferenceExistAsync(string reference);


    }
}
