

namespace SocialApp.Model
{
   public class Messages
    {
        public int ChatID { get; set; }
        public List<Msg> messagesList { get; set; }

        public Messages(int cid)
        {
            ChatID = cid;
            messagesList = new List<Msg>();
        }

        public void AddMsg(Msg msg)
        {
            messagesList.Add(msg);
        }


    }
}
