using SocialApp.Abstractions;
using SocialApp.Structure;

namespace SocialApp.ErrorPages
{
    public class clsNotAuthenticatedPage : absPage
    {
        public override string PageName => "Error 401";

        protected override stPageRow GetPageHeader()
        {
            return new stPageRow(centerContent: PageName);
        }

        protected override void SetPageBody()
        {
            ContentGrids[4] = "You Are Not Authenticated!";
            ContentGrids[7] = $"You Have To{clsCustomTags.LineBreak}Sign Up / Login";
        }
    }
}
