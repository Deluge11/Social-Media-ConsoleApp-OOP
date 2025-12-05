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
    public class MyFriendsPage : IPage, IScrollPage
    {
        public string PageName { get; } = "My Friends";
        public string DefaultMassage { get; } = "You have no friends";
        public string[] ContentGrids { get; } = new string[12];
        public int Start { get; set; }
        public FriendServices FriendServices { get; }
        public AppState AppState { get; }

        public MyFriendsPage(AppState appState, FriendServices friendServices)
        {
            FriendServices = friendServices;
            AppState = appState;
        }
        public void ScrollDown()
        {
            var friendList = FriendServices.GetUserFriends(AppState.User.Name);

            if (Start + 3 < friendList.Count)
                Start++;
        }
        public void ScrollUp()
        {
            if (Start > 0)
                Start--;
        }

        public void SetPageContent()
        {
            for (int i = 0; i < ContentGrids.Length; i++)
            {
                ContentGrids[i] = "";
            }

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
        public void ResetStart()
        {
            Start = 0;
        }
    }
}
