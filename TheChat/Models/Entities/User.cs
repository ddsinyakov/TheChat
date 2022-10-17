namespace TheChat.Models.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public String FirstName { get; set; } = null!;
        public String SecondName { get; set; } = null!;
        public String Login { get; set; } = null!;
        public String Email { get; set; } = null!;

        public String Password { get; set; } = null!;
        public String Salt { get; set; } = String.Empty;

        public DateTime CreatedDate { get; set; }
    }
}
