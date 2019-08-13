using System;
using System.Threading.Tasks;
using Evento.InfraStructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Evento.Api.Controllers
{ 

    [Authorize]
    [Route("events/{eventId}/tickets")]
    public class TicketsController: ApiControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet("{ticketId}")]
        public async Task<IActionResult> Get(Guid eventId, Guid ticketId)
        {
            var ticket = await _ticketService.GetAsync(userID, eventId, ticketId);

            return Json(ticket);
        }

        [HttpPost("purchase/{amount}")]
        public async Task<IActionResult> Post(Guid eventId, int amount)
        {
           await _ticketService.PurchaseAsync(userID, eventId,amount);
            return NoContent();
        }


        [HttpDelete("cancel/{amount}")]
        public async Task<IActionResult> Delete(Guid eventId, int amount)
        {
            await _ticketService.CancelAsync(userID, eventId, amount);
            return NoContent();
        }
    }
}
