using FinanceManager.Domain.Common;
using SharedKernel.Exceptions;

namespace FinanceManager.Domain.AccountsAggregate;
public class AccountHolder : BaseEntity
{
    private string? _firstName;
    private string? _lastName;
    private List<BankAccount>? _bankAccounts;

    public string FirstName
    {
        get => _firstName ?? throw new UninitializedPropertyException();
        set => _firstName = value;
    }
    public string LastName
    {
        get => _lastName ?? throw new UninitializedPropertyException();
        set => _lastName = value;
    }

    public List<BankAccount> BankAccounts
    {
        get => _bankAccounts ?? throw new UninitializedPropertyException();
        set => _bankAccounts = value;
    }
}
