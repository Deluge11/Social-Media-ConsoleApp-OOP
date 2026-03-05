using SocialApp.Abstractions;
using SocialApp.Interfaces;
using SocialApp.Structure;

namespace SocialApp.Pages
{
    public class clsSendFriendRequestPage : absScrollCursor, IAction, INeedAuthentication
    {
        public override string PageName { get; } = "Add Friends";
        protected override string EmptyRowsMessage { get; } = "There is no users Try to check later";
        public string ActionName { get; } = "Send friend request";
        public clsAppState AppState { get; }
        public clsServiceCollection Services { get; }

        public clsSendFriendRequestPage(clsAppState appState, clsServiceCollection services)
        {
            AppState = appState;
            Services = services;
        }


        public void Execute()
        {
            var usersList = Services.FriendService.GetUserWhoCanSendFriendRequest(AppState.User.Name);

            if (usersList.Count == 0) return;

            Services.FriendService.AddFriendRequest(AppState.User.Name, usersList[Cursor]);
            ScrollUp();
        }

        protected override List<stPageRow> GetContentRows()
        {
            return Services.FriendService
                .GetUserWhoCanSendFriendRequest(AppState.User.Name)
                .Select(u => new stPageRow(u))
                .ToList();
        }

        protected override stPageRow GetPageHeader()
        {
            return new stPageRow(centerContent: PageName);
        }
    }
}
