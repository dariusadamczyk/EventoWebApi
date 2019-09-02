using System;
using System.Threading;
using System.Threading.Tasks;
using Evento.InfraStructure.Services;
using MediatR;

namespace Evento.Api.Features.Events
{
    public class AddEventCommandPostProcessor : INotificationHandler<INotification>
    {
        private readonly IEventService _eventService;

        public AddEventCommandPostProcessor(IEventService eventService)
        {
            _eventService = eventService;
        }

        

        public Task Handle(INotification notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
