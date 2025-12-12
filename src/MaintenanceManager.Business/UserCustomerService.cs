using MaintenanceManager.DomainModel.Enums;
using MaintenanceManager.DomainModel.Helpers;
using MaintenanceManager.DomainModel.Models.User;
using MaintenanceManager.DomainModel.Models.UserCustomerLink;
using MaintenanceManager.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.Business
{
    public class UserCustomerService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserCustomerRepository _userCustomerRepository;

        public UserCustomerService(IUserRepository userRepository, ICustomerRepository customerRepository, IUserCustomerRepository userCustomerRepository)
        {
            _userRepository = userRepository;
            _customerRepository = customerRepository;
            _userCustomerRepository = userCustomerRepository;
        }

        public async Task<LinkUserToCustomerResponseDto> LinkUserToCustomer(LinkUserDto linkUserRequest,string customerRef)
        {
            var admin = await _userRepository.GetUserByReference(linkUserRequest.AdminRef);
            if (admin.Role != UserRole.Admin)
            {
                throw new UnauthorizedAccessException("Only admins can assign users");//403
            }
            if (!(await _customerRepository.ReferenceExistAsync(customerRef)))
            {
                throw new KeyNotFoundException($"Customer with reference: {customerRef} was not found");//404
            }
            if (!(await _userRepository.ReferenceExistAsync(linkUserRequest.UserRef)))
            {
                throw new KeyNotFoundException($"user with reference: {linkUserRequest.UserRef} was not found");
            }
            if ((await _userCustomerRepository.IsLinkedAsync(linkUserRequest.UserRef, customerRef)))
            {
                throw new InvalidOperationException("The User is already assigned to this Customer");
            }

            var assignment = await _userCustomerRepository.LinkUserToCustomerAsync(linkUserRequest.UserRef, customerRef);

            assignment.Reference = ReferenceGenerator.GenerateUserCustomerReference();
            return new LinkUserToCustomerResponseDto
            {
                Reference = assignment.Reference,
                UserRef = linkUserRequest.UserRef,
                CustomerRef = customerRef,
                AssignedAt = assignment.AssignedAt
            };
        }
        public async Task<IEnumerable<UserResponseDto>> GetUsersByCustomerAsync(string customerRef)
        {
            if (!(await _customerRepository.ReferenceExistAsync(customerRef)))
            {
                throw new KeyNotFoundException($"Customer with reference: {customerRef} was not found");//404
            }
            var users = await _userCustomerRepository.GetUsersByCustomerAsync(customerRef);
            return users.Select(u => new UserResponseDto
            {
                Reference = u.Reference,
                Name = u.Name,
                Email = u.Email,
                Role = u.Role,
                IsActive = u.IsActive,
                CreatedDate = u.CreatedDate,
            });

        }
    }
}

