namespace FinanceManager.Domain.ExpensesAggregate;
public class Expense
{
    public Expense(
        DateOnly bookingDate, 
        string recipient, 
        string description, 
        string intendedUse, 
        Direction direction, 
        decimal amountInEur, 
        Category category, 
        BankAccount bankAccount)
        =>
        (BookingDate, Recipient, Description, IntendedUse, Direction, AmountInEur, Category, BankAccount)
        = (bookingDate, recipient, description, intendedUse, direction, amountInEur, category, bankAccount);
    public Guid? ExpenseId {get; init;}
    public DateOnly BookingDate { get; set; }
    public string Recipient { get; set; }
    public string Description { get; set; }
    /// <summary>
    /// Verwendungszweck
    /// </summary>
    public string IntendedUse { get; set; }
    public Direction Direction { get; set; }
    public decimal AmountInEur { get; set; }
    public Category Category { get; set; }

    public BankAccount BankAccount {get; set; }
}
