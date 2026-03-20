using SocialApp.Abstractions;
using SocialApp.Enums;
using SocialApp.Interfaces;
using SocialApp.Structure;

namespace SocialApp.Pages
{
    public class clsChatListPage : absScrollSelection, IRootPage, INeedAuthentication
    {
        public override string PageName { get; } = "Chat Page";
        protected override string EmptyRowsMessage { get; } = "You have no friends";

        public clsAppState AppState { get; }
        public clsServiceCollection Services { get; }

        public override enPermission AccessPermission => enPermission.None;
        protected override enResetCursor CursorResetCommand => enResetCursor.Up;


        public clsChatListPage(clsAppState appState, clsServiceCollection services)
        {
            AppState = appState;
            Services = services;
        }

        public absBasePage Next()
        {
            var friendsList = Services.FriendService.GetUserFriends(AppState.User.Name);

            if (friendsList.Count == 0)
                return null!;

            int chatId = Services.MessageService.GetChatId(AppState.User.Name, friendsList[SelectionCursor]);

            if (chatId == -1)
                return null!;

            return new clsMessagesPage(AppState, Services, chatId, friendsList[SelectionCursor]);
        }

        protected override List<stPageRow> GetContentRows()
        {
            return Services.FriendService
                .GetUserFriends(AppState.User.Name)
                .Select(f => new stPageRow(f))
                .ToList();
        }

        protected override stPageRow GetHeaderRow()
        {
            return new stPageRow(centerContent: $"Choose Friend {clsCustomTags.LineBreak} To Chat With");
        }
    }
}
