using SocialApp.Interfaces;
using SocialApp.Services;


namespace SocialApp.Scripts
{
    public class clsRegisterAction : IAction
    {
        public string ActionName { get; } = "Register";
        public clsServiceCollection Services { get; }

        public clsRegisterAction(clsServiceCollection services)
        {
            Services = services;
        }

        public void Execute()
        {
            Console.Clear();
            clsConsoleUI.PrintMessage($"Register Screen");
            string username = clsConsoleInput.GetStringInput("Enter User Name");
            string password = clsConsoleInput.GetStringInput("Enter Password");
            string validateErrorMessage = clsValidation.GetUsernameAndPasswordValidateErrorMessage(username, password);
            Console.Clear();

            if (!string.IsNullOrEmpty(validateErrorMessage))
            {
                clsConsoleUI.PrintMessage(validateErrorMessage);
                clsConsoleUI.PressKeyToContinue();
                return;
            }

            if (!Services.AuthenticationService.Register(username, password))
            {
                clsConsoleUI.PrintMessage("User Already Exists!");
                clsConsoleUI.PressKeyToContinue();
            }

        }

    }
}
