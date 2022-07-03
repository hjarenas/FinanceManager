using Microsoft.EntityFrameworkCore;
using FinanceManager.Domain.ExpensesAggregate;
using System.Reflection;
using FinanceManager.Application.Common.Interfaces;

namespace FinanceManager.Infrastructure.Persistence;
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(
     DbContextOptions<ApplicationDbContext> options)
     : base(options)
    {
    }
    public DbSet<Expense> Expenses => Set<Expense>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }


}
