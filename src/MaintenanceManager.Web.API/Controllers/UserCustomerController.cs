using MaintenanceManager.Business;
using MaintenanceManager.DomainModel.Models.User;
using MaintenanceManager.DomainModel.Models.UserCustomerLink;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.Web.API.Controllers
{
    [ApiController]
    [Route("api/")]
    public class UserCustomerController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly UserCustomerService _userCustomerService;

        public UserCustomerController(UserService userService, UserCustomerService userCustomerService)
        {
            _userService = userService;
            _userCustomerService = userCustomerService;
        }

        [HttpPost("users")]
        public async Task<IActionResult> CreateUser(AddUserDto addUserRequest)
        {
            try
            {
                var result = await _userService.CreateUser(addUserRequest);
                return StatusCode(201, result);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);// returns 409
            }
        }

        [HttpPost("customers/{customerRef}/users")]
        public async Task<IActionResult> LinkUserToCustomer(string customerRef, [FromBody] LinkUserDto linkUserRequest)
        {
            try
            {
                var result = await _userCustomerService.LinkUserToCustomer(linkUserRequest, customerRef);
                return StatusCode(201, result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpGet("customers/{customerRef}/users")]
        public async Task<IActionResult> GetUsersForCustomer(string customerRef)
        {
            try
            {
                var result = await _userCustomerService.GetUsersByCustomerAsync(customerRef);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }
    }
}
