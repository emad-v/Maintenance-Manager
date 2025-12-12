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
    public class UserCustomerRepository : IUserCustomerRepository
    {
        private readonly MaintenanceManagerDbContext _context;

        public UserCustomerRepository(MaintenanceManagerDbContext context)
        {
            _context = context;
        }

        public async Task<UserCustomer> LinkUserToCustomerAsync(string userRef, string customerRef)
        {
            UserCustomerDb userCustomerDb = new UserCustomerDb
            {
                
                UserReference = userRef,
                CustomerReference = customerRef,
                AssignedAt = DateTime.UtcNow
            };

            _context.UserCustomers.Add(userCustomerDb);
            await _context.SaveChangesAsync();
            return MapToUserCustomer(userCustomerDb);
        }
        
        public async Task<IEnumerable<User>> GetUsersByCustomerAsync(string customerRef)
        {
            var usersCustomerDb = await _context.UserCustomers
                .Where(u => u.CustomerReference == customerRef).ToListAsync();// get the assignemnts that matches the customer ref
            var usersReferences = usersCustomerDb.Select(u => u.UserReference).ToList();// extract the references from those assignemnts
            var usersDb = await _context.Users
                .Where(u => usersReferences.Contains(u.Reference))
                .ToListAsync(); // extract the users objects out of them 
            return usersDb.Select(u => MapToUser(u));// converts database entities to domain models  
        }

        public async Task<bool> IsLinkedAsync(string userRef,string customerRef)
        {
            return await _context.UserCustomers.AnyAsync(l => l.UserReference == userRef && l.CustomerReference == customerRef);
        }
      
        private static UserCustomer MapToUserCustomer(UserCustomerDb userCustomerDb)
        {
            return new UserCustomer
            {
                Reference = userCustomerDb.Reference,
                UserReference = userCustomerDb.UserReference,
                CustomerReference = userCustomerDb.CustomerReference,
                AssignedAt = userCustomerDb.AssignedAt
            };

        }

        private static UserCustomerDb MapToUserCustomerDb(UserCustomer userCustomer)
        {
            return new UserCustomerDb
            {
                Reference = userCustomer.Reference,
                UserReference = userCustomer.UserReference,
                CustomerReference = userCustomer.CustomerReference,
                AssignedAt = userCustomer.AssignedAt
            };
        }
       

        private static User MapToUser(UserDb userDb)
        {
            return new User
            {
                Reference = userDb.Reference,
                Name = userDb.Name,
                Email = userDb.Email,
                Role = userDb.Role,
                IsActive = userDb.IsActive,
                CreatedDate = userDb.CreatedDate,

            };
        }


    }
}
