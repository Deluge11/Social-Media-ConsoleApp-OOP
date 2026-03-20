using SocialApp.Abstractions;
using SocialApp.Enums;
using SocialApp.Interfaces;
using SocialApp.Structure;

namespace SocialApp.Pages
{
    public class clsSendFriendRequestPage : absScrollSelection, IAction, INeedAuthentication
    {
        public override string PageName { get; } = "Add Friends";
        protected override string EmptyRowsMessage { get; } = "There is no users Try to check later";
        public string ActionName { get; } = "Send friend request";

        public clsAppState AppState { get; }
        public clsServiceCollection Services { get; }

        public enPermission ActionPermission => enPermission.None;
        public override enPermission AccessPermission => enPermission.None;
        protected override enResetCursor CursorResetCommand => enResetCursor.Up;


        public clsSendFriendRequestPage(clsAppState appState, clsServiceCollection services)
        {
            AppState = appState;
            Services = services;
        }


        public void Execute()
        {
            var usersList = Services.FriendService.GetUsersWhoCanSendFriendRequest(AppState.User.Name);

            if (usersList.Count == 0) return;

            Services.FriendService.AddFriendRequest(AppState.User.Name, usersList[SelectionCursor]);
            ScrollUp();
        }

        protected override List<stPageRow> GetContentRows()
        {
            return Services.FriendService
                .GetUsersWhoCanSendFriendRequest(AppState.User.Name)
                .Select(u => new stPageRow(u))
                .ToList();
        }

        protected override stPageRow GetHeaderRow()
        {
            return new stPageRow(centerContent: PageName);
        }
    }
}
