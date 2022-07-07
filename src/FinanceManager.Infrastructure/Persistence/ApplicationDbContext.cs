using Microsoft.EntityFrameworkCore;
using System.Reflection;
using FinanceManager.Application.Common.Interfaces;
using FinanceManager.Domain.TransactionsAggregate;

namespace FinanceManager.Infrastructure.Persistence;
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(
     DbContextOptions<ApplicationDbContext> options)
     : base(options)
    {
    }
    public DbSet<Transaction> Expenses => Set<Transaction>();

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
