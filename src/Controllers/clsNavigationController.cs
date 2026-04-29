using SocialApp.Interfaces;
using SocialApp.Pages;
using SocialApp.Pages.Abstractions;

namespace SocialApp.Controllers
{
    public class clsNavigationController
    {
        protected Stack<absBasePage> _authenticatePageStack { get; } = new();
        protected Stack<absBasePage> _appPageStack { get; } = new();
        

        public void SetMainPage(absBasePage page)
        {
            SetStackDefaultPage(_appPageStack, page);

        }

        public void SetAuthenticationPage(absBasePage page)
        {
            SetStackDefaultPage(_authenticatePageStack, page);
        }

        public void SetStackDefaultPage(Stack<absBasePage> stack, absBasePage page)
        { 
            stack.Clear();
            stack.Push(page);
            ResetPagePointers(page);
        }

        public void PushPageToCurrentStack(absBasePage nextPage)
        {
            var targetPage = nextPage ?? new clsNotFoundPage();

            targetPage = ResolveAuthorizedPage(targetPage);

            ExecuteNavigation(targetPage);

            ResetPagePointers(targetPage);
        }

        private absBasePage ResolveAuthorizedPage(absBasePage page)
        {
            if (page is INeedAuthentication && !clsAppState.IsAuthenticated())
            {
                return new clsNotAuthenticatedPage();
            }
            return page;
        }

        private void ExecuteNavigation(absBasePage page)
        {
            GetCurrentStack().Push(page);
        }

        private void ResetPagePointers(absBasePage page)
        {
            if (page is absScrollPage scrollPage)
            {
                scrollPage.ResetCursors();
            }
        }

        public absBasePage PopPageFromCurrentStack()
        {
            return GetCurrentStack().Count > 1 ?
                GetCurrentStack().Pop() : null!;
        }

        public absBasePage GetCurrentPage()
        {
            return GetCurrentStack().Count > 0 ?
                GetCurrentStack().Peek() : null!;
        }

        protected Stack<absBasePage> GetCurrentStack()
        {
            return clsAppState.IsAuthenticated() || clsAppState.IsGuest ?
                _appPageStack : _authenticatePageStack;
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
            return GetCurrentStack().Select(p => p.PageName).Reverse().ToList();
        }

        public void ResetNavigation()
        {
            ResetStack(_appPageStack);
            ResetStack(_authenticatePageStack);
        }

        private void ResetStack(Stack<absBasePage> stack)
        {
            while (stack.Count > 1)
            {
                stack.Pop();
            }

            if (stack.Count > 0)
            {
                ResetPagePointers(stack.Peek());
            }
        }
    }
}
