using SocialApp.Abstractions;
using SocialApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Controllers
{
    public class InputController : IInputController
    {
        private INavigationController NavigationController { get; }
        public AppState AppState { get; }

        public InputController(AppState appState, INavigationController navigationController)
        {
            NavigationController = navigationController;
            AppState = appState;
        }

        public void TakeAction(char key)
        {
            AbPage page = NavigationController.GetCurrentPage();

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
            if (page is IAction actionPage && key == 'x')
            {
                actionPage.Action();
            }
            else if (page is IRootPage rootPage && key == 'x')
            {
                NavigationController.GoNext(rootPage.Next());
            }
            if (key == 'b')
            {
                NavigationController.GoBack();
            }
            if (key == 'e')
            {
                NavigationController.ClearStack();
            }
            if (key == 'l')
            {
                AppState.IsAuthenticated = false;
                NavigationController.ResetStacksToDefault();
            }
        }
    }
}
