using SocialApp.Abstractions;
using SocialApp.Interfaces;
using SocialApp.Services;
using SocialApp.Structure;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SocialApp.Pages
{
    public class MessagesPage : AbScrollPage, IAction
    {
        public override string PageName { get; init; } = "Messages Page";
        public override string DefaultMassage { get; init; } = "Break the silence";
        public string ActionName { get; init; } = "Add new message";
        public string FriendName { get; set; }
        public int ChatId { get; set; }
        public MessageServices MessageServices { get; }
        public AppState AppState { get; }

        public MessagesPage(AppState appState, MessageServices messageServices, int chatId, string friendname)
        {
            MessageServices = messageServices;
            AppState = appState;
            ChatId = chatId;
            FriendName = friendname;
            Start = MessageServices.GetChatMessagesCount(chatId) - 1;
        }

        public override string GetPageLeftHeaders() => $"{{ {AppState.User.Name} }}";
        public override string GetPageRightHeaders() => $"{{ {FriendName} }}";
        public override string GetPageCenterHeaders() => $"Messages Count #h #h    ( {GetContentRows().Count.ToString()} )";

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
            var massageList = MessageServices.GetChatMessages(ChatId);
            return massageList.Select(
                m => new stPageRow(
                    leftContent: m.UserId == AppState.User.Id ? m.MsgString : "",
                    centerContent: "",
                    rightContent: m.UserId != AppState.User.Id ? m.MsgString : ""
                    ))
                .ToList();
        }
    }
}
