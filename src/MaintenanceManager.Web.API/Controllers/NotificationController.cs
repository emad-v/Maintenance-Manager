using MaintenanceManager.Business;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.Web.API.Controllers
{
    [ApiController]
    [Route("api/")]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationService _notificationService;

        public NotificationController(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("users/{userRef}/notifications")]
        public async Task<IActionResult> GetNotificationsByUser(string userRef)
        {
            
                var result = await _notificationService.GetNotificationsByUser(userRef);
                return Ok(result);  
            
        }
    }
}
