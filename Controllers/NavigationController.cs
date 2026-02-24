using SocialApp.Abstractions;
using SocialApp.Interfaces;
using SocialApp.Pages;

namespace SocialApp.Controllers
{
    public class NavigationController
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
            next = next != null ? 
                next : new NotFoundPage();

            GetCurrentStack().Push(next);

            if (next is AbScrollPage scrollPage)
                scrollPage.Reset();

        }

        public void GoBack()
        {
            if (GetCurrentStack().Count > 0)
                GetCurrentStack().Pop();
        }

        protected Stack<AbPage> GetCurrentStack()
        {
            return AppState.IsAuthenticated ?
                AppPageStack : AuthenticatePageStack;
        }

        public AbPage GetCurrentPage()
        {
            return GetCurrentStack().Count > 0 ?
                GetCurrentStack().Peek() : new NotFoundPage();
        }

        public void ResetStacksToDefault()
        {
            while (AppPageStack.Count > 1)
            {
                AppPageStack.Pop();
            }
            while (AuthenticatePageStack.Count > 1)
            {
                AuthenticatePageStack.Pop();
            }

            if (AppPageStack.Count > 0)
            {
                if (AppPageStack.Peek() is AbScrollPage scrollPage)
                    scrollPage.Reset();
            }
            if (AuthenticatePageStack.Count > 0)
            {
                if (AuthenticatePageStack.Peek() is AbScrollPage scrollPage)
                    scrollPage.Reset();
            }
        }
        public int GetCurrentStackCount()
        {
            return GetCurrentStack().Count;
        }

        public void ClearStack()
        {
            GetCurrentStack().Clear();
        }

        public List<string> GetPagesNames()
        {
            var pagesName = new List<string>();

            var stack = GetCurrentStack();

            if (stack.Count == 0)
            {
                return pagesName;
            }

            foreach (var page in stack)
            {
                pagesName.Add(page.PageName);
            }
            return pagesName;
        }
    }
}
