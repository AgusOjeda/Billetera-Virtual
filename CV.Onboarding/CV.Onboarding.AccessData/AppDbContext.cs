using Microsoft.EntityFrameworkCore;
using CV.Onboarding.Domain.Entities;


namespace CV.Onboarding_AccessData
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Address> Address { get; set; }
        public DbSet<Customer> Customers { get; set; } 
        public DbSet<IdentityVerification> IdentityVerification { get; set; }
        public DbSet<VerificationState> VerificationState { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(entity => entity.Id);
                entity.Property(a => a.Id).ValueGeneratedOnAdd();
                entity.Property(a => a.Number).HasMaxLength(6);
                entity.Property(a => a.Street).HasMaxLength(50);
                entity.Property(a => a.Location).HasMaxLength(50);
                entity.Property(a => a.Province).HasMaxLength(50);
                entity.HasOne(a => a.Customer).WithOne(c => c.Address);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(entity => entity.Id);
                entity.Property(c => c.Id).HasColumnType("uniqueidentifier"); ;
                entity.Property(c => c.Dni).HasMaxLength(10);
                entity.Property(c => c.Cuil).HasMaxLength(13);
                entity.Property(c => c.Phone).HasMaxLength(13);
                entity.HasOne(a => a.Address).WithOne(c => c.Customer).HasForeignKey<Address>(c => c.CustomerId);
            });

            modelBuilder.Entity<IdentityVerification>(entity =>
            {
                entity.HasKey(entity => entity.Id);
                entity.Property(i => i.Id).ValueGeneratedOnAdd();
                entity.Property(i => i.State).HasMaxLength(2);
                entity.HasOne(i => i.VerificationState).WithMany(i => i.IdentityVerifications).HasForeignKey(x=> x.State);
                entity.HasOne(i => i.Customer).WithOne(c => c.IdentityVerification);
            });

            modelBuilder.Entity<VerificationState>(entity =>
            {
                entity.HasKey(entity => entity.Id);
                entity.Property(v => v.Id)
                      .ValueGeneratedOnAdd()
                      .HasMaxLength(2);
                entity.Property(v => v.Name).HasMaxLength(10);
            });
            
            modelBuilder.Entity<VerificationState>().HasData(
                new VerificationState
                {
                    Id = 1,
                    Name= "Verificado"
                },
                new VerificationState
                {
                    Id = 2,
                    Name = "NoVerified"
                }
                );

        }

    }
}
