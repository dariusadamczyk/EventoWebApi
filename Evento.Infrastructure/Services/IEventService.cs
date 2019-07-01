using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Evento.Core.Domain;
using Evento.InfraStructure.DTO;

namespace Evento.InfraStructure.Services
{
    public interface IEventService
    {
        Task<EventDto> GetAsync(Guid id);
        Task<EventDto> GetAsync(string name);
        Task<IEnumerable<EventDto>> BrowseAsync(string name = null);
        Task CreateAync(Guid id, string name, string description, DateTime startDate, DateTime endDate);
        Task UpdateAsync(Guid id, string name, string description);
        Task DeleteAsync(Guid id);
        Task AddTicketAsync(Guid eventId, int amount, decimal price);

    }
}
