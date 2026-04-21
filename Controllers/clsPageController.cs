

using SocialApp.Enums;
using SocialApp.Forms;
using SocialApp.Interfaces;

namespace SocialApp.Controllers
{
    public class clsPageController
    {
        public clsPageController(
            clsNavigationController navigationController,
            clsMainForm mainForm,
            clsInputController inputController
            )
        {
            NavigationController = navigationController;
            MainForm = mainForm;
            InputController = inputController;
        }

        protected clsNavigationController NavigationController { get; }
        protected clsMainForm MainForm { get; }
        protected clsInputController InputController { get; }

        public void Start()
        {
            while (NavigationController.GetCurrentStackCount() > 0)
            {
                MainForm.Print();
                InputController.TakeAction();
            }
        }

    }
}
