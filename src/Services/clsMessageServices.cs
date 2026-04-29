using SocialApp.Data;
using SocialApp.Model;


namespace SocialApp.Services
{
    public class clsMessageServices
    {
        public static List<clsMessage> GetChatMessages(int chatId)
        {
            return clsDataManager.MessagesDB[chatId].messagesList;
        }

        public static int GetChatMessagesCount(int chatId)
        {
            return clsDataManager.MessagesDB[chatId].messagesList.Count;
        }

        public static int GetChatId(string userName, string friendName)
        {
            foreach (int chatId in clsDataManager.UsersDB[userName].ChatID)
            {
                if (clsDataManager.UsersDB[friendName].ChatID.Contains(chatId))
                {
                    return chatId;
                }
            }
            return -1;
        }

        public static bool AddMessage(int chatId, string userName, string message)
        {
            if (message.Length < 1 || !clsDataManager.MessagesDB.ContainsKey(chatId) || !clsDataManager.UsersDB[userName].ChatID.Contains(chatId))
            {
                return false;
            }

            clsMessage newMessage = new clsMessage(clsDataManager.UsersDB[userName].Id, message, DateTime.Now);
            clsDataManager.MessagesDB[chatId].AddMsg(newMessage);
            return true;

        }

    }
}
