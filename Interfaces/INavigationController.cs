using SocialApp.Abstractions;


namespace SocialApp.Interfaces
{
    public interface INavigationController
    {
        void GoNext(AbPage next);
        void GoBack();
        AbPage GetCurrentPage();
        int GetStackCount();
        void SetDefaultAppPage(AbPage homePage);
        void SetDefaultAuthPage(AbPage authenticationPage);
        void ClearStack();
        void ResetStacksToDefault();
        List<string> GetPagesNames();
    }
}
