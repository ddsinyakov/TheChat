using Microsoft.EntityFrameworkCore;
using TheChat.Features;
using TheChat.Models.Entities;
using TheChat.Services.DataBase;

namespace TheChat.Services.Database.RoleDao
{
    public class RoleDao : IRoleDao
    {
        private TheChatDbContext _dbContext { get; init; }

        public RoleDao(TheChatDbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException(nameof(dbContext));

            _dbContext = dbContext;
        }

        public async Task<Role> GetAdminRole()
        {
            Role res = await _dbContext.Roles
                .FirstAsync(role => role.RoleName == RolesDefinitions.Admin);

            return res;
        }

        public async Task<Role> GetCommonUserRole()
        {
            Role res = await _dbContext.Roles
                .FirstAsync(role => role.RoleName == RolesDefinitions.CommonUser);

            return res;
        }

        public async Task<Role?> GetRoleAsync(string roleName)
        {
            if (roleName is null)
                throw new ArgumentNullException(nameof(roleName));

            Role? res = await _dbContext.Roles
                .FirstOrDefaultAsync(role => role.RoleName == roleName);

            return res;
        }
    }
}
