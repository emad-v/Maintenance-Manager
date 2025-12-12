using MaintenanceManager.DomainModel.Entities;
using MaintenanceManager.DomainModel.Enums;
using MaintenanceManager.DomainModel.Helpers;
using MaintenanceManager.DomainModel.Models.Notification;
using MaintenanceManager.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.Business
{
    public class NotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IComponentRuleStatusRepository _statusRepository;
        private readonly IComponentRepository _componentRepository;
        private readonly IMachineRepository _machineRepository;
        private readonly IUserCustomerRepository _userCustomerRepository;

        public NotificationService(INotificationRepository notificationRepository, IComponentRuleStatusRepository statusRepository, IComponentRepository componentRepository, IMachineRepository machineRepository, IUserCustomerRepository userCustomerRepository)
        {
            this._notificationRepository = notificationRepository;
            this._statusRepository = statusRepository;
            this._componentRepository = componentRepository;
            this._machineRepository = machineRepository;
            this._userCustomerRepository = userCustomerRepository;
        }

        public async Task CreateOverDueNotifications(string componentRuleStatus)
        {

            var status = await _statusRepository.GetComponentRuleStatusByReference(componentRuleStatus);
            string componentRef = status.ComponentReference;

            var component = await _componentRepository.GetComponentByReference(componentRef);
            string machineRef = component.MachineReference;

            var machine = await _machineRepository.GetMachineByReference(machineRef);
            string customerRef = machine.CustomerReference;

            var users = await _userCustomerRepository.GetUsersByCustomerAsync(customerRef);

            foreach (var user in users)
            {
                if (user.Role == UserRole.Technician || user.Role == UserRole.Operator)
                {

                    await _notificationRepository.CreateNotification(new Notification
                    {
                        Reference = ReferenceGenerator.GenerateNotificationReference(),
                        RecipientReference = user.Reference,
                        Message = $"The Maintenance for the Component {componentRef} is due, the Usage counter has already reached the MaintenanceRule threshold  ",
                        StatusReference = componentRuleStatus,

                    });
                }
            }

        }


        public async Task<IEnumerable<NotificationResponseDto>> GetNotificationsByUser(string userRef)
        {
            //I need to check if user with userREf exist, better done here, or in the notificationRepoistory? 
            var notifications = await _notificationRepository.GetNotificationsByUser(userRef);


            return notifications.Select(n => new NotificationResponseDto
            {
                Reference = n.Reference,
                RecipientReference = n.RecipientReference,
                Message = n.Message,
                StatusReference = n.StatusReference,
                CreatedAt = n.CreatedAt,
            }
            );

        }


    }
}
