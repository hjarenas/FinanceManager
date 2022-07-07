using FinanceManager.Domain.TransactionsAggregate;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Transaction> Expenses { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
