using MediatR;
using UniSystem.Application.Common.Interfaces;
using UniSystem.Domain.Entities;

namespace UniSystem.Application.Users.Commands.CreateUser
{
    public class CriarUsuarioComandoHandler : IRequestHandler<CriarUsuarioComando, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public CriarUsuarioComandoHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<string> Handle(CriarUsuarioComando request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (existingUser != null)
            {
                throw new Exception("O e-mail já existe.");
            }

            var passwordHash = _passwordHasher.HashPassword(request.Senha);

            var user = new User(request.Nome, request.Email, passwordHash);

            await _userRepository.AddAsync(user, cancellationToken);

            return user.Id;
        }
    }
}
