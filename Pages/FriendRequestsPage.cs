using SocialApp.Abstractions;
using SocialApp.Interfaces;
using SocialApp.Services;
using SocialApp.Structure;


namespace SocialApp.Pages
{
    public class FriendRequestsPage : AbScrollCursor, IAction
    {
        public override string PageName { get; } = "Friend Requests";
        public override string DefaultMassage { get; } = "There Is No Requests, Check Again Later";
        public string ActionName { get; } = "Accept friend request";
        public FriendServices FriendServices { get; }
        public AppState AppState { get; }

        public FriendRequestsPage(AppState appState, FriendServices friendServices)
        {
            FriendServices = friendServices;
            AppState = appState;
        }
        public void Action()
        {
            var username = AppState.User.Name;
            var usersList = FriendServices.GetFriendRequistsUsers(username);

            if(usersList.Count == 0) return;

            FriendServices.ConnectUsers(username, usersList[Cursor]);
            ScrollUp();
        }
      
        public override List<stPageRow> GetContentRows()
        {
            return FriendServices
                .GetFriendRequistsUsers(AppState.User.Name)
                .Select(fr => new stPageRow(fr))
                .ToList();
        }
        public override stPageRow GetPageHeaders()
        {
            return new stPageRow(centerContent: PageName);
        }
    }
}
