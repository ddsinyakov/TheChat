using TheChat.Models.Entities;

namespace TheChat.Services.Database.RoleDao
{
    public interface IRoleDao
    {
        Task<Role?> GetRoleAsync(String roleName);
        Task<Role> GetAdminRole();
        Task<Role> GetCommonUserRole();
    }
}
