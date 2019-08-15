using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using Evento.Core.Repositories;
using Evento.InfraStructure.DTO;
using Evento.InfraStructure.Extensions;

namespace Evento.InfraStructure.Services
{
    public class TicketService : ITicketService
    {
        private IUserRepository _userRepository;
        private IEventRepository _eventRepository;
        private IMapper _mapper;

        public TicketService(IUserRepository userRepository, IEventRepository eventRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        

        public async Task<TicketDto> GetAsync(Guid userId, Guid eventId, Guid ticketId)
        {
            var user = _userRepository.GetOrFailAsync(userId);
            var ticket = _eventRepository.GetTicketOrFailAsync(eventId, ticketId);

            return _mapper.Map<TicketDto>(ticket);
        }

        public async Task PurchaseAsync(Guid userId, Guid eventId, int amount)
        {
            var user = await _userRepository.GetOrFailAsync(userId);
            var @event = await _eventRepository.GetOrFailAsync(eventId);
            @event.PurchaseTickets(user, amount);
            await _eventRepository.UpdateAsync(@event);

        }

        public async Task CancelAsync(Guid userId, Guid eventId, int amount)
        {
            var user = await _userRepository.GetOrFailAsync(userId);
            var @event = await _eventRepository.GetOrFailAsync(eventId);
            @event.CancelTickets(user, amount);
            await _eventRepository.UpdateAsync(@event);
        }

        public async Task<IEnumerable<TicketDto>> GetForUserAsync(Guid userId)
        {
            var user = await _userRepository.GetOrFailAsync(userId);
            var events = await _eventRepository.BrowseAsync();

            var tickets = events.SelectMany(x => x.GetTicketsPurchesByUser(user));

            return _mapper.Map<IEnumerable<TicketDto>>(tickets);
        }
    }
}
