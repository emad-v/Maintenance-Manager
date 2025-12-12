using MaintenanceManager.Data.Models;
using MaintenanceManager.DomainModel.Entities;
using MaintenanceManager.Interfaces.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {

        private readonly MaintenanceManagerDbContext _context;
        public CustomerRepository(MaintenanceManagerDbContext context)
        {
            _context = context;
        }


        public async Task<Customer> AddCustomer(Customer customer)
        {
            customer.CreatedDate = DateTime.UtcNow;

            CustomerDb customerDb = MapToCustomerDb(customer);
            _context.Customers.Add(customerDb);
            await _context.SaveChangesAsync();
            return MapToCustomer(customerDb);
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            var customersDb = await _context.Customers
                .Where(c => c.IsActive == true)
                .ToListAsync();//<CustomerDb> Customers

            return customersDb.Select(customersDb => MapToCustomer(customersDb));
        }


        public async Task<Customer> GetCustomerByReference(string reference)
        {
            CustomerDb? customerDb = await _context.Customers.SingleOrDefaultAsync(c => c.Reference == reference && c.IsActive);
            if (customerDb == null)
            {
                throw new KeyNotFoundException($"Customer with reference: {reference} was not found");
            }
            return MapToCustomer(customerDb);
        }

        public async Task<Customer> UpdateCustomer(string reference, string name, string email)
        {
            var customerDb = await _context.Customers
                .SingleOrDefaultAsync(c => c.Reference == reference && c.IsActive);
            if (customerDb == null)
            {
                throw new KeyNotFoundException($"Customer with reference: {reference} was not found");
            }

            customerDb.Name = name;
            customerDb.Email = email;
            await _context.SaveChangesAsync();
            return MapToCustomer(customerDb);
        }

        public async Task DeactivateCustomer(string reference)
        {
            var customerDb = await _context.Customers
                .SingleOrDefaultAsync(c => c.Reference == reference && c.IsActive);
            if (customerDb == null)
            {
                throw new KeyNotFoundException($"Customer with reference: {reference} was not found");
            }
            customerDb.IsActive = false;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Customers.AnyAsync(c => c.Email == email && c.IsActive);
        }

        public async Task<bool> ReferenceExistAsync(string reference)
        {
            return await _context.Customers.AnyAsync(c => c.Reference == reference && c.IsActive);
        }


        private static CustomerDb MapToCustomerDb(Customer customer)
        {
            return new CustomerDb
            {
                Reference = customer.Reference,
                Name = customer.Name,
                Email = customer.Email,
                CreatedDate = customer.CreatedDate,
                IsActive = customer.IsActive,
            };
        }

        private static Customer MapToCustomer(CustomerDb customerDb)
        {
            return new Customer
            {
                Reference = customerDb.Reference,
                Name = customerDb.Name,
                Email = customerDb.Email,
                CreatedDate = customerDb.CreatedDate,
                IsActive = customerDb.IsActive
            };
        }
       

     

    
    }
}

//Direction 1: Database → Domain (MapToCustomer)
//GetCustomerByReference() queries CustomerDB, returns Customer 

//Direction 2: Domain → Database (MapToCustomerDB)
//example: AddCustomer() receives Customer, saves CustomerDB