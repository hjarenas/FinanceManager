using FinanceManager.Domain.AccountsAggregate;
using FinanceManager.Domain.TransactionsAggregate;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Transaction> Transactions { get; }
    DbSet<Category> Categories { get; }

    DbSet<BankAccount> BankAccounts { get; }
    DbSet<AccountHolder> AccountHolders { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
