

namespace SocialApp.Model
{
   public class clsChat
    {
        public int ChatID { get; }
        public List<clsMessage> messagesList { get; }

        public clsChat(int cid)
        {
            ChatID = cid;
            messagesList = new List<clsMessage>();
        }

        public void AddMsg(clsMessage message)
        {
            messagesList.Add(message);
        }


    }
}
