using MediatR;

namespace UniSystem.Application.Users.Commands.CreateUser
{
    public class CriarUsuarioComando : IRequest<string>
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
