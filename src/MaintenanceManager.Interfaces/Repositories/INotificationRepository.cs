using MaintenanceManager.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.Interfaces.Repositories
{
    public interface INotificationRepository
    {
        Task<Notification>CreateNotification(Notification notification);
        Task<IEnumerable<Notification>> GetNotificationsByUser(string userReference);
    }
}
