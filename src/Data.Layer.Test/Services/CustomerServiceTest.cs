using MaintenanceManager.Business;
using MaintenanceManager.Data.Models;
using MaintenanceManager.DomainModel.Entities;
using MaintenanceManager.DomainModel.Models.Customers;
using MaintenanceManager.Interfaces.Repositories;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Data.Layer.Test.Services
{

    [TestFixture]

    public class CustomerServiceTest
    {
        private FakeCustomerRepository _fakeCustomerRepository;
        private CustomerService _customerService;

        [SetUp]

        public void Setup()
        {
            _fakeCustomerRepository = new FakeCustomerRepository();
            _customerService = new CustomerService(_fakeCustomerRepository);

            //MethodName_Scenario_ExpectedBehavior
        }

        [Test]
        public async Task CreateCustomer_WhenEmailAlreadyExists_ThrowsInvalidOperationException()
        {
            _fakeCustomerRepository.ReferenceExistsAsSyncResult = false;
            _fakeCustomerRepository.EmailExistsAsSyncResult = true;
            AddCustomerDto addCustomerRequest = new AddCustomerDto()
            {
                Reference = "EEE",
                Name = "Emad",
                Email = "EMad@gmail.com"
            };
            await Assert.ThatAsync(() => _customerService.CreateCustomer(addCustomerRequest), Throws.InvalidOperationException);
        }


        [Test]
        public async Task CreateCustomer_WhenReferenceAlreadyExists_ThrowsInvalidOperationException()
        {
            _fakeCustomerRepository.ReferenceExistsAsSyncResult = true;
            _fakeCustomerRepository.EmailExistsAsSyncResult = false;

            AddCustomerDto addCustomerRequest = new AddCustomerDto()
            {
                Reference = "EEE",
                Name = "Emad",
                Email = "EMad@gmail.com"
            };
            await Assert.ThatAsync(() => _customerService.CreateCustomer(addCustomerRequest), Throws.InvalidOperationException);

        }

       
        //I am testing wether my call from teh service will call the reopository correcly or now 
        [Test]
        public async Task CreatingCustomer_WithValidData_CallsRepositoryAddCustomer()
        {
            _fakeCustomerRepository.ReferenceExistsAsSyncResult = false;
            _fakeCustomerRepository.EmailExistsAsSyncResult = false;
            AddCustomerDto addCustomerRequest = new AddCustomerDto()
            {
                Reference = "EEE",
                Name = "Emad",
                Email = "EMad@gmail.com"
            };

            await _customerService.CreateCustomer(addCustomerRequest);
            Assert.That(_fakeCustomerRepository.AddCustomerCalled, Is.Not.Null, "The Add Customer method wasnt called ");

        }

        [Test]
        public async Task CreatingCustomer_WithValidData_ReturnsCustomerDtoWithCorrectData()
        {
            AddCustomerDto addCustomerRequest = new AddCustomerDto()
            {
                Reference = "EEE",
                Name = "Emad",
                Email = "EMad@gmail.com"
            };

            var customer = await _customerService.CreateCustomer(addCustomerRequest);

            Assert.Multiple(() =>
            {
                Assert.That(customer, Is.Not.Null, "The Customer response dto shouldnt be null");
                Assert.That(customer.Reference, Is.EqualTo(addCustomerRequest.Reference), "Customer reference should match ");
                Assert.That(customer.Name, Is.EqualTo(addCustomerRequest.Name), "Customer name should match ");
                Assert.That(customer.Email, Is.EqualTo(addCustomerRequest.Email), "Customer Email should match ");
            });

        }

        [Test]
        public async Task GetCustomerByReference_WithValidReference_ReturnsCustomerREsponseDto()
        {
            _fakeCustomerRepository.GetCustomerByReferenceCalled = new Customer()
            {
                Reference = "EEE",
                Name = "Emad",
                Email = "EMad@gmail.com"
            };
            var dto = await _customerService.GetCustomerByReference("test");
            Assert.Multiple(() =>
            {
                Assert.That(dto, Is.Not.Null, "The Customer response dto shouldnt be null");
                Assert.That(dto.Reference, Is.EqualTo("EEE"), "Customer reference should match ");
                Assert.That(dto.Name, Is.EqualTo("Emad"), "Customer name should match ");
                Assert.That(dto.Email, Is.EqualTo("EMad@gmail.com"), "Customer Email should match ");
            });
        }



        [Test]
        public async Task GetCustomers_WithValidData_ReturnTheExpectedCustomersInDtoFormat()
        {
            Customer customerOne = new Customer()
            {
                Reference = "EEE",
                Name = "Emad",
                Email = "EMad@gmail.com"
            };

            Customer customerTwo = new Customer()
            {
                Reference = "FFF",
                Name = "Fead",
                Email = "Fead@gmail.com"
            };

            _fakeCustomerRepository.customers.Add(customerOne);
            _fakeCustomerRepository.customers.Add(customerTwo);

            var customersList = (await _customerService.ListCustomers()).ToList();



            Assert.Multiple(() =>

            {
                Assert.That(customersList.Count(), Is.EqualTo(2), "should return 2 customers");
                Assert.That(customersList[0].Reference, Is.EqualTo(customerOne.Reference), "First customer reference should match");
                Assert.That(customersList[0].Name, Is.EqualTo(customerOne.Name), "first customer name should match ");
                Assert.That(customersList[0].Email, Is.EqualTo(customerOne.Email), "first customer email should match ");

                Assert.That(customersList[1].Reference, Is.EqualTo(customerTwo.Reference), "second customer reference should match");
                Assert.That(customersList[1].Name, Is.EqualTo(customerTwo.Name), "second customer name should match ");
                Assert.That(customersList[1].Email, Is.EqualTo(customerTwo.Email), "second customer email should match ");
            });


        }

        [Test]
        public async Task UpdateCustomer_WithDifferentValues_CorrectlyReturnTheUpdatedOne()
        {
            Customer Customer = new Customer()
            {
                Reference = "EEE",
                Name = "oldName",
                Email = "old@gmail.com"
            };
            _fakeCustomerRepository.UpdateCustomerCalled = Customer;

           
            CustomerResponseDto customerResponseDto = await _customerService.UpdateCustomer("EEE", new UpdateCustomerDto()
            {
                Name = Customer.Name,
                Email = Customer.Email
            });
            Assert.Multiple(() =>
            {
                Assert.That(_fakeCustomerRepository.UpdateCustomerCalled, Is.Not.Null, "Repository should have customer");
                Assert.That(customerResponseDto, Is.Not.Null, "The Customer response dto shouldnt be null");

            });

        }

        //Does the service correctly call the repository? 
        [Test]
        public async Task DeactivateCustomer_WithReference_ReturnRepositoryCalled()
        {
            await _customerService.DeactivateCustomer("test");
            Assert.That(_fakeCustomerRepository.DeactivateCustomerReference,Is.EqualTo("test"));
        }

    }
    public class FakeCustomerRepository : ICustomerRepository
    {
        public bool ReferenceExistsAsSyncResult { get; set; }
        public bool EmailExistsAsSyncResult { get; set; }
        public Customer AddCustomerCalled { get; set; }
        public Customer GetCustomerByReferenceCalled { get; set; }
        public Customer UpdateCustomerCalled { get; set; }
        public string DeactivateCustomerReference {  get; set; }
        public List<Customer> customers = [];



        public Task<Customer> AddCustomer(Customer customer)
        {
            AddCustomerCalled = customer;
            return Task.FromResult(customer);
        }

        public Task DeactivateCustomer(string reference)
        {
            DeactivateCustomerReference = reference;
            return Task.CompletedTask;
        }

        public Task<bool> EmailExistsAsync(string email)
        {
            return Task.FromResult(EmailExistsAsSyncResult);
        }

        public Task<Customer> GetCustomerByReference(string reference)
        {
            return Task.FromResult(GetCustomerByReferenceCalled);
        }

        public Task<IEnumerable<Customer>> GetCustomers()
        {
            return Task.FromResult<IEnumerable<Customer>>(customers);
        }

        public Task<bool> ReferenceExistAsync(string reference)
        {
            return Task.FromResult(ReferenceExistsAsSyncResult);
        }

        public Task<Customer> UpdateCustomer(string reference, string name, string email)
        {
            return Task.FromResult(UpdateCustomerCalled);
        }
    }
}
