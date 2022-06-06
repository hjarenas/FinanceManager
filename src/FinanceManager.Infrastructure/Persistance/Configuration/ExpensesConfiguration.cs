using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FinanceManager.Domain.ExpensesAggregate;

namespace FinanceManager.Infrstructure.Persistance.Configuration;
public class ExpensesConfiguration : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.OwnsOne(e => e.BankAccount);
        builder.OwnsOne(e => e.Category);
    }
}