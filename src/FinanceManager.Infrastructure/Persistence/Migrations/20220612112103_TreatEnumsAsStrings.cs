using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceManager.Infrastructure.Persistence.Migrations;

public partial class TreatEnumsAsStrings : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder) => migrationBuilder.AlterColumn<string>(
            name: "Direction",
            table: "Expenses",
            type: "text",
            nullable: false,
            oldClrType: typeof(int),
            oldType: "integer");

    protected override void Down(MigrationBuilder migrationBuilder) => migrationBuilder.AlterColumn<int>(
            name: "Direction",
            table: "Expenses",
            type: "integer",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "text");
}
