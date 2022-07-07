using SharedKernel.Exceptions;

namespace FinanceManager.Domain.AccountsAggregate;
public class AccountHolders
{
    private string? _firstName;
    private string? _lastName;

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
}
