using SocialApp.Abstractions;
using SocialApp.Interfaces;
using SocialApp.Services;
using SocialApp.Structure;

namespace SocialApp.Pages
{
    public class MessagesPage : AbScrollPage, IAction
    {
        public override string PageName { get; } = "Messages Page";
        public override string DefaultMassage { get; } = "Break the silence";
        public string ActionName { get; } = "Add new message";
        public string FriendName { get; }
        public int ChatId { get; }
        public MessageServices MessageServices { get; }
        public AppState AppState { get; }

        public MessagesPage(AppState appState, MessageServices messageServices, int chatId, string friendname)
        {
            MessageServices = messageServices;
            FriendName = friendname;
            AppState = appState;
            ChatId = chatId;
        }

        public void Action()
        {
            if (!AppState.IsAuthenticated) return;
            MessageServices.AddMessage(ChatId, AppState.User.Id);
            ResetStart();
        }
        public override void ResetStart()
        {
            Start = GetContentRows().Count - 3;
            if (Start < 0)
                Start = 0;
        }
        public override List<stPageRow> GetContentRows()
        {
            return MessageServices
                .GetChatMessages(ChatId)
                .Select(
                message => new stPageRow(
                    leftContent: message.UserId == AppState.User.Id ? message.MessageString : "",
                    centerContent: "",
                    rightContent: message.UserId != AppState.User.Id ? message.MessageString : ""
                    ))
                .ToList();
        }
        public override stPageRow GetPageHeaders()
        {
            return new stPageRow(
                $"--={{ `You` }}=--",
                $"Messages Count #h #h ---=( {GetContentRows().Count.ToString()} )=---",
                $"--={{ {FriendName} }}=--"
            );
        }
    }
}
