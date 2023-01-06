namespace CV.Onboarding.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public int State { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        //public UserAccountState UserAccountState { get; set; }
        public Customer Customer { get; set; }
    }
}