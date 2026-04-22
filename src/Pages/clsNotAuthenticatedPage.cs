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
            ContentStrings[1] = "Error 401";
            ContentStrings[4] = "You Are Not Authenticated!";
            ContentStrings[7] = $"You Have To{clsCustomTags.LineBreak}Sign Up / Login";
        }
    }
}
