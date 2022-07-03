using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FinanceManager.Domain.ExpensesAggregate;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FinanceManager.Infrastructure.Persistence.Configuration;
public class ExpensesConfiguration : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        var converter = new EnumToStringConverter<Direction>();
        builder.Property(e => e.Direction)
            .HasConversion(converter);
        builder.OwnsOne(e => e.BankAccount);
        builder.OwnsOne(e => e.Category);
    }
}
