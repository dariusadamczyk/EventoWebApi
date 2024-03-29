﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Evento.InfraStructure.DTO;

namespace Evento.InfraStructure.Services
{
    public interface ITicketService : IService
    {
        Task<IEnumerable<TicketDetailsDto>> GetForUserAsync(Guid userId);
        Task<TicketDto> GetAsync(Guid userId, Guid eventId, Guid ticketId);
        Task PurchaseAsync(Guid userId, Guid eventId, int amount);
        Task CancelAsync(Guid userId, Guid eventId, int amount);
    }
}
