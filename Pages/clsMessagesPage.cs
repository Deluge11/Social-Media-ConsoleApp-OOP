using SocialApp.Abstractions;
using SocialApp.Interfaces;
using SocialApp.Services;
using SocialApp.Structure;

namespace SocialApp.Pages
{
    public class clsMessagesPage : absScrollPage, IAction, INeedAuthentication
    {
        public override string PageName { get; } = "Messages Page";
        protected override string EmptyRowsMessage { get; } = "Break the silence";
        public string ActionName { get; } = "Add new message";
        public string FriendName { get; }
        public int ChatId { get; }
        public clsAppState AppState { get; }
        public clsServiceCollection Services { get; }

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
            Reset();
        }

        public override void Reset()
        {
            Start = GetContentRows().Count - PAGE_ROWS_LIMIT;
            if (Start < 0)
                Start = 0;
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

        protected override stPageRow GetPageHeader()
        {
            return new stPageRow(
                $"--={{ `You` }}=--",
                $"Messages Count {clsCustomTags.LineBreak}{clsCustomTags.LineBreak} ---=( {GetContentRows().Count.ToString()} )=---",
                $"--={{ {FriendName} }}=--"
            );
        }
    }
}
