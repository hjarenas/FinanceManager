using FinanceManager.Domain.ExpensesAggregate;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Expense> Expenses { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
