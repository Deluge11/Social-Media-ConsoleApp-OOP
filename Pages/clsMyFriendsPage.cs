using SocialApp.Abstractions;
using SocialApp.Interfaces;
using SocialApp.Structure;

namespace SocialApp.Pages
{
    public class clsMyFriendsPage : absScrollPage, INeedAuthentication
    {
        public override string PageName { get; } = "My Friends";
        protected override string EmptyRowsMessage { get; } = "You have no friends";
        public clsAppState AppState { get; }
        public clsServiceCollection Services { get; }

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

        protected override stPageRow GetPageHeader()
        {
            return new stPageRow(centerContent: PageName);
        }
    }
}
