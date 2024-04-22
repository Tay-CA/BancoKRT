using GestaoLimitesContas.Infraestructure.Contexts;
using GestaoLimitesContas.Infraestructure.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GestaoLimitesContas.Infraestructure
{
    public static class DependencyInjection
    {
        public static void AddInfraDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IGestaoLimiteContext, GestaoLimiteContext>();
            services.Configure<DynamoOptions>(configuration.GetSection(DynamoOptions.DynamoDb));
            var sp = services.BuildServiceProvider();
            var gestaoLimiteContext = sp.GetRequiredService<IGestaoLimiteContext>();
            gestaoLimiteContext.Seed();
        }
    }
}