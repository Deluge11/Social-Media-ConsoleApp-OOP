using SocialApp.Abstractions;
using SocialApp.Enums;
using SocialApp.Interfaces;
using SocialApp.Structure;


namespace SocialApp.Pages
{
    public class clsFriendRequestsPage : absScrollCursor, IAction, INeedAuthentication
    {
        public override string PageName { get; } = "Friend Requests";
        protected override string EmptyRowsMessage { get; } = "There Is No Requests, Check Again Later";
        public string ActionName { get; } = "Accept friend request";
        public clsAppState AppState { get; }
        public clsServiceCollection Services { get; }

        public enPermission ActionPermission => enPermission.None;
        public override enPermission AccessPermission => enPermission.None;


        public clsFriendRequestsPage(clsAppState appState, clsServiceCollection services)
        {
            AppState = appState;
            Services = services;
        }

        public void Execute()
        {
            var usersList = Services.FriendService.GetFriendRequestsUsers(AppState.User.Name);

            if (usersList.Count == 0) return;

            Services.FriendService.ConnectUsers(AppState.User.Name, usersList[Cursor]);
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
