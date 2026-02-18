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
        public override string PageName { get; } = "My Friends";
        public override string DefaultMassage { get; } = "You have no friends";
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

        public override void SetPageContent()
        {
            var friendList = FriendServices.GetUserFriends(AppState.User.Name);

            ContentGrids[1] = PageName;

            if (friendList.Count == 0)
            {
                ContentGrids[4] = DefaultMassage;
            }

            if (Start < friendList.Count)
            {
                ContentGrids[3] = friendList[Start];
            }
            if (Start + 1 < friendList.Count)
            {
                ContentGrids[6] = friendList[Start + 1];
            }
            if (Start + 2 < friendList.Count)
            {
                ContentGrids[9] = friendList[Start + 2];
            }
        }
    }
}
