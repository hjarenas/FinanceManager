using System.Reflection;
using FinanceManager.Application.Expenses;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceManager.Application;
public static class RegisterApplication
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<IExpensesService, ExpensesService>();
    }
}