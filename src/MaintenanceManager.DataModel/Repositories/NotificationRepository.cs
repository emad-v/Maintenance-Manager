using MaintenanceManager.Data.Models;
using MaintenanceManager.DomainModel.Entities;
using MaintenanceManager.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.Data.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly MaintenanceManagerDbContext _context;

        public NotificationRepository(MaintenanceManagerDbContext context)
        {
            _context = context;
        }

     

        public async Task<Notification> CreateNotification(Notification notification)
        {
            //notification.CreatedAt = DateTime.UtcNow;
            //NotificationDb notificationDb = MapToNotificationDb(notification);
            //_context.Notifications.Add(notificationDb);
            //await _context.SaveChangesAsync();
            //return MapToNotification(notificationDb);
            throw new NotImplementedException();

        }

        public async Task<IEnumerable<Notification>> GetNotificationsByUser(string userReference)
        {
            //var notificationsDb = await _context.Notifications.Where(n => n.RecipientReference == userReference).ToListAsync();
            //return notificationsDb.Select(n => MapToNotification(n));
            throw new NotImplementedException();
        }

        private static Notification MapToNotification(NotificationDb notificationDb)
        {
            return new Notification
            {
                Reference = notificationDb.Reference,
                RecipientReference = notificationDb.RecipientReference,
                Message = notificationDb.Message,
                StatusReference = notificationDb.StatusReference,
                CreatedAt = notificationDb.CreatedAt,
            };
        }

        private static NotificationDb MapToNotificationDb(Notification notification)
        {
            return new NotificationDb
            {
                Reference = notification.Reference,
                RecipientReference = notification.RecipientReference,
                Message = notification.Message,
                StatusReference = notification.StatusReference,
                CreatedAt = notification.CreatedAt,
            };
        }
    }
}

