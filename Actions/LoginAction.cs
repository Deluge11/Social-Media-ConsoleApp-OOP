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
            clsConsoleUI.PrintMessage($"Login Screen");
            string username = clsConsoleInput.GetStringInput("Enter User Name");
            string password = clsConsoleInput.GetStringInput("Enter Password");

            if (AuthenticationServices.Login(username, password))
            {
                clsConsoleUI.PrintMessage($"Welcome {username}");
                clsConsoleUI.PressKeyToContinue();
            }
            else
            {
                clsConsoleUI.PrintMessage("Username or Password is invalid");
                clsConsoleUI.PressKeyToContinue();
            }
        }

    }
}
