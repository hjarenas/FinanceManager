using FinanceManager.Application.Common.Mappings;
using FinanceManager.Domain.AccountsAggregate;

namespace FinanceManager.Application.BankAccounts;
public record AccountHolderDto : IMapFrom<AccountHolder>
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}
