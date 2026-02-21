using SocialApp.Abstractions;
using SocialApp.Interfaces;
using SocialApp.Model;
using SocialApp.Services;
using SocialApp.Structure;
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
        public override string DefaultMassage { get; } = "There Is No Requests, Check Again Later";
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
      
        public override List<stPageRow> GetContentRows()
        {
            List <string > friendRequests = FriendServices.GetFriendRequistsUsers(AppState.User.Name);
            return friendRequests.Select(fr => new stPageRow(fr)).ToList();
        }

    }
}
