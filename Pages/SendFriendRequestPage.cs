using SocialApp.Abstractions;
using SocialApp.Interfaces;
using SocialApp.Services;
using SocialApp.Structure;

namespace SocialApp.Pages
{
    public class SendFriendRequestPage : AbScrollCursor, IAction
    {
        public override string PageName { get; init; } = "Add Friends";
        public override string DefaultMassage { get; init; } = "There is no users Try to check later";
        public string ActionName { get; init; } = "Send friend request";
        public FriendServices FriendServices { get; }
        public AppState AppState { get; }

        public SendFriendRequestPage(AppState appState, FriendServices friendServices)
        {
            FriendServices = friendServices;
            AppState = appState;
        }
        public void Action()
        {
            string username = AppState.User.Name;
            var usersList = FriendServices.GetUnfriendsUsers(username);

            if (usersList.Count == 0)
                return;

            var otherUsername = usersList[Cursor];

            if (!FriendServices.CanSendRequestToThisUser(username, otherUsername))
                return;

            FriendServices.AddFreindRequest(username, otherUsername);
            ScrollUp();
        }


        public override List<stPageRow> GetContentRows()
        {
            List<string> users = FriendServices.GetUnfriendsUsers(AppState.User.Name);
            return users.Select(u => new stPageRow(u)).ToList();
        }
    }
}
