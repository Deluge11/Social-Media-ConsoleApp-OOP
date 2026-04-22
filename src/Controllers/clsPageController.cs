

using SocialApp.Enums;
using SocialApp.Forms;
using SocialApp.Interfaces;

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
            while (NavigationController.GetCurrentStackCount() > 0)
            {
                Form.Print();
                InputController.TakeAction();
            }
        }

    }
}
