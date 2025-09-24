using MediatR;
using UniSystem.Application.Common.Interfaces;
using UniSystem.Domain.Entities;

namespace UniSystem.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public CreateUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // Check if email already exists
            var existingUser = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (existingUser != null)
            {
                throw new Exception("Email already exists."); 
            }

            var passwordHash = _passwordHasher.HashPassword(request.Password);

            var user = new User(request.Name, request.Email, passwordHash);

            await _userRepository.AddAsync(user, cancellationToken);

            return user.Id;
        }
    }
}