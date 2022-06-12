using Microsoft.EntityFrameworkCore;
using FinanceManager.Domain.ExpensesAggregate;
using System.Reflection;
using CleanArchitecture.Application.Common.Interfaces;

namespace FinanceManager.Infrstructure.Persistence;
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(
     DbContextOptions<ApplicationDbContext> options)
     : base(options)
    {
    }
    public DbSet<Expense> Expenses => Set<Expense>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }


}
