

using SocialApp.Enums;
using SocialApp.HelperTools;
using SocialApp.Interfaces;
using SocialApp.Interfaces.Form;

namespace SocialApp.Controllers
{
    public class clsPageController
    {
        public clsPageController(
            IForm mainForm,
            clsInputController inputController,
            clsNavigationController navigationController
            )
        {
            Form = mainForm;
            InputController = inputController;
            NavigationController = navigationController;
        }

        protected IForm Form { get; }
        protected clsInputController InputController { get; }
        protected clsNavigationController NavigationController { get; }

        public void Start()
        {
            Console.Clear();
            clsConsoleUI.PrintMessage("System Loading!");
            Thread.Sleep(2000);

            while (NavigationController.GetCurrentStackCount() > 0)
            {
                Console.SetCursorPosition(0, 0);
                Form.Print();
                InputController.TakeAction();
            }
        }

    }
}
