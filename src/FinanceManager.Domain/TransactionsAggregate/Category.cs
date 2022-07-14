using FinanceManager.Domain.Common;

namespace FinanceManager.Domain.TransactionsAggregate;
public class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;
}
