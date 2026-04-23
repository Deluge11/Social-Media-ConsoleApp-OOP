using SocialApp.Data;
using SocialApp.Model;


namespace SocialApp.Services
{
    public class clsMessageServices
    {
        protected Dictionary<int, clsChat> MessagesDB { get; }
        protected Dictionary<string, clsUser> UsersDB { get; }

        public clsMessageServices(clsDataManager dataManager)
        {
            MessagesDB = dataManager.MessagesDB;
            UsersDB = dataManager.UsersDB;
        }

        public List<clsMessage> GetChatMessages(int chatId)
        {
            return MessagesDB[chatId].messagesList;
        }

        public int GetChatMessagesCount(int chatId)
        {
            return MessagesDB[chatId].messagesList.Count;
        }

        public int GetChatId(string userName, string friendName)
        {
            foreach (int chatId in UsersDB[userName].ChatID)
            {
                if (UsersDB[friendName].ChatID.Contains(chatId))
                {
                    return chatId;
                }
            }
            return -1;
        }

        public bool AddMessage(int chatId, string userName, string message)
        {
            if (message.Length < 1 || !MessagesDB.ContainsKey(chatId) || !UsersDB[userName].ChatID.Contains(chatId))
            {
                return false;
            }

            clsMessage newMessage = new clsMessage(UsersDB[userName].Id, message, DateTime.Now);
            MessagesDB[chatId].AddMsg(newMessage);
            return true;

        }

    }
}
