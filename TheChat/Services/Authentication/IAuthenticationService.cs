using TheChat.Features;
using TheChat.Models.Entities;

namespace TheChat.Services.Authentication
{
    public interface IAuthenticationService
    {
        void Authenticate(User toAuthenticate);
        void LogOut();
    }
}
