using Microsoft.EntityFrameworkCore;
using CV.MsAccount.Domain.Entities;

namespace CV.MsAccount.AccessData.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {

        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<AccountState> AccountStates { get; set; } = null!;
        public virtual DbSet<Currency> Currencies { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(account => {
                account.ToTable("Account");
                account.HasKey(a => a.AccountId);
                account.Property(a => a.AccountId).ValueGeneratedOnAdd();
                account.Property(a => a.Alias).HasMaxLength(27);
                account.Property(a => a.Cbu).HasMaxLength(22);
                account.Property(a => a.Balance).HasColumnType("decimal").HasPrecision(15, 2);

                account.HasOne<Currency>(a => a.Currency)
                .WithMany(cu => cu.Accounts)
                .HasForeignKey(a => a.CurrencyId);

                account.HasOne<AccountState>(a => a.AccountState)
                 .WithMany(s => s.Accounts)
                 .HasForeignKey(a => a.AccountStateId);
            });

            modelBuilder.Entity<AccountState>(currency => {
                currency.ToTable("Account State");
                currency.HasKey(a => a.AccountStateId);
                currency.Property(a => a.AccountStateId).ValueGeneratedOnAdd();
                currency.Property(e => e.Name).HasMaxLength(15);
            });

            modelBuilder.Entity<Currency>(currency => {
                currency.ToTable("Currency");
                currency.HasKey(c => c.CurrencyId);
                currency.Property(c => c.CurrencyId).ValueGeneratedOnAdd();
                currency.Property(e => e.Name).HasMaxLength(3);
            });

        }
    }
}