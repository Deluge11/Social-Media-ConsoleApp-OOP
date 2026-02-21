using SocialApp.Abstractions;
using SocialApp.Interfaces;
using SocialApp.Services;
using SocialApp.Structure;

namespace SocialApp.Pages
{
    public class ChatPage : AbScrollCursor, IRootPage
    {
        public override string PageName { get; } = "Chat Page";
        public override string DefaultMassage { get; } = "You have no friends";
        public AppState AppState { get; }
        public FriendServices FriendServices { get; }
        public MessageServices MessageServices { get; }

        public ChatPage(AppState appState, FriendServices friendServices, MessageServices messageServices)
        {
            AppState = appState;
            FriendServices = friendServices;
            MessageServices = messageServices;
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

        public override List<stPageRow> GetContentRows()
        {
            return FriendServices
                .GetUserFriends(AppState.User.Name)
                .Select(f => new stPageRow(f))
                .ToList();
        }
        public override stPageRow GetPageHeaders()
        {
            return new stPageRow(centerContent: PageName);
        }
    }
}
