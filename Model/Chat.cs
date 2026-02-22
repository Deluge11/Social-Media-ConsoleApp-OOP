

namespace SocialApp.Model
{
   public class Chat
    {
        public int ChatID { get; }
        public List<Message> messagesList { get; }

        public Chat(int cid)
        {
            ChatID = cid;
            messagesList = new List<Message>();
        }

        public void AddMsg(Message message)
        {
            messagesList.Add(message);
        }


    }
}
