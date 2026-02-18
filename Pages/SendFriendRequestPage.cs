using SocialApp.Abstractions;
using SocialApp.Interfaces;
using SocialApp.Model;
using SocialApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Pages
{
    public class SendFriendRequestPage : AbScrollCursor, IAction
    {
        public override string PageName { get; } = "Add Friends";
        public override string DefaultMassage { get; } = "There is no users" + "#h" + "Try to check later";
        public string ActionName { get; } = "Send friend request";
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
            {
                return;
            }

            var otherUsername = usersList[Cursor];

            if (!FriendServices.CanSendRequestToThisUser(username, otherUsername))
            {
                return;
            }
            FriendServices.AddFreindRequest(username, otherUsername);
            ScrollUp();
        }
    
        public override void SetPageContent()
        {
            var usersList = FriendServices.GetUnfriendsUsers(AppState.User.Name);

            ContentGrids[1] = PageName;

            if (usersList.Count == 0)
            {
                ContentGrids[4] = DefaultMassage;
                return;
            }

            if (Start < usersList.Count)
            {
                ContentGrids[3] = usersList[Start];
            }
            if (Start + 1 < usersList.Count)
            {
                ContentGrids[6] = usersList[Start + 1];
            }
            if (Start + 2 < usersList.Count)
            {
                ContentGrids[9] = usersList[Start + 2];
            }
        }

        public override int GetScrollContentCount()
        {
            return FriendServices.GetUnfriendsUsers(AppState.User.Name).Count;
        }
    }
}
