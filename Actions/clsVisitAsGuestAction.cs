
using SocialApp.Interfaces;

namespace SocialApp.Actions
{
    public class clsVisitAsGuestAction : IAction
    {
        public string ActionName => "Visit As Guest";

        public clsServiceCollection Services { get; }

        public clsVisitAsGuestAction(clsServiceCollection services)
        {
            Services = services;
        }

        public void Execute()
        {
            Services.AuthenticationService.RegisterAsGuest();
        }
    }
}
