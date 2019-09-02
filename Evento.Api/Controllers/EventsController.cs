using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Evento.Api.Features.Events;
using Evento.InfraStructure.Commands.Events;
using Evento.InfraStructure.DTO;
using Evento.InfraStructure.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Evento.Api.Controllers
{
    [Route("[controller]")]
    public class EventsController: ApiControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IMemoryCache _memoryCache;
        private readonly IMediator mediator;

        public EventsController(IEventService eventService, IMemoryCache memoryCache, IMediator mediator)
        {
            _eventService = eventService;
            _memoryCache = memoryCache;
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string name)
        {
            var events = _memoryCache.Get<IEnumerable<EventDto>>("events");
            if (events==null)
            {
                //events = await _eventService.BrowseAsync(name);
                events = await mediator.Send(new BrowseEventsQuery { Name = name });
                _memoryCache.Set("events", events, TimeSpan.FromMinutes(1));
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

            await mediator.Publish(new AddEventCommand { Command = command });
     
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
