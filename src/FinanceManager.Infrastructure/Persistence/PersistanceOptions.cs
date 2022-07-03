namespace FinanceManager.Infrastructure.Persistence;
public record PersistanceOptions
{
    public const string Persistance = nameof(Persistance);
    public bool UseInMemoryDatabase { get; set; }
    public string? PostgresSqlConnectionString { get; set; }
}
