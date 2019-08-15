using System;
using System.Threading.Tasks;
using Evento.InfraStructure.Commands.Users;
using Evento.InfraStructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Evento.Api.Controllers
{
    public class AccountController : ApiControllerBase
    {
        public AccountController(IUserService userService, ITicketService ticketService)
        {
            _userService = userService;
            _ticketService = ticketService;
        }

        private IUserService _userService { get; }
        public ITicketService _ticketService { get; }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        => Json(await _userService.GetAccountAsync(userID));

        [HttpGet("tickets")]
        [Authorize]
        public async Task<IActionResult> GetTickets()
        => Json(await _ticketService.GetForUserAsync(userID));
           
        

        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody]Register command)
        {
            await _userService.RegisterAsync(Guid.NewGuid(), command.Email, command.Name, command.Password, command.Role);

            return Created("/account", null);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody]Login command)
        => Json(await _userService.LoginAsync(command.Email, command.Password));
    }
}
