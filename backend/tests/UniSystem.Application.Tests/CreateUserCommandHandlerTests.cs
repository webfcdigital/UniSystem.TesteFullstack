using Moq;
using Xunit;
using FluentAssertions;
using UniSystem.Application.Users.Commands.CreateUser;
using UniSystem.Application.Common.Interfaces;
using UniSystem.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace UniSystem.Application.Tests
{
    public class CreateUserCommandHandlerTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IPasswordHasher> _mockPasswordHasher;
        private readonly CreateUserCommandHandler _handler;

        public CreateUserCommandHandlerTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockPasswordHasher = new Mock<IPasswordHasher>();
            _handler = new CreateUserCommandHandler(_mockUserRepository.Object, _mockPasswordHasher.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateUser_WhenEmailIsUnique()
        {
            // Arrange
            var command = new CreateUserCommand
            {
                Name = "Test User",
                Email = "test@example.com",
                Password = "Password123!"
            };

            _mockUserRepository.Setup(repo => repo.GetByEmailAsync(command.Email, It.IsAny<CancellationToken>()))
                .ReturnsAsync((User)null);

            _mockPasswordHasher.Setup(hasher => hasher.HashPassword(command.Password))
                .Returns("hashed_password");

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeEmpty();
            _mockUserRepository.Verify(repo => repo.AddAsync(It.Is<User>(u => u.Email == command.Email), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenEmailIsNotUnique()
        {
            // Arrange
            var command = new CreateUserCommand
            {
                Name = "Test User",
                Email = "existing@example.com",
                Password = "Password123!"
            };

            var existingUser = new User("Existing User", command.Email, "hashed_password");

            _mockUserRepository.Setup(repo => repo.GetByEmailAsync(command.Email, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingUser);

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Email already exists.");
            _mockUserRepository.Verify(repo => repo.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
