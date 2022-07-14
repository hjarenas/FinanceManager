using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using FinanceManager.Domain.TransactionsAggregate;

namespace FinanceManager.Infrastructure.Persistence.Configuration;
public class TransactionsConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        var converter = new EnumToStringConverter<Direction>();
        builder.Property(e => e.Direction)
            .HasConversion(converter);
    }
}
