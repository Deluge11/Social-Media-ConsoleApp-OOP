using SocialApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Services
{
    public class MessageServices
    {
        public Dictionary<int, Messages> MessagesDB { get; }
        public Dictionary<string, User> UsersDB { get; }

        public MessageServices(DataManager dataManager)
        {
            MessagesDB = dataManager.MessagesDB;
            UsersDB = dataManager.UsersDB;
        }

        public List<Msg> GetChatMessages(int chatId)
        {
            return MessagesDB[chatId].messagesList;
        }

        public int GetChatMessagesCount(int chatId)
        {
            return MessagesDB[chatId].messagesList.Count;
        }

        public int GetChatId(string userName,string friendName)
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

        public void AddMessage(int chatId,string message,int userId)
        {
            if (message.Length < 1 || !MessagesDB.ContainsKey(chatId))
                return;

            Msg newMessage = new Msg(userId, message, DateTime.Now);
            MessagesDB[chatId].AddMsg(newMessage);
        }

    }
}
