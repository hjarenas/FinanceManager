using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceManager.Infrastructure.Persistence.Migrations;

public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder) => migrationBuilder.CreateTable(
            name: "Expenses",
            columns: table => new
            {
                ExpenseId = table.Column<Guid>(type: "uuid", nullable: false),
                BookingDate = table.Column<DateOnly>(type: "date", nullable: false),
                Recipient = table.Column<string>(type: "text", nullable: false),
                Description = table.Column<string>(type: "text", nullable: false),
                IntendedUse = table.Column<string>(type: "text", nullable: false),
                Direction = table.Column<int>(type: "integer", nullable: false),
                AmountInEur = table.Column<decimal>(type: "numeric", nullable: false),
                Category_Name = table.Column<string>(type: "text", nullable: false),
                BankAccount_BankName = table.Column<string>(type: "text", nullable: false),
                BankAccount_Isin = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Expenses", x => x.ExpenseId);
            });

    protected override void Down(MigrationBuilder migrationBuilder) => migrationBuilder.DropTable(
            name: "Expenses");
}
