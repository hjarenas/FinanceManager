using FinanceManager.Infrastructure.ExternalServices.BankDataImporters;

namespace FinanceManager.WebAPI.RecurringJobs;
public class ImportTransactions : BackgroundService
{

    private readonly PeriodicTimer _timer = new(TimeSpan.FromSeconds(5));
    private readonly IServiceProvider _serviceProvider;

    public ImportTransactions(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var transactionsImporterService =
                scope.ServiceProvider.GetRequiredService<ITransactionsImporterService>();
        while (await _timer.WaitForNextTickAsync(stoppingToken)
            && !stoppingToken.IsCancellationRequested)
        {
            await transactionsImporterService.ImportDataFromAllBanksAsync();
        }
    }
}
