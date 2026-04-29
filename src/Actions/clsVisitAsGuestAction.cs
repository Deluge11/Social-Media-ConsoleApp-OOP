
using SocialApp.Enums;
using SocialApp.Interfaces.Page;
using SocialApp.Services;

namespace SocialApp.Actions
{
    public class clsVisitAsGuestAction : IAction
    {
        public string ActionName => "Visit As Guest";
        public enPermission ActionPermission => enPermission.None;

        public void Execute()
        {
            clsAuthenticationServices.RegisterAsGuest();
        }
    }
}
