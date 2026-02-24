using SocialApp.Abstractions;
using SocialApp.Interfaces;


namespace SocialApp.Controllers
{
    public class InputController : IInputController
    {
        public INavigationController NavigationController { get; }
        public AppState AppState { get; }

        public InputController(AppState appState, INavigationController navigationController)
        {
            NavigationController = navigationController;
            AppState = appState;
        }

        public void TakeAction(char key)
        {
            AbPage page = NavigationController.GetCurrentPage();


            // Scrolling Pages
            if (page is AbScrollPage scrollPage)
            {
                if (key == 'w')
                {
                    scrollPage.ScrollUp();
                }
                if (key == 's')
                {
                    scrollPage.ScrollDown();
                }
            }

            // Action Page -> Action Behavior | Root Page -> Go Next Page
            if (page is IAction actionPage && key == 'x')
            {
                actionPage.Action();
            }
            else if (page is IRootPage rootPage && key == 'x')
            {
                NavigationController.GoNext(rootPage.Next());
            }

            // Return To Previous Page
            if (NavigationController.GetCurrentStackCount() > 1 && key == 'b') 
            {
                NavigationController.GoBack();
            }

            // Exit
            if (key == 'e') 
            {
                NavigationController.ClearStack();
            }

            // Logout
            if (AppState.IsAuthenticated && key == 'l') 
            {
                AppState.IsAuthenticated = false;
                NavigationController.ResetStacksToDefault();
            }
        }
    }
}
