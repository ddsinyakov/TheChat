using System.Linq.Expressions;
using TheChat.Models.Entities;

namespace TheChat.Services.DataBase.UserDAO
{
    public interface IUserDao
    {
        Task<User?> GetUserByIdAsync(Guid id);
        Task<User?> GetUserAsync(Expression<Func<User, Boolean>> predicate);

        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(User user);
    }
}
