using JWT.InternalServices.Interfaces;
using JWT.InternalServices.Services;
using Microsoft.Extensions.DependencyInjection;

namespace JWT.InternalServices
{
    public static class ServiceRegistrar
    {
        public static void AddInternalService(this IServiceCollection services)
        {
            services.AddTransient<IJwtAuthenTokenService, JwtAuthenTokenService>();
        }
    }
}