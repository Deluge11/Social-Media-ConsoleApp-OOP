using SocialApp.Abstractions;
using SocialApp.Interfaces;

namespace SocialApp.Controllers
{
    public class NavigationController : INavigationController
    {
        public Stack<AbPage> AuthenticatePageStack { get; } = new();
        public Stack<AbPage> AppPageStack { get; } = new();
        public AppState AppState { get; }

        public NavigationController(AppState AppState)
        {
            this.AppState = AppState;
        }


        public void SetDefaultAppPage(AbPage page)
        {
            AppPageStack.Clear();
            AppPageStack.Push(page);
        }

        public void SetDefaultAuthPage(AbPage page)
        {
            AuthenticatePageStack.Clear();
            AuthenticatePageStack.Push(page);
        }

        public void GoNext(AbPage next)
        {
            if (next == null) return;

            if (AppState.IsAuthenticated)
            {
                AppPageStack.Push(next);
            }
            else
            {
                AuthenticatePageStack.Push(next);
            }
            if(next is AbScrollPage scrollPage)
            {
                scrollPage.ResetStart();
            }
            if (next is AbScrollCursor scrollCursorPage)
            {
                scrollCursorPage.ResetCursor();
            }
        }

        public void GoBack()
        {
            var stack = AppState.IsAuthenticated ? AppPageStack : AuthenticatePageStack;

            if (stack.Count > 0)
            {
                stack.Pop();
            }
        }

        public AbPage GetCurrentPage()
        {
            var stack = AppState.IsAuthenticated ? AppPageStack : AuthenticatePageStack;

            return stack.Count > 0 ? stack.Peek() : null;
        }

        public void ResetStacksToDefault()
        {
            while (AppPageStack.Count > 1)
                AppPageStack.Pop();

            while (AuthenticatePageStack.Count > 1)
                AuthenticatePageStack.Pop();

            if (AppPageStack.Count > 0)
            {
                AbPage page = AppPageStack.Peek();
                if (page is AbScrollPage scrollPage)
                {
                    scrollPage.ResetStart();
                }
                if (page is AbScrollCursor scrollCursorPage)
                {
                    scrollCursorPage.ResetCursor();
                }
            }
            if (AuthenticatePageStack.Count > 0)
            {
                AbPage page = AuthenticatePageStack.Peek();
                if (page is AbScrollPage scrollPage)
                {
                    scrollPage.ResetStart();
                }
                if (page is AbScrollCursor scrollCursorPage)
                {
                    scrollCursorPage.ResetCursor();
                }
            }
        }
        public int GetStackCount()
        {
            return AppState.IsAuthenticated ? AppPageStack.Count : AuthenticatePageStack.Count;
        }

        public void ClearStack()
        {
            var stack = AppState.IsAuthenticated ? AppPageStack : AuthenticatePageStack;
            stack.Clear();
        }
        
        public List<string> GetPagesNames()
        {
            var pagesName = new List<string>();

            var stack = AppState.IsAuthenticated ? AppPageStack : AuthenticatePageStack;

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
