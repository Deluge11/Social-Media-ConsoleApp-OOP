using SocialApp.Abstractions;
using SocialApp.Enums;


namespace SocialApp.Pages
{
    public class clsPostsPage : absPageCollectorPage
    {
        public override string PageName { get; } = "Posts Page";

        public override enPermission AccessPermission => enPermission.None;
        protected override enResetCursor CursorResetCommand => enResetCursor.Up;
    }
}
