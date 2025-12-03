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
        public InputController(INavigationController navigationController)
        {
            NavigationController = navigationController;
        }

        public INavigationController NavigationController { get; }



        public void TakeAction(char key)
        {
            IPage page = NavigationController.GetCurrentPage();

            if (page is IScrollPage scrollPage)
            {
                if (key == 'w')
                {
                    scrollPage.ScrollUp();
                    return;
                }
                if (key == 's')
                {
                    scrollPage.ScrollDown();
                    return;
                }
            }
            if (page is IAction actionPage && key == 'x')
            {
                actionPage.Action();
                return;
            }
            if (page is IRootPage rootPage && key == 'x')
            {
                NavigationController.GoNext(rootPage.Next());
                return;
            }

            NavigationController.GoBack();
        }
    }
}
