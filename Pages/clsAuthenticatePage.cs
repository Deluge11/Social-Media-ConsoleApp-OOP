

using SocialApp.Abstractions;
using SocialApp.Enums;


namespace SocialApp.Pages
{
    public class clsAuthenticatePage : absActionCollectorPage
    {
        public override string PageName => "Authentication Page";

        public override enPermission AccessPermission => enPermission.None;
        protected override enResetCursor CursorResetCommand => enResetCursor.Up;
    }
}
