using SocialApp.Abstractions;
using SocialApp.Interfaces;
using SocialApp.Services;


namespace SocialApp.Controllers
{
    public class InputController
    {
        public NavigationController NavigationController { get; }
        public AuthenticationServices AuthenticationService { get; }
        public AppState AppState { get; }

        public InputController(AppState appState, NavigationController navigationController, AuthenticationServices authenticationService)
        {
            NavigationController = navigationController;
            AuthenticationService = authenticationService;
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
            if (key == 'x')
            {
                if (page is IAction actionPage)
                {
                    actionPage.Action();
                }
                else if (page is IRootPage rootPage)
                {
                    NavigationController.GoNext(rootPage.Next());
                }
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
                AuthenticationService.Logout();
                NavigationController.ResetStacksToDefault();
            }
        }
    }
}
