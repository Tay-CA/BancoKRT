using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GestaoLimitesContas.App
{
    public static class DependencyInjection
    {
        public static void AddApplicationDependencies(this IServiceCollection services)
        {
            var assembly = Assembly.Load("GestaoLimitesContas.App");
            services.AddMediatR(p =>
            {
                p.RegisterServicesFromAssembly(assembly);
            });
        }
    }
}
