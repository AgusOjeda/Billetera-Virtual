using Microsoft.EntityFrameworkCore;
using CV.Authentication.Domain.Entities;

namespace CV.Authentication.AccessData
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<UserAccountState> UserAccountState { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity => 
            {
                entity.ToTable("User");
                entity.HasKey(e => e.Id);
                entity.Property(u => u.Id)
                    .HasDefaultValueSql("NEWID()")
                    .HasColumnName("Id")
                    .HasColumnType("uniqueidentifier");
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("Email")
                    .HasColumnType("nvarchar")
                    .HasMaxLength(50);
                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasColumnName("PasswordHash")
                    .HasColumnType("varbinary(max)");
                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasColumnName("PasswordSalt")
                    .HasColumnType("varbinary(max)");
                entity.Property(e => e.VerificationToken)
                    .IsRequired()
                    .HasColumnName("VerificationCode")
                    .HasColumnType("nvarchar")
                    .HasMaxLength(8);
                entity.Property(e => e.VerifiedAt)
                    .HasColumnName("VerifiedAt")
                    .HasColumnType("datetime");
                entity.Property(u => u.PasswordResetToken)
                    .HasColumnName("ResetCode")
                    .HasColumnType("nvarchar")
                    .HasMaxLength(8);
                entity.Property(u => u.ResetTokenExpires)
                    .HasColumnName("ResetTokenExpires")
                    .HasColumnType("datetime");
                entity.Property(u => u.State)
                    .IsRequired()
                    .HasColumnName("State")
                    .HasColumnType("int")
                    .HasMaxLength(2)
                    .HasDefaultValue(4);                

                entity.HasOne<UserAccountState>(u => u.UserAccountState)
                    .WithMany(userState => userState.Users)
                    .HasForeignKey(u => u.State);

            });
            modelBuilder.Entity<UserAccountState>(entity =>
            {
                entity.ToTable("UserAccountState");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Id")
                    .HasColumnType("int")
                    .HasMaxLength(2);
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("Description")
                    .HasColumnType("nvarchar")
                    .HasMaxLength(50);
            });

            //modelBuilder.Entity<User>().HasData(
            //    new User
            //    {
            //        Id = new Guid("c0a80121-7001-eb11-a812-002248001e4c"),
            //        Password = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
            //        Email = "agustinojeda20@gmail.com",
            //        EmailConfirmed = false,
            //        State = 4
            //    }
            //    );

            modelBuilder.Entity<UserAccountState>().HasData(
                new UserAccountState { Id = 1, Description = "Active" },
                new UserAccountState { Id = 2, Description = "Inactive" },
                new UserAccountState { Id = 3, Description = "Blocked" },
                new UserAccountState { Id = 4, Description = "Customer registration in progress" }
            );
        }
        
    }
}