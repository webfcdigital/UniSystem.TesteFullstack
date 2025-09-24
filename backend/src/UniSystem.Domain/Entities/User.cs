namespace UniSystem.Domain.Entities
{
    public class User
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }

        public User(string name, string email, string passwordHash)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
        }
        private User() { }
    }
}