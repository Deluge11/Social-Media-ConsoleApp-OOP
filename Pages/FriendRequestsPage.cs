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
        public override string PageName { get; init; } = "Friend Requests";
        public override string DefaultMassage { get; init; } = "There is no requests" + "#h" + "Check again later";
        public string ActionName { get; init; } = "Accept friend request";
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
      
        public override int GetScrollContentCount()
        {
            return FriendServices.GetFriendRequistsUsers(AppState.User.Name).Count;
        }

        public override List<string> GetScrollContent()
        {
            return FriendServices.GetFriendRequistsUsers(AppState.User.Name);
        }
    }
}
