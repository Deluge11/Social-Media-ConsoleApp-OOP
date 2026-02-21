using SocialApp.Interfaces;
using SocialApp.Services;


namespace SocialApp.Scripts
{
    public class RegisterAction : IAction
    {
        public string ActionName { get; } = "Register";
        public AuthenticationServices AuthenticationServices { get; }

        public RegisterAction(AuthenticationServices authenticationServices)
        {
            AuthenticationServices = authenticationServices;
        }
        public void Action()
        {
            AuthenticationServices.Register();
        }

    }
}
