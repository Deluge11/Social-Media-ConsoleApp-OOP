using SocialApp.Enums;
using SocialApp.Pages.Abstractions;
using SocialApp.Structure;

namespace SocialApp.Pages
{
    public class clsNotFoundPage : absBasePage
    {
        public override string PageName => "Error Page";
        public override enPermission AccessPermission => enPermission.None;

        public override void SetContent()
        {
            ContentStrings[1] = "Error 404";
            ContentStrings[4] = "Page Not Found!";
        }
    }
}
