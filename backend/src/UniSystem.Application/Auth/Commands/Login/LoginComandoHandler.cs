using MediatR;
using UniSystem.Application.Common.Interfaces;
using UniSystem.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace UniSystem.Application.Auth.Commands.Login
{
    public class LoginComandoHandler : IRequestHandler<LoginComando, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public LoginComandoHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<string> Handle(LoginComando request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (user == null)
            {
                throw new Exception("Credenciais inválidas.");
            }

            if (!_passwordHasher.VerifyPassword(request.Senha, user.PasswordHash))
            {
                throw new Exception("Credenciais inválidas.");
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            return token;
        }
    }
}
