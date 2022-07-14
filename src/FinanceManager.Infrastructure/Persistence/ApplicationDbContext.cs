using Microsoft.EntityFrameworkCore;
using System.Reflection;
using FinanceManager.Application.Common.Interfaces;
using FinanceManager.Domain.TransactionsAggregate;
using FinanceManager.Domain.AccountsAggregate;

namespace FinanceManager.Infrastructure.Persistence;
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(
     DbContextOptions<ApplicationDbContext> options)
     : base(options)
    {
    }

    public DbSet<Transaction> Transactions => Set<Transaction>();

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<BankAccount> BankAccounts => Set<BankAccount>();

    public DbSet<AccountHolder> AccountHolders => Set<AccountHolder>();

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
