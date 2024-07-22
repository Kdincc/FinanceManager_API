using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Task11.Application.Common.Behaviours;
using System.Globalization;

namespace Task11.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            ValidatorOptions.Global.LanguageManager.Culture = new("en");
            services.AddValidatorsFromAssembly(assembly);

            return services;
        }
    }
}
