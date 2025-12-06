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
    public class MessagesPage : IPage, IScrollPage, IAction
    {
        public string PageName { get; } = "Messages Page";
        public string DefaultMassage { get; } = "Break the silence";
        public string[] ContentGrids { get; } = new string[12];
        public int Start { get; set; }
        public MessageServices MessageServices { get; }
        public AppState AppState { get; }
        public int ChatId { get; set; }
        public string FriendName { get; set; }
        public string ActionName { get; } = "Add new message";


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
            if (!AppState.IsAuthenticated)
            {
                return;
            }

            Console.Clear();
            Console.WriteLine("| Write new massage ");
            Console.Write(" => ");

            string message = Console.ReadLine()!.Trim();
            MessageServices.AddMessage(ChatId, message, AppState.User.Id);
            Start = MessageServices.GetChatMessagesCount(ChatId) - 1;
        }

        public void ScrollDown()
        {
            if (Start < MessageServices.GetChatMessagesCount(ChatId) - 1)
                Start++;
        }
        public void ScrollUp()
        {
            if (Start - 2 > 0)
                Start--;
        }

        public void SetPageContent()
        {
          
            ContentGrids[0] = $"{{ {AppState.User.Name} }}";
            ContentGrids[2] = $"{{ {FriendName} }}";

            var massageList = MessageServices.GetChatMessages(ChatId);

            int listCount = massageList.Count;

            if(listCount == 0)
            {
                ContentGrids[4] = DefaultMassage;
            }

            if (Start < 0)
                return;

            if (massageList[Start].UserId == AppState.User.Id)
            {
                ContentGrids[9] = massageList[Start].MsgString;
            }
            else
            {
                ContentGrids[11] = massageList[Start].MsgString;
            }

            if (Start < 1)
                return;

            if (Start - 1 < listCount && massageList[Start - 1].UserId == AppState.User.Id)
            {
                ContentGrids[6] = massageList[Start - 1].MsgString;
            }
            else
            {
                ContentGrids[8] = massageList[Start - 1].MsgString;
            }

            if (Start < 2)
                return;

            if (Start - 2 < listCount && massageList[Start - 2].UserId == AppState.User.Id)
            {
                ContentGrids[3] = massageList[Start - 2].MsgString;
            }
            else
            {
                ContentGrids[5] = massageList[Start - 2].MsgString;
            }
        }

        public void ResetStart()
        {
            Start = MessageServices.GetChatMessagesCount(ChatId) - 1;
        }

        public void ResetContent()
        {
            for (int i = 0; i < ContentGrids.Length; i++)
            {
                ContentGrids[i] = "";
            }
        }
    }
}
