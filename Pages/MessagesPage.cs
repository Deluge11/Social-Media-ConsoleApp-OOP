using SocialApp.Abstractions;
using SocialApp.Interfaces;
using SocialApp.Services;
using SocialApp.Structure;

namespace SocialApp.Pages
{
    public class MessagesPage : AbScrollPage, IAction
    {
        public override string PageName { get; } = "Messages Page";
        protected override string DefaultMessage { get; } = "Break the silence";
        public string ActionName { get; } = "Add new message";
        public string FriendName { get; }
        public int ChatId { get; }
        public MessageServices MessageServices { get; }
        public AppState AppState { get; }

        public MessagesPage(AppState appState, MessageServices messageServices, int chatId, string friendName)
        {
            MessageServices = messageServices;
            FriendName = friendName;
            AppState = appState;
            ChatId = chatId;
        }

        public void Action()
        {
            if (!AppState.IsAuthenticated) return;

            clsConsoleUI.PrintMessage("Message Screen");
            MessageServices.AddMessage(ChatId, AppState.User.Id, clsConsoleInput.GetStringInput("Write New Message"));
            Reset();
        }

        public override void Reset()
        {
            Start = GetContentRows().Count - 3;
            if (Start < 0)
                Start = 0;
        }

        protected override List<stPageRow> GetContentRows()
        {
            return MessageServices
                .GetChatMessages(ChatId)
                .Select(message => new stPageRow(
                    leftContent: message.UserId == AppState.User.Id ? message.MessageString : "",
                    centerContent: "",
                    rightContent: message.UserId != AppState.User.Id ? message.MessageString : ""
                    ))
                .ToList();
        }

        protected override stPageRow GetPageHeader()
        {
            return new stPageRow(
                $"--={{ `You` }}=--",
                $"Messages Count #h #h ---=( {GetContentRows().Count.ToString()} )=---",
                $"--={{ {FriendName} }}=--"
            );
        }
    }
}
