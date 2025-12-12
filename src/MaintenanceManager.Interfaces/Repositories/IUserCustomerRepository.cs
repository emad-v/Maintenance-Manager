using MaintenanceManager.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.Interfaces.Repositories
{
    public interface IUserCustomerRepository
    {
        Task<UserCustomer>LinkUserToCustomerAsync(string userRef, string customerRef);
        Task<bool> IsLinkedAsync(string userRef, string customerRef);
        Task<IEnumerable<User>> GetUsersByCustomerAsync(string customerRef);
        
    }
}
