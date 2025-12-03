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
    public class SendFriendRequestPage : IPage, IScrollCursor, IAction
    {
        public string PageName { get; } = "Add Friends";
        public string DefaultMassage { get; } = "There is no users #hTry to check later";
        public string[] ContentGrids { get; } = new string[12];
        public int Cursor { get; set; }
        public int Start { get; set; }
        public FriendServices FriendServices { get; }
        public AppState AppState { get; }
        public string ActionName { get; } = "Send friend request";

        public SendFriendRequestPage(AppState appState, FriendServices friendServices)
        {
            FriendServices = friendServices;
            AppState = appState;
        }
        public void Action()
        {
            string username = AppState.User.Name;
            var usersList = FriendServices.GetUnfriendsUsers(username);

            if (Cursor >= usersList.Count)
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

        public void ScrollDown()
        {
            var usersList = FriendServices.GetUnfriendsUsers(AppState.User.Name);
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
            for (int i = 0; i < ContentGrids.Length; i++)
            {
                ContentGrids[i] = "";
            }

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

        public void ResetCursor()
        {
            Cursor = 0;
        }
        public void ResetStart()
        {
            Start = 0;
        }
    }
}
