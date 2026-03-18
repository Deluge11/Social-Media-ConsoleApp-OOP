using SocialApp.Enums;
using SocialApp.HelperTools;
using SocialApp.Interfaces;


namespace SocialApp.Scripts
{
    public class clsLoginAction : IAction
    {
        public string ActionName { get; } = "Login";
        public clsServiceCollection Services { get; }

        public enPermission ActionPermission => enPermission.None;

        public clsLoginAction(clsServiceCollection services)
        {
            Services = services;
        }

        public void Execute()
        {
            Console.Clear();
            clsConsoleUI.PrintMessage($"Login Screen");
            string username = clsConsoleInput.GetStringInput("Enter User Name");
            string password = clsConsoleInput.GetStringInput("Enter Password");
            Console.Clear();

            if (Services.AuthenticationService.Login(username, password))
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
