using UniSystem.Domain.Entities;

namespace UniSystem.Application.Common.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}