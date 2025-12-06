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
    public class FriendRequestsPage : IPage, IScrollCursor, IAction
    {
        public string PageName { get; } = "Friend Requests";
        public string DefaultMassage { get; } = "There is no requests" + "#h" + "Check again later";
        public string ActionName { get; } = "Accept friend request";
        public string[] ContentGrids { get; } = new string[12];
        public int Cursor { get; set; }
        public int Start { get; set; }
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

        public void ScrollDown()
        {
            var usersList = FriendServices.GetFriendRequistsUsers(AppState.User.Name);

            if (Cursor < usersList.Count - 1)
                Cursor++;
            if (Cursor > Start + 2)
                Start++;
        }
        public void ScrollUp()
        {
            if (Cursor > 0)
                Cursor--;
            if (Cursor < Start)
                Start--;
        }

        public void SetPageContent()
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

        public void ResetCursor()
        {
            Cursor = 0;
        }
        public void ResetStart()
        {
            Start = 0;
        }

        public void ResetContent()
        {
            for (int i = 0; i < ContentGrids.Length; i++)
            {
                ContentGrids[i] = "";
            }
        }
    }
}
