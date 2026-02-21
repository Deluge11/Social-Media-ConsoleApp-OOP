using SocialApp.Interfaces;
using SocialApp.Services;


namespace SocialApp.Scripts
{
    public class LoginAction : IAction
    {
        public string ActionName { get; } = "Login";
        public AuthenticationServices AuthenticationServices { get; }

        public LoginAction(AuthenticationServices authenticationServices)
        {
            AuthenticationServices = authenticationServices;
        }
        public void Action()
        {
            AuthenticationServices.Login();
        }

    }
}
