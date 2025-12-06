using SocialApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Controllers
{
    public class NavigationController : INavigationController
    {
        private Stack<IPage> AuthenticatePageStack { get; } = new();
        private Stack<IPage> AppPageStack { get; } = new();
        public AppState Appstate { get; }

        public NavigationController(AppState appstate)
        {
            Appstate = appstate;
        }
        public void SetDefaultAppPage(IPage page)
        {
            AppPageStack.Clear();
            AppPageStack.Push(page);
        }
        public void SetDefaultAuthPage(IPage page)
        {
            AuthenticatePageStack.Clear();
            AuthenticatePageStack.Push(page);
        }

        public void GoNext(IPage next)
        {
            if (next == null) return;

            if (Appstate.IsAuthenticated)
            {
                AppPageStack.Push(next);
            }
            else
            {
                AuthenticatePageStack.Push(next);
            }
            if(next is IScrollPage scrollPage)
            {
                scrollPage.ResetStart();
            }
            if (next is IScrollCursor scrollCursorPage)
            {
                scrollCursorPage.ResetCursor();
            }
        }
        public void GoBack()
        {
            var stack = Appstate.IsAuthenticated ? AppPageStack : AuthenticatePageStack;

            if (stack.Count > 0)
            {
                stack.Pop();
            }
        }
        public IPage GetCurrentPage()
        {
            var stack = Appstate.IsAuthenticated ? AppPageStack : AuthenticatePageStack;

            return stack.Count > 0 ? stack.Peek() : null;
        }

        public int GetStackCount()
        {
            return Appstate.IsAuthenticated ? AppPageStack.Count : AuthenticatePageStack.Count;
        }

        public void ClearStack()
        {
            var stack = Appstate.IsAuthenticated ? AppPageStack : AuthenticatePageStack;
            stack.Clear();
        }

        public List<string> GetPagesStackNames()
        {
            var pagesName = new List<string>();

            var stack = Appstate.IsAuthenticated ? AppPageStack : AuthenticatePageStack;

            if (stack.Count == 0)
            {
                return pagesName;
            }

            foreach(var page in stack)
            {
                pagesName.Add(page.PageName);
            }
            return pagesName;
        }
    }
}
