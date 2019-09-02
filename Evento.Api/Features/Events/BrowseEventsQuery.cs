using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Evento.InfraStructure.DTO;
using Evento.InfraStructure.Services;
using MediatR;

namespace Evento.Api.Features.Events
{
    public class BrowseEventsQuery: IRequest<IEnumerable<EventDto>>
    {
        public string Name { get; set; }
    }

    public class BrowseEventsQueryHandler : IRequestHandler<BrowseEventsQuery, IEnumerable<EventDto>>
    {
        private readonly IEventService _eventService;

        public BrowseEventsQueryHandler(IEventService eventService )
        {
            _eventService = eventService;
        }
            
        public async Task<IEnumerable<EventDto>> Handle(BrowseEventsQuery request, CancellationToken cancellationToken)
        {
            return await _eventService.BrowseAsync(request.Name);
        }
    }
}
