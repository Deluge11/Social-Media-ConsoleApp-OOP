using SocialApp.Abstractions;
using SocialApp.Interfaces;
using SocialApp.Model;
using SocialApp.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Pages
{
    public class MessagesPage : AbScrollPage, IAction
    {
        public override string PageName { get; } = "Messages Page";
        public override string DefaultMassage { get; } = "Break the silence";
        public string ActionName { get; } = "Add new message";
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
        public void Action()
        {
            if (!AppState.IsAuthenticated) return;
            MessageServices.AddMessage(ChatId, AppState.User.Id);
            ResetStart();
        }

        public override void SetPageContent()
        {
            ContentGrids[0] = $"{{ {AppState.User.Name} }}";
            ContentGrids[2] = $"{{ {FriendName} }}";

            var massageList = MessageServices.GetChatMessages(ChatId);

            int listCount = massageList.Count;

            if (listCount == 0)
            {
                ContentGrids[4] = DefaultMassage;
            }

            if (Start < 0) return;

            if (massageList[Start].UserId == AppState.User.Id)
            {
                ContentGrids[9] = massageList[Start].MsgString;
            }
            else
            {
                ContentGrids[11] = massageList[Start].MsgString;
            }

            if (Start < 1) return;

            if (Start - 1 < listCount && massageList[Start - 1].UserId == AppState.User.Id)
            {
                ContentGrids[6] = massageList[Start - 1].MsgString;
            }
            else
            {
                ContentGrids[8] = massageList[Start - 1].MsgString;
            }

            if (Start < 2) return;

            if (Start - 2 < listCount && massageList[Start - 2].UserId == AppState.User.Id)
            {
                ContentGrids[3] = massageList[Start - 2].MsgString;
            }
            else
            {
                ContentGrids[5] = massageList[Start - 2].MsgString;
            }
        }

        public override int GetScrollContentCount()
        {
            return MessageServices.GetChatMessagesCount(ChatId);
        }
        public override void ResetStart()
        {
            Start = GetScrollContentCount() - 1;
        }
        public override void ScrollDown()
        {
            if (Start < GetScrollContentCount() - 1)
                Start++;
        }
        public override void ScrollUp()
        {
            if (Start - 2 > 0)
                Start--;
        }

    }
}
