namespace FinanceManager.Application.Transactions;
public record BankAccountDto(string Isin, IEnumerable<string> AccountHolders);
