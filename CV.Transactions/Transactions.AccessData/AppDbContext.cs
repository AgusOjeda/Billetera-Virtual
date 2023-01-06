using Microsoft.EntityFrameworkCore;
using Transactions.Domain.Entities;

namespace Transactions.AccessData
{
    public class AppDbContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionState> TransactionStates { get; set; }
        public DbSet<MovementHistory> MovementsHistory { get; set; }
        public DbSet<OperationType> OperationTypes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("Transaction");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnType("UniqueIdentifier");
                entity.Property(e => e.FromAccountId).HasColumnType("UniqueIdentifier").IsRequired();
                entity.Property(e => e.ToAccountId).HasColumnType("UniqueIdentifier").IsRequired();
                entity.Property(e => e.State).HasColumnType("int").IsRequired();
                entity.Property(e => e.OperationTypeId).HasColumnType("int").IsRequired();
                entity.Property(e => e.Reason).HasColumnType("nvarchar(max)").IsRequired();
                entity.Property(e => e.Amount).HasColumnType("decimal(15,2)").IsRequired();
                entity.Property(e => e.DateTime).HasColumnType("DateTime").IsRequired();
                entity.HasOne<TransactionState>(e => e.TransactionState)
                    .WithMany(o => o.Transactions)
                    .HasForeignKey(o => o.State);
                entity.HasOne<OperationType>(e => e.OperationType)
                    .WithMany(o => o.Transactions)
                    .HasForeignKey(o => o.OperationTypeId);

            });

            modelBuilder.Entity<TransactionState>(entity =>
            {
                entity.ToTable("TransactionState");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnType("int").ValueGeneratedOnAdd();
                entity.Property(e => e.Name).HasColumnType("nvarchar(15)").IsRequired();
                entity.HasData(
                       new
                       {
                           Id = 1,
                           Name = "TRX SUCCESS"
                       },
                       new
                       {
                           Id = 2,
                           Name = "TRX REJECTED"
                       },
                       new
                       {
                           Id = 3,
                           Name = "TRX PENDING"
                       });
            });

            modelBuilder.Entity<OperationType>(entity =>
            {
                entity.ToTable("OperationType");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnType("int").ValueGeneratedOnAdd();
                entity.Property(e => e.Name).HasColumnType("nvarchar(max)").IsRequired();
                entity.HasData(
                       new
                       {
                           Id = 1,
                           Name = "TRANSFERENCIA ENTRE CUENTAS DE MISMO TITULAR"
                       },
                       new
                       {
                           Id = 2,
                           Name = "TRANSFERENCIA ENTRE CUENTAS DE DIFERENTE TITULAR"
                       },
                       new
                       {
                           Id = 3,
                           Name = "INGRESO DE DINERO POR VENTANILLA"
                       },
                       new
                       {
                           Id = 4,
                           Name = "EXTRACCION DE DINERO POR VENTANILLA"
                       });
            });

            modelBuilder.Entity<MovementHistory>(entity =>
            {
                entity.ToTable("MovementHistory");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnType("UniqueIdentifier");
                entity.Property(e => e.OperationType).HasColumnType("nvarchar(max)").IsRequired();
                entity.Property(e => e.FromAccountId).HasColumnType("UniqueIdentifier").IsRequired();
                entity.Property(e => e.ToAccountId).HasColumnType("UniqueIdentifier").IsRequired();
                entity.Property(e => e.FromCbu).HasColumnType("nvarchar(22)").IsRequired();
                entity.Property(e => e.ToCbu).HasColumnType("nvarchar(22)").IsRequired();
                entity.Property(e => e.FullNameEmisorCustomer).HasColumnType("nvarchar(max)").IsRequired();
                entity.Property(e => e.DniEmisorCustomer).HasColumnType("nvarchar(10)").IsRequired();
                entity.Property(e => e.CuilEmisorCustomer).HasColumnType("nvarchar(13)").IsRequired();
                entity.Property(e => e.FullNameReceiverCustomer).HasColumnType("nvarchar(max)").IsRequired();
                entity.Property(e => e.DniReceiverCustomer).HasColumnType("nvarchar(10)").IsRequired();
                entity.Property(e => e.CuilReceiverCustomer).HasColumnType("nvarchar(13)").IsRequired();
                entity.Property(e => e.DateTimeTransaction).HasColumnType("DateTime").IsRequired();
                entity.Property(e => e.AmountTransaction).HasColumnType("decimal(15,2)").IsRequired();
                entity.Property(e => e.Currency).HasColumnType("nvarchar(3)").IsRequired();
                entity.Property(e => e.ResultingStateOfTransaction).HasColumnType("nvarchar(max)").IsRequired();

                entity.HasData(
                       new
                       {
                           Id = Guid.NewGuid(),
                           OperationType = "TRANSFERENCIA ENTRE CUENTAS DE DIFERENTE TITULAR",
                           FromAccountId = Guid.NewGuid(),
                           ToAccountId = Guid.NewGuid(),
                           FromCbu = "123456789",
                           ToCbu = "987654321",
                           FullNameEmisorCustomer = "Carlos Franco",
                           DniEmisorCustomer = "33.123.123",
                           CuilEmisorCustomer = "12-33123123-8",
                           FullNameReceiverCustomer = "Luciano Franco",
                           DniReceiverCustomer = "22.123.123",
                           CuilReceiverCustomer = "12-22123123-8",
                           DateTimeTransaction = new DateTime(2022, 9, 24),
                           AmountTransaction = new decimal(10000.50),
                           Currency = "ARS",
                           ResultingStateOfTransaction = "TRX SUCCESS"
                       });
            });
        }
    }
}