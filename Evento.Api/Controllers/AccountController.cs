using System;
using System.Threading.Tasks;
using Evento.InfraStructure.Commands.Users;
using Evento.InfraStructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Evento.Api.Controllers
{
    public class AccountController: Controller
    {
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        private  IUserService _userService { get; }

        [HttpGet]
        public async Task<IActionResult> Get()
        {

            throw new NotImplementedException();
        }

        [HttpGet("tickets")]
        public async Task<IActionResult> GetTickets()
        {

            throw new NotImplementedException();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Post(Register command)
        {
            await _userService.RegisterAsync(Guid.NewGuid(), command.Email, command.Name, command.Password, command.Role);

            return Created("/account", null);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Post(Login command)
        => Json(await _userService.LoginAsync(command.Email, command.Password));
    }
}
