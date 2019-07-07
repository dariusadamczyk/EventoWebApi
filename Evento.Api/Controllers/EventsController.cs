using System;
using System.Threading.Tasks;
using Evento.InfraStructure.Commands.Events;
using Evento.InfraStructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Evento.Api.Controllers
{
    [Route("[controller]")]
    public class EventsController: Controller
    {
        private readonly IEventService _eventService;
        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string name)
        {

            var events =  await _eventService.BrowseAsync(name);

            return Json(events);
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
    }
}
