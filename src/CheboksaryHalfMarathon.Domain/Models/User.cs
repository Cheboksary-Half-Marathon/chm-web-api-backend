namespace CheboksaryHalfMarathon.Domain.Models
{
    public class User
    {
        public int UserId { get; private set; }
        public string UserEmail { get; private set; }
        public string UserPassword { get; private set; }
        public string UserRole { get; private set; }
        public int UserVersion { get; private set; }
        public DateTimeOffset UserRegistrationDate { get; private set; }

        public static User Save(
            string userEmail,
            string userPassword,
            string userRole)
        {
            return new User()
            {
                UserEmail = userEmail,
                UserPassword = userPassword,
                UserRole = userRole,
                UserVersion = 1,
                UserRegistrationDate = DateTimeOffset.UtcNow
            };
        }
    }
}
