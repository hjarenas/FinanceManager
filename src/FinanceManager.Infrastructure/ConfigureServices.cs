using CleanArchitecture.Application.Common.Interfaces;
using FinanceManager.Infrstructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceManager.Infrstructure;
public static class ConfigureServices
{
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, 
            IConfiguration configuration)
    {
        var persistanceOptions = configuration.GetSection(PersistanceOptions.Persistance).Get<PersistanceOptions>();
        if (persistanceOptions.UseInMemoryDatabase)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("FinanceManagerDB"));
        }
        else
        {
            if(string.IsNullOrEmpty(persistanceOptions.PostgresSqlConnectionString))
            {
                throw new InvalidOperationException("Cannot use Postgres if it is not configured");
            }
            
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(persistanceOptions.PostgresSqlConnectionString));
        }
        // else
        // {
        //     services.AddDbContext<ApplicationDbContext>(options =>
        //         options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
        //             builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        // }
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        return services;
    }
}