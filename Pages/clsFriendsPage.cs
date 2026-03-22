

using SocialApp.Abstractions;
using SocialApp.Enums;

namespace SocialApp.Pages
{
    public class clsFriendsPage : absPageCollectorPage
    {
        public override string PageName { get; } = "Friends Page";

        public override enPermission AccessPermission => enPermission.None;
        protected override enResetCursor CursorResetCommand => enResetCursor.Up;
    }
}
