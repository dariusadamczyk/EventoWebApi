using System;
using AutoMapper;
using Evento.Core.Domain;
using Evento.InfraStructure.DTO;
using System.Linq;

namespace Evento.InfraStructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration( cfg =>
            {
                cfg.CreateMap<Event, EventDto>()
                .ForMember(x => x.TicketsCount, m => m.MapFrom(p => p.Tickets.Count()))
                .ForMember(x => x.AvailableTicketsCount, m => m.MapFrom(p => p.AvailableTickets.Count()))
                .ForMember(x => x.PurchasedTicketsCount, m => m.MapFrom(p => p.PurchasedTickets.Count()));
                cfg.CreateMap<Ticket, TicketDto>();
                cfg.CreateMap<Event, EventDetailsDto>();
                cfg.CreateMap<User, AccountDto>();


             }).CreateMapper();
    }
}
