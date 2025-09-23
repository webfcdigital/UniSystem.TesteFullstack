using MediatR;
using UniSystem.Application.Common.Interfaces;
using UniSystem.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace UniSystem.Application.Auth.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public LoginCommandHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            // 1. Retrieve the user by email
            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (user == null)
            {
                // TODO: Throw a specific exception for invalid credentials
                throw new Exception("Invalid credentials.");
            }

            // 2. Verify the password
            if (!_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
            {
                // TODO: Throw a specific exception for invalid credentials
                throw new Exception("Invalid credentials.");
            }

            // 3. Generate JWT
            var token = _jwtTokenGenerator.GenerateToken(user);

            // 4. Return the JWT
            return token;
        }
    }
}