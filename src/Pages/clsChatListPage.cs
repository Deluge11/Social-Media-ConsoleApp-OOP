using SocialApp.Enums;
using SocialApp.Interfaces;
using SocialApp.Interfaces.Page;
using SocialApp.Pages.Abstractions;
using SocialApp.Services;
using SocialApp.Structure;

namespace SocialApp.Pages
{
    public class clsChatListPage : absScrollSelection, IRootPage, INeedAuthentication
    {
        public override string PageName { get; } = "Chat Page";
        protected override string EmptyRowsMessage { get; } = "You have no friends";

        public override enPermission AccessPermission => enPermission.None;
        protected override enResetCursor CursorResetCommand => enResetCursor.Up;


        public absBasePage Next()
        {
            var friendsList = clsFriendServices.GetUserFriends(clsAppState.User.Name);

            if (friendsList.Count == 0)
            {
                return null!;
            }

            if (clsMessageServices.GetChatId(clsAppState.User.Name, friendsList[SelectionCursor], out int chatId))
            {
                return new clsMessagesPage(chatId, friendsList[SelectionCursor]);
            }

            return null!;

        }

        protected override List<stPageRow> GetContentRows()
        {
            return clsFriendServices
                .GetUserFriends(clsAppState.User.Name)
                .Select(f => new stPageRow(f))
                .ToList();
        }

        protected override stPageRow GetHeaderRow()
        {
            return new stPageRow(centerContent: $"Choose Friend {clsCustomTags.LineBreak} To Chat With");
        }
    }
}
