using MediatR;

namespace UniSystem.Application.Auth.Commands.Login
{
    public record LoginCommand : IRequest<string>
    {
        public string Email { get; init; }
        public string Password { get; init; }
    }
}