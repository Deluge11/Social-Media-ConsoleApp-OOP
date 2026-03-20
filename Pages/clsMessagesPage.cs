using SocialApp.Abstractions;
using SocialApp.Enums;
using SocialApp.HelperTools;
using SocialApp.Interfaces;
using SocialApp.Structure;

namespace SocialApp.Pages
{
    public class clsMessagesPage : absScrollPage, IAction, INeedAuthentication
    {
        public override string PageName { get; } = "Messages Page";
        public string ActionName { get; } = "Add new message";
        protected override string EmptyRowsMessage { get; } = "Break the silence";

        public int ChatId { get; }
        public string FriendName { get; }

        public clsAppState AppState { get; }
        public clsServiceCollection Services { get; }

        public enPermission ActionPermission => enPermission.None;
        public override enPermission AccessPermission => enPermission.None;
        protected override enResetCursor CursorResetCommand => enResetCursor.Down;


        public clsMessagesPage(clsAppState appState, clsServiceCollection services, int chatId, string friendName)
        {
            FriendName = friendName;
            AppState = appState;
            Services = services;
            ChatId = chatId;
        }

        public void Execute()
        {
            Console.Clear();
            clsConsoleUI.PrintMessage("Message Screen");
            string newMessage = clsConsoleInput.GetStringInput("Write New Message");

            Services.MessageService.AddMessage(ChatId, AppState.User.Id, newMessage);
            ResetCursors();
        }

        protected override List<stPageRow> GetContentRows()
        {
            return Services.MessageService
                .GetChatMessages(ChatId)
                .Select(message => new stPageRow(
                    message.UserId == AppState.User.Id ? message.MessageString : "",
                    message.Date.ToString("yyy/MM/dd | HH:mm"),
                    message.UserId != AppState.User.Id ? message.MessageString : ""
                    ))
                .ToList();
        }

        protected override stPageRow GetHeaderRow()
        {
            int count = Services.MessageService.GetChatMessages(ChatId).Count();

            return new stPageRow(
                $"--={{ `You` }}=--",
                $"Messages Count {clsCustomTags.LineBreak}{clsCustomTags.LineBreak} ---=( {count} )=---",
                $"--={{ {FriendName} }}=--"
            );
        }
    }
}
