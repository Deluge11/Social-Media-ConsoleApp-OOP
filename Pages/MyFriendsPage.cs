using SocialApp.Abstractions;
using SocialApp.Interfaces;
using SocialApp.Model;
using SocialApp.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Pages
{
    public class MyFriendsPage : AbScrollPage
    {
        public override string PageName { get; init; } = "My Friends";
        public override string DefaultMassage { get; init; } = "You have no friends";
        public FriendServices FriendServices { get; }
        public AppState AppState { get; }

        public MyFriendsPage(AppState appState, FriendServices friendServices)
        {
            FriendServices = friendServices;
            AppState = appState;
        }

        public override int GetScrollContentCount()
        {
            return FriendServices.GetUserFriends(AppState.User.Name).Count;
        }
        public override List<string> GetScrollContent()
        {
            return FriendServices.GetUserFriends(AppState.User.Name);
        }
    }
}
