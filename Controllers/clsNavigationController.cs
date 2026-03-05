using SocialApp.Abstractions;
using SocialApp.Interfaces;
using SocialApp.ErrorPages;

namespace SocialApp.Controllers
{
    public class clsNavigationController
    {
        protected Stack<absPage> _authenticatePageStack { get; } = new();
        protected Stack<absPage> _appPageStack { get; } = new();
        protected clsAppState _appState { get; }

        public clsNavigationController(clsAppState appState)
        {
            _appState = appState;
        }


        public void SetMainPage(absPage page)
        {
            SetStackDefaultPage(_appPageStack, page);

        }

        public void SetAuthenticationPage(absPage page)
        {
            SetStackDefaultPage(_authenticatePageStack, page);
        }

        public void SetStackDefaultPage(Stack<absPage> stack, absPage page)
        {
            stack.Clear();
            stack.Push(page);
            ResetPagePointers(page);
        }

        public void PushPageToCurrentStack(absPage nextPage)
        {
            var targetPage = nextPage ?? new clsNotFoundPage();

            targetPage = ResolveAuthorizedPage(targetPage);

            ExecuteNavigation(targetPage);

            ResetPagePointers(targetPage);
        }

        private absPage ResolveAuthorizedPage(absPage page)
        {
            if (page is INeedAuthentication && !_appState.IsAuthenticated())
            {
                return new clsNotAuthenticatedPage();
            }
            return page;
        }

        private void ExecuteNavigation(absPage page)
        {
            GetCurrentStack().Push(page);
        }

        private void ResetPagePointers(absPage page)
        {
            if (page is absScrollPage scrollPage)
            {
                scrollPage.Reset();
            }
        }

        public absPage PopPageFromCurrentStack()
        {
            return GetCurrentStack().Count > 0 ?
                GetCurrentStack().Pop() : null!;
        }

        protected Stack<absPage> GetCurrentStack()
        {
            return _appState.IsAuthenticated() || _appState.IsGuest ?
                _appPageStack : _authenticatePageStack;
        }

        public absPage GetCurrentPage()
        {
            return GetCurrentStack().Count > 0 ?
                GetCurrentStack().Peek() : new clsNotFoundPage();
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
            return GetCurrentStack().Select(p => p.PageName).ToList();
        }

        public void ResetNavigation()
        {
            ResetStack(_appPageStack);
            ResetStack(_authenticatePageStack);
        }

        private void ResetStack(Stack<absPage> stack)
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
