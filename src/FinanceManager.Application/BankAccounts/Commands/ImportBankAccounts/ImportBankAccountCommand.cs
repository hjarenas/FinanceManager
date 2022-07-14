using AutoMapper;
using FinanceManager.Application.Common.Interfaces;
using FinanceManager.Domain.AccountsAggregate;
using MediatR;

namespace FinanceManager.Application.BankAccounts.Commands.ImportBankAccounts;
public record ImportBankAccountCommand(
    string Iban,
    string BankName,
    IEnumerable<CreateAccountHolderDto> AccountHoldersNames)
    : IRequest<BankAccountDto>;

public class ImportBankAccountsCommandHandler : IRequestHandler<ImportBankAccountCommand, BankAccountDto>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public ImportBankAccountsCommandHandler(
        IApplicationDbContext dbContext,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<BankAccountDto> Handle(ImportBankAccountCommand request, CancellationToken cancellationToken)
    {
        var bankAccount = _dbContext.BankAccounts.SingleOrDefault(ba => ba.Iban == request.Iban);
        if (bankAccount is null)
        {
            bankAccount = CreateNewBankAccount(request);
        }
        else
        {
            var newAccountHolders = GetNewAccountHolders(request.AccountHoldersNames, bankAccount);
            bankAccount.AccountHolders.AddRange(newAccountHolders);
        }
        await _dbContext.SaveChangesAsync(cancellationToken);
        return _mapper.Map<BankAccountDto>(bankAccount);
    }

    private BankAccount CreateNewBankAccount(ImportBankAccountCommand request)
    {
        BankAccount? bankAccount;
        var accountHolders = GetOrCreateAccountHolders(request.AccountHoldersNames);
        bankAccount = new BankAccount
        {
            AccountHolders = accountHolders.ToList(),
            Iban = request.Iban,
            BankName = request.BankName
        };
        _dbContext.BankAccounts.Add(bankAccount);
        return bankAccount;
    }

    private static IEnumerable<AccountHolder> GetNewAccountHolders(
        IEnumerable<CreateAccountHolderDto> accountHoldersDtos,
        BankAccount bankAccount)
    {
        var existingAccountHolders = accountHoldersDtos
            .Where(ahd => bankAccount.AccountHolders.Any(ahe => ahd.FirstName == ahe.FirstName && ahd.LastName == ahe.LastName));
        var newAccountHolders = accountHoldersDtos
            .Where(ahd => !existingAccountHolders.Contains(ahd))
            .Select(ah => new AccountHolder
            {
                FirstName = ah.FirstName,
                LastName = ah.LastName,
                BankAccounts = new()
            });
        return newAccountHolders;
    }

    private IEnumerable<AccountHolder> GetOrCreateAccountHolders(IEnumerable<CreateAccountHolderDto> accountHoldersDtos)
    {
        var existingAccountHolders = accountHoldersDtos
            .Select(ahd =>
                _dbContext.AccountHolders
                    .SingleOrDefault(ahe => ahd.FirstName == ahe.FirstName && ahd.LastName == ahe.LastName))
            .Where(ah => ah != null)
            .ToList();
        var newEntities = accountHoldersDtos
            .Where(ahd => !_dbContext.AccountHolders.Any(ahe => ahd.FirstName == ahe.FirstName && ahd.LastName == ahe.LastName))
            .Select(ah => new AccountHolder
            {
                FirstName = ah.FirstName,
                LastName = ah.LastName,
                BankAccounts = new()
            })
            .ToList();
        return newEntities.Union(existingAccountHolders)!;
    }
}
