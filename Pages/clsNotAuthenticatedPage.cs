using SocialApp.Enums;
using SocialApp.Pages.Abstractions;
using SocialApp.Structure;

namespace SocialApp.Pages
{
    public class clsNotAuthenticatedPage : absBasePage
    {
        public override string PageName => "Error Page";
        public override enPermission AccessPermission => enPermission.None;

        public override void SetContent()
        {
            ContentGrids[1] = "Error 401";
            ContentGrids[4] = "You Are Not Authenticated!";
            ContentGrids[7] = $"You Have To{clsCustomTags.LineBreak}Sign Up / Login";
        }
    }
}
