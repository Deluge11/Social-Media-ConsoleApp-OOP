using SocialApp.Enums;
using SocialApp.HelperTools;
using SocialApp.Interfaces.Page;
using SocialApp.Pages.Abstractions;

namespace SocialApp.Controllers
{
    public class clsInputController
    {
        public clsNavigationController NavigationController { get; }

        public clsInputController( clsNavigationController navigationController)
        {
            NavigationController = navigationController;
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
                    HandleExit();
                    return;
            }

        }

        private void HandleExit()
        {
            NavigationController.ClearStack();

            Console.Clear();
            clsConsoleUI.PrintMessage("Program End!");
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

        private void HandleSpecialAction(absBasePage page)
        {
            if (page is IAction actionPage)
            {
                if (clsAppState.User.HasPermission(actionPage.ActionPermission))
                {
                    actionPage.Execute();
                }
                else
                {
                    Console.Clear();
                    clsConsoleUI.PrintMessage($"You don't have permission to execute `{actionPage.ActionName}`");
                    clsConsoleUI.PressKeyToContinue();
                }
            }
            else if (page is IRootPage rootPage)
            {
                absBasePage nextPage = rootPage.Next();

                if (clsAppState.User.HasPermission(nextPage.AccessPermission))
                {
                    NavigationController.PushPageToCurrentStack(nextPage);
                }
                else
                {
                    Console.Clear();
                    clsConsoleUI.PrintMessage($"You don't have permission to access `{nextPage.PageName}`");
                    clsConsoleUI.PressKeyToContinue();
                }
            }
        }

        private void PerformLogout()
        {
            if (clsAppState.IsAuthenticated() || clsAppState.IsGuest)
            {
                clsAppState.Clear();
                NavigationController.ResetNavigation();
            }
        }

    }
}
