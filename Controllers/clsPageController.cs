

using SocialApp.Enums;
using SocialApp.Forms;
using SocialApp.Interfaces;

namespace SocialApp.Controllers
{
    public class clsPageController
    {
        public clsPageController(
            clsNavigationController navigationController,
            clsMainForm rendererController,
            clsInputController inputController
            )
        {
            NavigationController = navigationController;
            RendererController = rendererController;
            InputController = inputController;
        }

        protected clsNavigationController NavigationController { get; }
        protected clsMainForm RendererController { get; }
        protected clsInputController InputController { get; }

        public void Start()
        {
            while (NavigationController.GetCurrentStackCount() > 0)
            {
                RendererController.Print();
                InputController.TakeAction();
            }
        }

    }
}
