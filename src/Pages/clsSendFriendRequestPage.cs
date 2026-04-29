using SocialApp.Enums;
using SocialApp.HelperTools;
using SocialApp.Interfaces;
using SocialApp.Interfaces.Page;
using SocialApp.Pages.Abstractions;
using SocialApp.Services;
using SocialApp.Structure;

namespace SocialApp.Pages
{
    public class clsSendFriendRequestPage : absScrollSelection, IAction, INeedAuthentication
    {
        public override string PageName { get; } = "Add Friends";
        protected override string EmptyRowsMessage { get; } = "There is no users Try to check later";
        public string ActionName { get; } = "Send friend request";

        public enPermission ActionPermission => enPermission.None;
        public override enPermission AccessPermission => enPermission.None;
        protected override enResetCursor CursorResetCommand => enResetCursor.Up;


        public void Execute()
        {
            var usersList = clsFriendServices.GetUsersWhoCanSendFriendRequest(clsAppState.User.Name);

            if (usersList.Count == 0) return;

            if (clsFriendServices.AddFriendRequest(clsAppState.User.Name, usersList[SelectionCursor]))
            {
                Console.Clear();
                clsConsoleUI.PrintMessage($"Friend Request Sent Successfully To {usersList[SelectionCursor]}");
                clsConsoleUI.PressKeyToContinue();
                ScrollUp();
            }
            else
            {
                Console.Clear();
                clsConsoleUI.PrintMessage($"Send Request Failed!");
                clsConsoleUI.PressKeyToContinue();
            }

        }

        protected override List<stPageRow> GetContentRows()
        {
            return clsFriendServices
                .GetUsersWhoCanSendFriendRequest(clsAppState.User.Name)
                .Select(u => new stPageRow(u))
                .ToList();
        }

        protected override stPageRow GetHeaderRow()
        {
            return new stPageRow(centerContent: PageName);
        }
    }
}
