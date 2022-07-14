using FinanceManager.Application.Common.Mappings;
using FinanceManager.Domain.AccountsAggregate;

namespace FinanceManager.Application.BankAccounts;
public record BankAccountDto : IMapFrom<BankAccount>
{
    public Guid Id { get; set; }
    public string Iban { get; set; } = string.Empty;
    public IEnumerable<AccountHolderDto> AccountHolders { get; set; } = new List<AccountHolderDto>();
}
