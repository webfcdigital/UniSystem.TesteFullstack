using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniSystem.Application.Users.Queries.GetUsers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using UniSystem.Application.Users.Commands.CreateUser;
using Microsoft.AspNetCore.Cors;

namespace UniSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateUser(CriarUsuarioComando command)
        {
            var userId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetUsers), new { id = userId }, userId);
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            var users = await _mediator.Send(new GetUsersQuery());
            return Ok(users);
        }
    }
}