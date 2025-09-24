using MediatR;

namespace UniSystem.Application.Auth.Commands.Login
{
    public record LoginComando : IRequest<string>
    {
        public string Email { get; init; }
        public string Senha { get; init; }
    }
}
