using SocialApp.Abstractions;
using SocialApp.Enums;
using SocialApp.Interfaces;
using SocialApp.Structure;

namespace SocialApp.Pages
{
    public class clsMyFriendsPage : absScrollCursor, INeedAuthentication
    {
        public override string PageName { get; } = "My Friends";
        protected override string EmptyRowsMessage { get; } = "You have no friends";
        public clsAppState AppState { get; }
        public clsServiceCollection Services { get; }

        public override enPermission AccessPermission => enPermission.None;

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
