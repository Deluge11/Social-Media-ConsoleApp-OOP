using SocialApp.Enums;
using SocialApp.Interfaces;
using SocialApp.Pages.Abstractions;
using SocialApp.Structure;

namespace SocialApp.Pages
{
    public class clsMyFriendsPage : absScrollPage, INeedAuthentication
    {
        public override string PageName { get; } = "My Friends";
        protected override string EmptyRowsMessage { get; } = "You have no friends";

        public clsAppState AppState { get; }
        public clsServiceCollection Services { get; }

        public override enPermission AccessPermission => enPermission.None;
        protected override enResetCursor CursorResetCommand => enResetCursor.Up;

        public clsMyFriendsPage(clsAppState appState, clsServiceCollection services)
        {
            AppState = appState;
            Services = services;
        }

        protected override List<stPageRow> GetContentRows()
        {
            return Services.FriendService
                .GetUserFriends(AppState.User.Name)
                .Select(n => new stPageRow(n))
                .ToList();
        }

        protected override stPageRow GetHeaderRow()
        {
            return new stPageRow(centerContent: PageName);
        }
    }
}
