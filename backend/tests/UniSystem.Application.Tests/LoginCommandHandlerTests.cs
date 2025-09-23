using Moq;
using Xunit;
using FluentAssertions;
using UniSystem.Application.Auth.Commands.Login;
using UniSystem.Application.Common.Interfaces;
using UniSystem.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace UniSystem.Application.Tests
{
    public class LoginCommandHandlerTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IPasswordHasher> _mockPasswordHasher;
        private readonly Mock<IJwtTokenGenerator> _mockJwtTokenGenerator;
        private readonly LoginCommandHandler _handler;

        public LoginCommandHandlerTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockPasswordHasher = new Mock<IPasswordHasher>();
            _mockJwtTokenGenerator = new Mock<IJwtTokenGenerator>();
            _handler = new LoginCommandHandler(
                _mockUserRepository.Object,
                _mockPasswordHasher.Object,
                _mockJwtTokenGenerator.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnToken_WhenCredentialsAreValid()
        {
            // Arrange
            var email = "test@example.com";
            var password = "Password123!";
            var hashedPassword = "hashedpassword";
            var user = new User("Test User", email, hashedPassword);
            var expectedToken = "jwt_token_here";

            _mockUserRepository.Setup(repo => repo.GetByEmailAsync(email, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);
            _mockPasswordHasher.Setup(hasher => hasher.VerifyPassword(password, hashedPassword))
                .Returns(true);
            _mockJwtTokenGenerator.Setup(generator => generator.GenerateToken(user))
                .Returns(expectedToken);

            var command = new LoginCommand { Email = email, Password = password };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(expectedToken);
            _mockUserRepository.Verify(repo => repo.GetByEmailAsync(email, It.IsAny<CancellationToken>()), Times.Once);
            _mockPasswordHasher.Verify(hasher => hasher.VerifyPassword(password, hashedPassword), Times.Once);
            _mockJwtTokenGenerator.Verify(generator => generator.GenerateToken(user), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenUserNotFound()
        {
            // Arrange
            var email = "nonexistent@example.com";
            var password = "Password123!";

            _mockUserRepository.Setup(repo => repo.GetByEmailAsync(email, It.IsAny<CancellationToken>()))
                .ReturnsAsync((User)null);

            var command = new LoginCommand { Email = email, Password = password };

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Invalid credentials.");
            _mockUserRepository.Verify(repo => repo.GetByEmailAsync(email, It.IsAny<CancellationToken>()), Times.Once);
            _mockPasswordHasher.Verify(hasher => hasher.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            _mockJwtTokenGenerator.Verify(generator => generator.GenerateToken(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenPasswordIsInvalid()
        {
            // Arrange
            var email = "test@example.com";
            var password = "WrongPassword!";
            var hashedPassword = "hashedpassword";
            var user = new User("Test User", email, hashedPassword);

            _mockUserRepository.Setup(repo => repo.GetByEmailAsync(email, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);
            _mockPasswordHasher.Setup(hasher => hasher.VerifyPassword(password, hashedPassword))
                .Returns(false);

            var command = new LoginCommand { Email = email, Password = password };

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Invalid credentials.");
            _mockUserRepository.Verify(repo => repo.GetByEmailAsync(email, It.IsAny<CancellationToken>()), Times.Once);
            _mockPasswordHasher.Verify(hasher => hasher.VerifyPassword(password, hashedPassword), Times.Once);
            _mockJwtTokenGenerator.Verify(generator => generator.GenerateToken(It.IsAny<User>()), Times.Never);
        }
    }
}