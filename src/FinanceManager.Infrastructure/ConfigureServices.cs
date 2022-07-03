using FinanceManager.Application.Common.Interfaces;
using FinanceManager.Infrastructure.ExternalServices.BankDataImporters;
using FinanceManager.Infrastructure.ExternalServices.BankDataImporters.BankSpecificImporters.Ing;
using FinanceManager.Infrastructure.Files;
using FinanceManager.Infrastructure.Files.Csv;
using FinanceManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceManager.Infrastructure;
public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(
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
            if (string.IsNullOrEmpty(persistanceOptions.PostgresSqlConnectionString))
            {
                throw new InvalidOperationException("Cannot use Postgres if it is not configured");
            }

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(persistanceOptions.PostgresSqlConnectionString));
        }
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IFileSearchService, FileSearchService>();
        services.AddScoped<ITransactionsImporterService, TransactionsImporterService>();
        services.AddScoped<IBankTransactionsImporter, IngTransactionsImporter>();
        services.AddScoped<ICsvReader, CsvReader>();
        services.AddScoped<ICsvReaderFactory, CsvReaderFactory>();
        return services;
    }
}
