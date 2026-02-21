

using SocialApp.Interfaces;

namespace SocialApp.Controllers
{
    public class PageController
    {
        public PageController(
            INavigationController navigationController,
            IRendererController rendererController,
            IInputController inputController
            )
        {
            NavigationController = navigationController;
            RendererController = rendererController;
            InputController = inputController;
        }

        public INavigationController NavigationController { get; }
        public IRendererController RendererController { get; }
        public IInputController InputController { get; }

        public void Play()
        {
            while (NavigationController.GetStackCount() > 0)
            {
                RendererController.Print();
                InputController.TakeAction(Console.ReadKey().KeyChar);
            }
        }
    }
}
