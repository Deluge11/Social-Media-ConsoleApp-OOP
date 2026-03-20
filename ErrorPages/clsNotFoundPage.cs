using SocialApp.Abstractions;
using SocialApp.Enums;
using SocialApp.Structure;

namespace SocialApp.ErrorPages
{
    public class clsNotFoundPage : absBasePage
    {
        public override string PageName => "Error Page";
        public override enPermission AccessPermission => enPermission.None;

        public override void SetContent()
        {
            ContentGrids[1] = "Error 404";
            ContentGrids[4] = "Page Not Found!";
        }
    }
}
