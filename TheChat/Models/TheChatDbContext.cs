using Microsoft.EntityFrameworkCore;
using TheChat.Models.Entities;

namespace TheChat.Models
{
    public class TheChatDbContext : DbContext
    {

        public DbSet<User> Users { get; set; } = null!;

        public TheChatDbContext(DbContextOptions<TheChatDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
