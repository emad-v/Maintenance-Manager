using MaintenanceManager.DomainModel.Entities;
using MaintenanceManager.DomainModel.Models.User;
using MaintenanceManager.Interfaces.Repositories;
using MaintenanceManager.DomainModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaintenanceManager.DomainModel.Helpers;

namespace MaintenanceManager.Business
{
    public class UserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository)
        {
            this._repository = repository;
        }

        public async Task<UserResponseDto> CreateUser(AddUserDto addUserRequest)
        {

            if (await _repository.EmailExistsAsync(addUserRequest.Email))
            {
                throw new InvalidOperationException($"User with email {addUserRequest.Email} already exist");
            }

            User user = new User()
            {
                Reference = ReferenceGenerator.GenerateUserReference(),
                Name = addUserRequest.Name,
                Email = addUserRequest.Email,
                Role = UserRole.Technician
            };
            var createdUser = await _repository.AddUser(user);

            return new UserResponseDto
            {
                Reference = createdUser.Reference,
                Name = createdUser.Name,
                Email = createdUser.Email,
                Role = createdUser.Role,
                IsActive = createdUser.IsActive,
                CreatedDate = createdUser.CreatedDate,
            };
        }
    }

}
