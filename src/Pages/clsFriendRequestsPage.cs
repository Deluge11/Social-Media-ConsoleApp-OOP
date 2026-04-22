using SocialApp.Enums;
using SocialApp.Interfaces;
using SocialApp.Interfaces.Page;
using SocialApp.Pages.Abstractions;
using SocialApp.Structure;


namespace SocialApp.Pages
{
    public class clsFriendRequestsPage : absScrollSelection, IAction, INeedAuthentication
    {
        public override string PageName { get; } = "Friend Requests";
        protected override string EmptyRowsMessage { get; } = "There Is No Requests, Check Again Later";
        public string ActionName { get; } = "Accept friend request";

        public clsAppState AppState { get; }
        public clsServiceCollection Services { get; }

        public enPermission ActionPermission => enPermission.None;
        public override enPermission AccessPermission => enPermission.None;
        protected override enResetCursor CursorResetCommand => enResetCursor.Up;



        public clsFriendRequestsPage(clsAppState appState, clsServiceCollection services)
        {
            AppState = appState;
            Services = services;
        }

        public void Execute()
        {
            var usersList = Services.FriendService.GetFriendRequestsUsers(AppState.User.Name);

            if (usersList.Count == 0) return;

            Services.FriendService.ConnectUsers(AppState.User.Name, usersList[SelectionCursor]);
            ScrollUp();
        }

        protected override List<stPageRow> GetContentRows()
        {
            return Services.FriendService
                .GetFriendRequestsUsers(AppState.User.Name)
                .Select(fr => new stPageRow(fr))
                .ToList();
        }

        protected override stPageRow GetHeaderRow()
        {
            return new stPageRow(centerContent: PageName);
        }
    }
}
