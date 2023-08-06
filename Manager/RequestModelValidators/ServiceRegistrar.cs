using FluentValidation;
using JWT.Manager.JwtAuthentication.Request;
//using JWT.Manager.Validators;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JWT.Manager.RequestModelValidators;

namespace JWT.Manager.RequestValidators
{
    public static class ServiceRegistrar
    {
        public static void AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<JwtLoginModel>, JwtLoginRequestValidator>();
            services.AddScoped<IValidator<JwtRegisterModel>, JwtRegisterRequestValidator>();
            services.AddScoped<IValidator<JwtRegisterModel1>, JwtRegisterRequestValidator1>();
        }
    }
}
