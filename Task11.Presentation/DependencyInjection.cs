using Mapster;
using MapsterMapper;
using System.Reflection;
using Task11.Presentation.Converters;

namespace Task11.Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter()));
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddMappings();


            return services;
        }

        private static IServiceCollection AddMappings(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;

            config.Scan(Assembly.GetExecutingAssembly());

            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();

            return services;
        }
    }
}
