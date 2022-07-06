using System.Text.Json.Serialization;
using FinanceManager.WebAPI.RecurringJobs;

namespace FinanceManager.WebAPI;
public static class RegisterWebAPI
{
    public static void AddWebApiServices(this IServiceCollection services)
    {
        services.AddHostedService<ImportTransactions>();
        services
            .AddControllers(options => options.UseDateOnlyTimeOnlyStringConverters())
            .AddJsonOptions(options =>
            {
                options.UseDateOnlyTimeOnlyStringConverters();
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c => c.UseDateOnlyTimeOnlyStringConverters());
    }
}
