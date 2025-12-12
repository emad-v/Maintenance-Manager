using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using MaintenanceManager.Data.Models;
using MaintenanceManager.DomainModel.Entities;
using MaintenanceManager.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.Data.Repositories
{
    public class NotificationDynamoDbRepository : INotificationRepository
    {
        private readonly DynamoDBContext _context;

        public NotificationDynamoDbRepository(DynamoDBContext context)
        {
            _context = context;
        }

        public async Task<Notification> CreateNotification(Notification notification)
        {
            notification.CreatedAt = DateTime.UtcNow;

            var notificationDynamoDb = MapToNotificationDynamoDb(notification);
            await _context.SaveAsync(notificationDynamoDb);
            return MapToNotification(notificationDynamoDb);
        }

        public async Task<IEnumerable<Notification>> GetNotificationsByUser(string userReference)
        {
            var userPk = $"USER#{userReference}"; 
            var search  = _context.QueryAsync<NotificationDynamoDb>(userPk);
            
            var results = await search.GetRemainingAsync();

            return results.Select(MapToNotification);

        }


        //Load = Get ONE specific item (needs both PK AND SK)
        // Query = Get ALL items with the same PK




        private static Notification MapToNotification(NotificationDynamoDb NotificationDynamoDb)
        {
            return new Notification
            {
                Reference = NotificationDynamoDb.Reference,
                RecipientReference = NotificationDynamoDb.RecipientReference,
                Message = NotificationDynamoDb.Message,
                StatusReference = NotificationDynamoDb.StatusReference,
                CreatedAt = NotificationDynamoDb.CreatedAt,
            };
        }

        private static NotificationDynamoDb MapToNotificationDynamoDb(Notification notification)
        {
            return new NotificationDynamoDb
            {
                PK = $"USER#{notification.RecipientReference}",
                SK = $"NOTIF#{notification.CreatedAt:yyyyMMddHHmmss}",
                Reference = notification.Reference,
                RecipientReference = notification.RecipientReference,
                Message = notification.Message,
                StatusReference = notification.StatusReference,
                CreatedAt = notification.CreatedAt,
            };
        }
    }
}
