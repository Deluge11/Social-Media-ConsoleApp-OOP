using SocialApp.Abstractions;
using SocialApp.Structure;

namespace SocialApp.ErrorPages
{
    public class clsNotFoundPage : absPage
    {
        public override string PageName => "Error 404";

        protected override stPageRow GetPageHeader()
        {
            return new stPageRow(centerContent: PageName);
        }

        protected override void SetPageBody()
        {
            ContentGrids[4] = "Page Not Found!";
        }
    }
}
