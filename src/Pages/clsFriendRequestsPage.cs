using SocialApp.Enums;
using SocialApp.Interfaces;
using SocialApp.Interfaces.Page;
using SocialApp.Pages.Abstractions;
using SocialApp.Services;
using SocialApp.Structure;


namespace SocialApp.Pages
{
    public class clsFriendRequestsPage : absScrollSelection, IAction, INeedAuthentication
    {
        public override string PageName { get; } = "Friend Requests";
        protected override string EmptyRowsMessage { get; } = "There Is No Requests, Check Again Later";
        public string ActionName { get; } = "Accept friend request";

        public enPermission ActionPermission => enPermission.None;
        public override enPermission AccessPermission => enPermission.None;
        protected override enResetCursor CursorResetCommand => enResetCursor.Up;

        public void Execute()
        {
            var usersList = clsFriendServices.GetFriendRequestsUsers(clsAppState.User.Name);

            if (usersList.Count == 0) return;

            clsFriendServices.ConnectUsers(clsAppState.User.Name, usersList[SelectionCursor]);
            ScrollUp();
        }

        protected override List<stPageRow> GetContentRows()
        {
            return clsFriendServices
                .GetFriendRequestsUsers(clsAppState.User.Name)
                .Select(fr => new stPageRow(fr))
                .ToList();
        }

        protected override stPageRow GetHeaderRow()
        {
            return new stPageRow(centerContent: PageName);
        }
    }
}
