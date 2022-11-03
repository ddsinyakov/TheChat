using Microsoft.EntityFrameworkCore;
using TheChat.Features;
using TheChat.Models.Entities;

namespace TheChat.Services.DataBase
{
    public class TheChatDbContext : DbContext
    {

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;

        public TheChatDbContext(DbContextOptions<TheChatDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(new Role[]
            {
                new Role { Id = Guid.NewGuid(), RoleName = RolesDefinitions.Admin },
                new Role { Id = Guid.NewGuid(), RoleName = RolesDefinitions.CommonUser }
            });

            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.Login).IsUnique();
        }
    }
}
