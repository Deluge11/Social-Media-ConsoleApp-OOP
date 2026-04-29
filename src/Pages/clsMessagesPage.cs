using SocialApp.Enums;
using SocialApp.HelperTools;
using SocialApp.Interfaces;
using SocialApp.Interfaces.Page;
using SocialApp.Pages.Abstractions;
using SocialApp.Services;
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

        public enPermission ActionPermission => enPermission.None;
        public override enPermission AccessPermission => enPermission.None;
        protected override enResetCursor CursorResetCommand => enResetCursor.Down;


        public clsMessagesPage(int chatId, string friendName)
        {
            FriendName = friendName;
            ChatId = chatId;
        }

        public void Execute()
        {
            Console.Clear();
            clsConsoleUI.PrintMessage("Message Screen");
            string newMessage = clsConsoleInput.GetStringInput("Write New Message");

            if(clsMessageServices.AddMessage(ChatId, clsAppState.User.Name, newMessage))
            {
                ResetCursors();
            }
            else
            {
                Console.Clear();
                clsConsoleUI.PrintMessage("Send Message Failed");
                clsConsoleUI.PressKeyToContinue();
            }
        }

        protected override List<stPageRow> GetContentRows()
        {
            return clsMessageServices
                .GetChatMessages(ChatId)
                .Select(message => new stPageRow(
                    message.UserId == clsAppState.User.Id ? message.MessageString : "",
                    clsCustomTags.LineBreak + message.Date.ToString("yyy/MM/dd | HH:mm"),
                    message.UserId != clsAppState.User.Id ? message.MessageString : ""
                    ))
                .ToList();
        }

        protected override stPageRow GetHeaderRow()
        {
            int count = clsMessageServices.GetChatMessages(ChatId).Count();

            return new stPageRow(
                $"--={{ `You` }}=--",
                $"Messages Count {clsCustomTags.LineBreak}{clsCustomTags.LineBreak} ---=( {count} )=---",
                $"--={{ {FriendName} }}=--"
            );
        }
    }
}
