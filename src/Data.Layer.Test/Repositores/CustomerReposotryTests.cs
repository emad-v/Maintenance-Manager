//using MaintenanceManager.Data;
//using MaintenanceManager.Data.Repositories;
//using MaintenanceManager.DomainModel.Entities;
//using Microsoft.EntityFrameworkCore;
//using System.Runtime.CompilerServices;
//using System.Threading.Tasks;


//namespace Data.Layer.Test.Repositores;

//public class Tests
//{
//    private CustomerRepository _repository; // the repo which is the place where we talk with the database
//    private MaintenanceManagerDbContext _context;//the bridge between C# and the database ( postgress etc ) 
//    [SetUp]
//    public void Setup()
//    {
//        var options = new DbContextOptionsBuilder<MaintenanceManagerDbContext>()
//        .UseInMemoryDatabase(databaseName: "TestDb")
//        .Options;

//        _context = new MaintenanceManagerDbContext(options);
//        _repository = new CustomerRepository(_context);

//    }

//    [TearDown]
//    public async Task CleanUp()
//    {
//        await _context.Database.EnsureDeletedAsync();
//        await _context.DisposeAsync();//dispose               
//    }


//    [Test]
//    public async Task AddCustomer_MustReturnCreatedCustomer()
//    {
//        Customer customer = new Customer() { Name = "Emad", Email = "Test@gmail.com" };
//        var result = await _repository.AddCustomer(customer);

//        Assert.Multiple(() =>
//        {
//            Assert.That(customer.Name.Equals(result.Name), Is.True);
//            Assert.That(result, Is.Not.Null, "Result should not be null");
//            Assert.That(result.Id, Is.GreaterThan(0), "id should be set by the database");
//            Assert.That(result.Name, Is.EqualTo(customer.Name), "customer name should match ");
//            Assert.That(result.Email, Is.EqualTo(customer.Email), "Customer email should match ");
//        }
//        );
//    }

//    [Test]
//    public async Task ListingCustomers_MustReturnAllOfThem()
//    {
//        Customer customerOne = new Customer() { Id = 1, Name = "test1", Email = "Test1@gmail.com", CreatedDate = DateTime.Now, IsActive = true };
//        Customer customerTwo = new Customer() { Id = 2, Name = "test2", Email = "Test2@gmail.com", CreatedDate = DateTime.Now, IsActive = true };
//        await _repository.AddCustomer(customerOne);
//        await _repository.AddCustomer(customerTwo);
//        int count = await _context.Customers.CountAsync();

//        Assert.Multiple(() =>
//        {
//            Assert.That(_context.Customers, Is.Not.Null, "The list should not be null after adding customers  ");
//            Assert.That(count, Is.EqualTo(2), "The list should containt the same number of customers after adding them");

//        });

//    }


//    [Test]
//    public async Task GettingCustomerByID_MustReturnTheSameCusotmer()
//    {
//        Customer customerOne = new Customer() { Id = 1, Name = "test1", Email = "Test1@gmail.com", CreatedDate = DateTime.Now, IsActive = true };
//        await _repository.AddCustomer(customerOne);
//        var getCustomer = await _repository.GetCustomerById(1);

//        Assert.Multiple(() =>
//        {
//            Assert.That(_context.Customers, Is.Not.Null, "The list should not be null after adding customers  ");
//            Assert.That(getCustomer.Id, Is.EqualTo(customerOne.Id), "The retrieved Customer id must match ");
//            Assert.That(getCustomer.Name, Is.EqualTo(customerOne.Name), "customer name should match ");
//            Assert.That(getCustomer.Email, Is.EqualTo(customerOne.Email), "Customer email should match ");
//        });
//    }
//    [Test]
//    public void GettingNonExistenceCustomer_MustThrowKeyNotFoundException()
//    {

//        int NonExistenceCustomerId = 999;

//        Assert.ThrowsAsync<KeyNotFoundException>(async () =>
//       {
//           await _repository.GetCustomerById(NonExistenceCustomerId);
//       });

//    }



//    [Test]
//    public async Task UpdatingCustomerInfo_MustReturnTheNewValues()
//    {
//        Customer customerOne = new Customer() { Id = 1, Name = "test1", Email = "Test1@gmail.com", CreatedDate = DateTime.Now, IsActive = true };
//        await _repository.AddCustomer(customerOne);
//        await _repository.UpdateCustomer(customerOne.Id, "NewName", "NewEmail@gmail.com");
//        var updatedCustomer = await _repository.GetCustomerById(customerOne.Id);
//        Assert.Multiple(() =>
//        {
//            Assert.That(updatedCustomer.Name, Is.EqualTo("NewName"), "new customer name should match ");
//            Assert.That(updatedCustomer.Email, Is.EqualTo("NewEmail@gmail.com"), "new Customer email should match ");
//        });
//    }

//    [Test]
//    public async Task DeactivatingCustomer_WillNoLongerBeInTheList()
//    {
//        Customer customerOne = new Customer() { Id = 1, Name = "test1", Email = "Test1@gmail.com", CreatedDate = DateTime.Now, IsActive = true };
//        Customer customerTwo = new Customer() { Id = 2, Name = "test2", Email = "Test2@gmail.com", CreatedDate = DateTime.Now, IsActive = true };
//        await _repository.AddCustomer(customerOne);
//        await _repository.AddCustomer(customerTwo);
//        await _repository.DeactivateCustomer(2);
//        int count = await _context.Customers.CountAsync(c => c.IsActive);

//        Assert.Multiple(() =>
//        {
//            Assert.That(count, Is.EqualTo(1), "The list must only have one value after deactivating the other customer");
//            Assert.That(customerTwo.IsActive, Is.False);
//        });

//    }
//}
