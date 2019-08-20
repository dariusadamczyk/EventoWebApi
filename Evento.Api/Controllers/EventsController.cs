using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Evento.InfraStructure.Commands.Events;
using Evento.InfraStructure.DTO;
using Evento.InfraStructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Evento.Api.Controllers
{
    [Route("[controller]")]
    public class EventsController: ApiControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IMemoryCache _memoryCache;

        public EventsController(IEventService eventService, IMemoryCache memoryCache)
        {
            _eventService = eventService;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string name)
        {
            var events = _memoryCache.Get<IEnumerable<EventDto>>("events");
            if (events==null)
            {
                events = await _eventService.BrowseAsync(name);
            }
           

            return Json(events);
        }

        [HttpGet("{eventId}")]
        public async Task<IActionResult> Get(Guid eventId)
        {

            var @event = await _eventService.GetAsync(eventId);
            if (@event==null)
            {
                NotFound();
            }


            return Json(@event);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateEvent command)
        {
            command.EventId = Guid.NewGuid();
            await _eventService.CreateAync(command.EventId,command.Name, command.Description, command.StartDate, command.EndDate);
            await _eventService.AddTicketAsync(command.EventId, command.Tikets, command.Price);
            return Created($"/events/{command.EventId}",null);
        }

        [HttpPut("{eventId}")]
        public async Task<IActionResult> Put(Guid eventId, [FromBody]UpdateEvent command)
        {
            
            await _eventService.UpdateAsync(eventId, command.Name, command.Description);

            return NoContent();
        }

        [HttpDelete("{eventId}")]
        public async Task<IActionResult> Delete(Guid eventId)
        {

            await _eventService.DeleteAsync(eventId);
            return NoContent();
        }
    }
}
