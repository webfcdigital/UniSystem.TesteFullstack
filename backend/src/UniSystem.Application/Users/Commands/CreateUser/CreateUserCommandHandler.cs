using MediatR;
using UniSystem.Application.Common.Interfaces;
using UniSystem.Domain.Entities;

namespace UniSystem.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public CreateUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // Check if email already exists
            var existingUser = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (existingUser != null)
            {
                throw new Exception("Email already exists."); // Or a more specific custom exception
            }

            var passwordHash = _passwordHasher.HashPassword(request.Password);

            var user = new User(request.Name, request.Email, passwordHash);

            await _userRepository.AddAsync(user, cancellationToken);

            return user.Id;
        }
    }
}