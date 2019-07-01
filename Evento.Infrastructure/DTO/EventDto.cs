using System;
using System.Collections.Generic;
using Evento.Core.Domain;

namespace Evento.InfraStructure.DTO
{
    public class EventDto
    {
        public Guid Id { get; set; }
        public string Name { get;  set; }
        public string Description { get;  set; }

        public DateTime StartDate { get;  set; }
        public DateTime EndDate { get;  set; }
        public DateTime UpdateAt { get; set; }

        public int TicketsCount { get; set; }
    }
}
