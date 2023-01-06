namespace CV.Authentication.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        public string? VerificationToken { get; set; }
        public DateTime? VerifiedAt { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
        public int State { get; set; }
        public UserAccountState UserAccountState { get; set; }
    }
}