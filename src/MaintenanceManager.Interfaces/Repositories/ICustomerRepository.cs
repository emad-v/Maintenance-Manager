using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaintenanceManager.DomainModel.Entities;



namespace MaintenanceManager.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetCustomers();
        Task<Customer> GetCustomerByReference(string reference);
        Task<Customer> AddCustomer(Customer customer);
        Task<Customer> UpdateCustomer(string reference, string name, string email);
        Task DeactivateCustomer(string reference);
        Task<bool> EmailExistsAsync(string email);
        Task<bool> ReferenceExistAsync(string reference);
    }
}
