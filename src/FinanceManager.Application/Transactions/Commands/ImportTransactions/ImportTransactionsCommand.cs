using FinanceManager.Application.BankAccounts.Commands.ImportBankAccounts;
using FinanceManager.Application.Common.Interfaces;
using FinanceManager.Domain.TransactionsAggregate;
using MediatR;

namespace FinanceManager.Application.Transactions.Commands.ImportTransactions;
public record ImportTransactionCommand(
    ImportBankAccountCommand BankAccountInformation,
    DateOnly BookingDate,
    string ThirdParty,
    string Description,
    string IntendedUse,
    Direction Direction,
    double AmountInEur,
    bool IsRecurring,
    IEnumerable<string> Categories
) : IRequest;

public class ImportTransactionsCommandHandler : IRequestHandler<ImportTransactionCommand>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IMediator _mediator;

    public ImportTransactionsCommandHandler(IApplicationDbContext applicationDbContext, IMediator mediator)
    {
        _applicationDbContext = applicationDbContext;
        _mediator = mediator;
    }
    public async Task<Unit> Handle(ImportTransactionCommand request, CancellationToken cancellationToken)
    {
        _ = await GetCategories(request.Categories);
        _ = await _mediator.Send(request.BankAccountInformation, cancellationToken);
        return new Unit();
    }

    private async Task<IEnumerable<Category>> GetCategories(IEnumerable<string> categoryNames)
    {
        var existingCategories = _applicationDbContext.Categories.Where(c => categoryNames.Contains(c.Name));
        var nonExistingCategories = categoryNames
            .Where(cn => existingCategories.Any(c => c.Name == cn))
            .Select(cn => new Category { Name = cn });
        await _applicationDbContext.Categories.AddRangeAsync(nonExistingCategories);
        return existingCategories.Union(nonExistingCategories);
    }
}
