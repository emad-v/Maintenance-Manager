using MaintenanceManager.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();//skip
        Task<User> GetUserByReference(string reference);
        Task<User> AddUser(User user);
        Task<User> UpdateUser(string reference, string name, string email);//skip
        Task DeactivateUser(string reference);//skip
        Task<bool> EmailExistsAsync(string email);
        Task<bool> ReferenceExistAsync(string reference);
    }
}
