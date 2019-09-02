using System;
using System.Threading;
using System.Threading.Tasks;
using Evento.InfraStructure.Commands.Events;
using Evento.InfraStructure.Services;
using MediatR;

namespace Evento.Api.Features.Events
{
    public class AddEventCommand : INotification
    {
        public CreateEvent Command { get; set; }
    }

    public class AddEvent : INotificationHandler<AddEventCommand>
    {
        private readonly IEventService _eventService;

        public AddEvent(IEventService eventService)
        {
            _eventService = eventService;
        }

        public async Task Handle(AddEventCommand notification, CancellationToken cancellationToken)
        {
            await _eventService.CreateAync(notification.Command.EventId, notification.Command.Name, notification.Command.Description, notification.Command.StartDate, notification.Command.EndDate);
            await _eventService.AddTicketAsync(notification.Command.EventId, notification.Command.Tikets, notification.Command.Price);
        }
    }
    
}
