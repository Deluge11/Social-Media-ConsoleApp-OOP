using SocialApp.Abstractions;
using SocialApp.Enums;
using SocialApp.ErrorPages;
using SocialApp.Interfaces;
using SocialApp.Services;


namespace SocialApp.Controllers
{
    public class clsInputController
    {
        public clsNavigationController NavigationController { get; }
        public clsServiceCollection Services { get; }
        public clsAppState AppState { get; }

        public clsInputController(clsAppState appState, clsNavigationController navigationController, clsServiceCollection services)
        {
            NavigationController = navigationController;
            Services = services;
            AppState = appState;
        }

        public void TakeAction()
        {
            var page = NavigationController.GetCurrentPage();

            switch (GetCommand())
            {
                case enCommand.ScrollUp:
                    if (page is absScrollPage sUp) sUp.ScrollUp();
                    return;

                case enCommand.ScrollDown:
                    if (page is absScrollPage sDown) sDown.ScrollDown();
                    return;

                case enCommand.SpecialAction:
                    HandleSpecialAction(page);
                    return;

                case enCommand.Logout:
                    PerformLogout();
                    return;

                case enCommand.Return:
                    NavigationController.PopPageFromCurrentStack();
                    return;

                case enCommand.Exit:
                    NavigationController.ClearStack();
                    return;
            }

        }

        private enCommand GetCommand()
        {
            while (true)
            {
                char keyChar = Console.ReadKey(intercept: true).KeyChar;

                if (Enum.IsDefined(typeof(enCommand), (int)keyChar))
                {
                    return (enCommand)keyChar;
                }
            }
        }

        private void HandleSpecialAction(absPage page)
        {
            if (page is IAction actionPage)
                actionPage.Execute();
            else if (page is IRootPage rootPage)
                NavigationController.PushPageToCurrentStack(rootPage.Next());
        }

        private void PerformLogout()
        {
            if (AppState.IsAuthenticated() || AppState.IsGuest)
            {
                AppState.Clear();
                NavigationController.ResetNavigation();
            }
        }
    }
}
