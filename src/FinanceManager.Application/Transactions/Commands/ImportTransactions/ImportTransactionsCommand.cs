using FinanceManager.Domain.TransactionsAggregate;

namespace FinanceManager.Application.Transactions.Commands.ImportTransactions;
public record ImportTransactionsCommand(
    BankAccountDto BankAccountInformation,
    DateOnly BookingDate,
    string ThirdParty,
    string Description,
    string IntendedUse,
    Direction Direction,
    double AmountInEur,
    bool IsRecurring
);
