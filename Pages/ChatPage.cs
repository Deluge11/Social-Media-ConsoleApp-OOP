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
    public class ChatPage : IPage, IScrollCursor, IRootPage
    {
        public string PageName { get; private set; } = "Chat Page";
        public string DefaultMassage { get; private set; } = "You have no friends";
        public string[] ContentGrids { get; private set; } = new string[12];
        public int Start { get; set; }
        public int Cursor { get; set; }
        public AppState AppState { get; }
        public FriendServices FriendServices { get; }
        public MessageServices MessageServices { get; }

        public ChatPage(
            AppState appState,
            FriendServices friendServices,
            MessageServices messageServices
            )
        {
            AppState = appState;
            FriendServices = friendServices;
            MessageServices = messageServices;
        }

        public void ScrollDown()
        {
            var friendsList = FriendServices.GetUserFriends(AppState.User.Name);

            if (Cursor < friendsList.Count - 1)
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

            var friendsList = FriendServices.GetUserFriends(AppState.User.Name);

            ContentGrids[1] = PageName;

            if (friendsList.Count == 0)
            {
                ContentGrids[4] = DefaultMassage;
                return;
            }

            if (Start < friendsList.Count)
            {
                ContentGrids[3] = friendsList[Start];
            }
            if (Start + 1 < friendsList.Count)
            {
                ContentGrids[6] = friendsList[Start + 1];
            }
            if (Start + 2 < friendsList.Count)
            {
                ContentGrids[9] = friendsList[Start + 2];
            }
        }

        public IPage Next()
        {
            var username = AppState.User.Name;

            var friendsList = FriendServices.GetUserFriends(username);

            int chatId = MessageServices.GetChatId(username, friendsList[Cursor]);

            if (chatId == -1)
            {
                return null;
            }

            return new MessagesPage(AppState, MessageServices, chatId, friendsList[Cursor]);
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
