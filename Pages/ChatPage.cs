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
    public class ChatPage : AbScrollCursor, IRootPage
    {
        public override string PageName { get; } = "Chat Page";
        public override string DefaultMassage { get; } = "You have no friends";
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

        public override int GetScrollContentCount() => FriendServices.GetUserFriends(AppState.User.Name).Count;

        public override void SetPageContent()
        {
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

        public AbPage Next()
        {
            var username = AppState.User.Name;

            var friendsList = FriendServices.GetUserFriends(username);

            if (friendsList.Count == 0)
            {
                return null;
            }

            int chatId = MessageServices.GetChatId(username, friendsList[Cursor]);

            if (chatId == -1)
            {
                return null;
            }

            return new MessagesPage(AppState, MessageServices, chatId, friendsList[Cursor]);
        }
    }
}
