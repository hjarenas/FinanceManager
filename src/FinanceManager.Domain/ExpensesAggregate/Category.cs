namespace FinanceManager.Domain.ExpensesAggregate;
public class Category
{
    public Category(string name) => (Name) = (name);
    public string Name { get; set; }
    public Category? SubCategory { get; init; }
}
