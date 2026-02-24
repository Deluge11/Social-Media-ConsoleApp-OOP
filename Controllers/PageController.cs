

using SocialApp.Interfaces;

namespace SocialApp.Controllers
{
    public class PageController
    {
        public PageController(
            NavigationController navigationController,
            RendererController rendererController,
            InputController inputController
            )
        {
            NavigationController = navigationController;
            RendererController = rendererController;
            InputController = inputController;
        }

        public NavigationController NavigationController { get; }
        public RendererController RendererController { get; }
        public InputController InputController { get; }

        public void Play()
        {
            while (NavigationController.GetCurrentStackCount() > 0)
            {
                RendererController.Print();
                InputController.TakeAction(Console.ReadKey().KeyChar);
            }
        }
    }
}
