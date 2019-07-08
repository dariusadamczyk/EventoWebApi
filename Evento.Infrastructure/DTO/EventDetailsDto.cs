using System;
using System.Collections.Generic;

namespace Evento.InfraStructure.DTO
{
    public class EventDetailsDto : EventDto
    {
        public IEnumerable<TicketDto> Tickets { get; set; }
    }
}
