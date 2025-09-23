using Microsoft.Extensions.DependencyInjection;
using UniSystem.Application.Common.Interfaces;
using UniSystem.Infrastructure.Persistence;
using UniSystem.Infrastructure.Services;

namespace UniSystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            return services;
        }
    }
}