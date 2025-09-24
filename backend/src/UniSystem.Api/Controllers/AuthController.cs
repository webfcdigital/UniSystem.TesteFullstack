using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniSystem.Application.Auth.Commands.Login;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace UniSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginComando command)
        {
            var token = await _mediator.Send(command);
            return Ok(token);
        }
    }
}