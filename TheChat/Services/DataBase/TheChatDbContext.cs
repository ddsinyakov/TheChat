using Microsoft.EntityFrameworkCore;
using TheChat.Models.Entities;

namespace TheChat.Services.DataBase
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
