using MaintenanceManager.DomainModel.Entities;
using MaintenanceManager.Interfaces.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Reflection.Metadata.Ecma335;
using MaintenanceManager.DomainModel.Models.Customers;



namespace MaintenanceManager.Business
{
    public class CustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CustomerResponseDto>> ListCustomers()
        {
            var customers = await _repository.GetCustomers(); // get the active customers 
            var result = customers.Select(c => new CustomerResponseDto
            {
                Reference = c.Reference,
                Name = c.Name,
                Email = c.Email,
                CreatedDate = c.CreatedDate
            });
            return result;
        }


        public async Task<CustomerResponseDto> GetCustomerByReference(string reference)
        {
            Customer customer = (await _repository.GetCustomerByReference(reference));

            return new CustomerResponseDto()
            {
                Reference = customer.Reference,
                Name = customer.Name,
                Email = customer.Email,
                CreatedDate = customer.CreatedDate

            };
        }


        public async Task<CustomerResponseDto> CreateCustomer(AddCustomerDto addCustomerRequest)
        {

            if (await _repository.ReferenceExistAsync(addCustomerRequest.Reference))
            {
                throw new InvalidOperationException($"Customer with reference {addCustomerRequest.Reference} already exist");
            }

            if (await _repository.EmailExistsAsync(addCustomerRequest.Email))
            {
                throw new InvalidOperationException($"Customer with email {addCustomerRequest.Email} already exist");
            }

            Customer customer = new Customer()
            {
                Reference = addCustomerRequest.Reference,
                Name = addCustomerRequest.Name,
                Email = addCustomerRequest.Email
            };
            var response = await _repository.AddCustomer(customer);
            return new CustomerResponseDto()
            {
                Reference = response.Reference,
                Name = response.Name,
                Email = response.Email,
                CreatedDate = response.CreatedDate
            };
        }

        public async Task<CustomerResponseDto> UpdateCustomer(string reference, UpdateCustomerDto updateCustomerRequest)
        {
            Customer customer = await _repository.UpdateCustomer(reference, updateCustomerRequest.Name, updateCustomerRequest.Email);
            return new CustomerResponseDto
            {
                Reference = customer.Reference,
                Name = customer.Name,
                Email = customer.Email
            };
        }
        
        public async Task DeactivateCustomer(string reference)
        {
            await _repository.DeactivateCustomer(reference);
        }
    }
}