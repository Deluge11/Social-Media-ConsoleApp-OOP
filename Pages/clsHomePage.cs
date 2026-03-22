

using SocialApp.Abstractions;
using SocialApp.Enums;

namespace SocialApp.Pages
{
    public class clsHomePage : absPageCollectorPage
    {
        public override string PageName { get; } = "Home Page";

        public override enPermission AccessPermission => enPermission.None;
        protected override enResetCursor CursorResetCommand => enResetCursor.Up;
    }
}
