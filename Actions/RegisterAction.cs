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
            clsConsoleUI.PrintMessage($"Login Screen");
            string username = clsConsoleInput.GetStringInput("Enter User Name");
            string password = clsConsoleInput.GetStringInput("Enter Password");
            
            string validateErrorMessage = clsInputValidation.GetUsernameAndPasswordValidateErrorMessage(username, password);

            if (!string.IsNullOrEmpty(validateErrorMessage))
            {
                clsConsoleUI.PrintMessage(validateErrorMessage);
                clsConsoleUI.PressKeyToContinue();
                return;
            }

            if (AuthenticationServices.Register(username, password))
            {
                clsConsoleUI.PrintMessage("User Already Exists!");
                clsConsoleUI.PressKeyToContinue();
            }

        }

    }
}
