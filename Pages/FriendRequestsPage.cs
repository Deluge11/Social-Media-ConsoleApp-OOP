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
    public class FriendRequestsPage : AbScrollCursor, IAction
    {
        public override string PageName { get; } = "Friend Requests";
        public override string DefaultMassage { get; } = "There is no requests" + "#h" + "Check again later";
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

            if(usersList.Count == 0)
            {
                return;
            }

            FriendServices.ConnectUsers(username, usersList[Cursor]);
            ScrollUp();
        }
        public override void SetPageContent()
        {
            var usersList = FriendServices.GetFriendRequistsUsers(AppState.User.Name);

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
            return FriendServices.GetFriendRequistsUsers(AppState.User.Name).Count;
        }
    }
}
