using SocialApp.Abstractions;
using SocialApp.Services;
using SocialApp.Structure;

namespace SocialApp.Pages
{
    public class MyFriendsPage : AbScrollPage
    {
        public override string PageName { get; } = "My Friends";
        protected override string DefaultMessage { get; } = "You have no friends";
        public FriendServices FriendServices { get; }
        public AppState AppState { get; }

        public MyFriendsPage(AppState appState, FriendServices friendServices)
        {
            FriendServices = friendServices;
            AppState = appState;
        }

        protected override List<stPageRow> GetContentRows()
        {
            return FriendServices
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
