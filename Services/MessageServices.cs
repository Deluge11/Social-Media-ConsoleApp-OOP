using SocialApp.Model;


namespace SocialApp.Services
{
    public class MessageServices
    {
        public Dictionary<int, Chat> MessagesDB { get; }
        public Dictionary<string, User> UsersDB { get; }

        public MessageServices(DataManager dataManager)
        {
            MessagesDB = dataManager.MessagesDB;
            UsersDB = dataManager.UsersDB;
        }

        public List<Message> GetChatMessages(int chatId)
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

        public void AddMessage(int chatId,int userId)
        {
            Console.Clear();
            Console.WriteLine("-----------------------------");
            Console.WriteLine($"\tWrite New Message");
            Console.WriteLine("-----------------------------");
            Console.Write(" => ");

            string message = Console.ReadLine()!.Trim();

            if (message.Length < 1 || !MessagesDB.ContainsKey(chatId))
                return;

            Message newMessage = new Message(userId, message, DateTime.Now);
            MessagesDB[chatId].AddMsg(newMessage);
        }

    }
}
