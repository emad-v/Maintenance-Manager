using MaintenanceManager.Business;
using MaintenanceManager.DomainModel.Models.Customers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ManitenanceManager.Web.API.Controllers
{

    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }


        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var result = await _customerService.ListCustomers();

            return Ok(result);
        }


        [HttpGet("{reference}")]
        public async Task<IActionResult> GetCustomerByReference(string reference)
        {
            try
            {
                var result = await _customerService.GetCustomerByReference(reference);

                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }


        [HttpPost]
        public async Task<IActionResult> CreateCustomer(AddCustomerDto addCustomerRequest)
        {
            try
            {
                var result = await _customerService.CreateCustomer(new AddCustomerDto
                {
                    Email = addCustomerRequest.Email,
                    Name = addCustomerRequest.Name,
                   Reference = addCustomerRequest.Reference,
                });

                return CreatedAtAction(
                    nameof(GetCustomerByReference),
                    new { reference = result.Reference }, result); // returns 201 - created
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);// returns 409
            }

        }

    


        [HttpPut("{reference}")]
        public async Task<IActionResult> UpdateCustomer(string reference, UpdateCustomerDto updateCustomerRequest)
        {
            //ASP.NET Core model binding automatically validates this
            //If the request body is missing / invalid, it returns 400 BadRequest automatically
            try
            {
                var result = await _customerService.UpdateCustomer(reference, updateCustomerRequest);

                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }


        [HttpDelete("{reference}")]
        public async Task<IActionResult> DeactivateCustomer(string reference)
        {
            try
            {
                await _customerService.DeactivateCustomer(reference);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }
    }
}
