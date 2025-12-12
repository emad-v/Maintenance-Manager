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
    public class UserRepository : IUserRepository
    {
        private readonly MaintenanceManagerDbContext _context;
        public UserRepository(MaintenanceManagerDbContext context)
        {
            _context = context;
        }

        public async Task<User> AddUser(User user)
        {
            user.CreatedDate = DateTime.UtcNow;
            UserDb userDb = MapToUserrDb(user);
            _context.Add(userDb);
            await _context.SaveChangesAsync();
            return MapToUser(userDb);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var usersDb = await _context.Users.Where(u => u.IsActive == true).ToListAsync();
            return usersDb.Select(userDb => MapToUser(userDb));
        }

        public async Task DeactivateUser(string reference)
        {
            var userDb = await _context.Users
                 .SingleOrDefaultAsync(c => c.Reference == reference && c.IsActive);
            if (userDb == null)
            {
                throw new KeyNotFoundException($"User with reference: {userDb} was not found");
            }
            userDb.IsActive = false;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email && u.IsActive);
        }

        public async Task<User> GetUserByReference(string reference)
        {
            var userDb = await _context.Users.SingleOrDefaultAsync(u => u.Reference == reference && u.IsActive);
            if (userDb == null)
            {
                throw new KeyNotFoundException($"User with reference: {reference} was not found");
            }
            return MapToUser(userDb);

        }



        public Task<bool> ReferenceExistAsync(string reference)
        {
            return _context.Users.AnyAsync(u => u.Reference.Equals(reference) && u.IsActive);
        }


        public async Task<User> UpdateUser(string reference, string name, string email)
        {
            var userDb = await _context.Users.SingleOrDefaultAsync(u => u.Reference == reference && u.IsActive);
            if (userDb == null)
            {
                throw new KeyNotFoundException($"User with reference: {reference} was not found");
            }
            userDb.Name = name;
            userDb.Email = email;
            await _context.SaveChangesAsync();
            return MapToUser(userDb);
        }



        private static UserDb MapToUserrDb(User user)
        {
            return new UserDb
            {
                Reference = user.Reference,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                IsActive = user.IsActive,
                CreatedDate = user.CreatedDate,
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
