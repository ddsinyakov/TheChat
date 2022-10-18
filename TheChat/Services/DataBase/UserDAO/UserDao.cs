using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using TheChat.Models.Entities;

namespace TheChat.Services.DataBase.UserDAO
{
    public class UserDao : IUserDao
    {

        // dependance inversion dbContext
        private TheChatDbContext _dbContext { get; init; }
        private DbSet<User> _users { get; init; }

        public UserDao(TheChatDbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException(nameof(dbContext));

            _dbContext = dbContext;
            _users = dbContext.Users;
        }

        public async Task AddUserAsync(User user)
        {
            if (user is null)
                throw new ArgumentNullException(nameof(user));

            await _users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(User user)
        {
            if (user is null)
                throw new ArgumentNullException(nameof(user));

            _dbContext.Remove(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User?> GetUserAsync(Expression<Func<User, bool>> predicate)
        {
            if (predicate is null) 
                throw new ArgumentNullException(nameof(predicate));
            
            User? result = await _users.FirstOrDefaultAsync(predicate);

            return result;
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            User? result = await _users.FindAsync(id);

            return result;
        }

        public async Task UpdateUserAsync(User user)
        {
            _users.Update(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
