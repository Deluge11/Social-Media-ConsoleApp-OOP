

namespace SocialApp.Model
{
    public class Msg
    {
        public int UserId { get; set; }
        public string MsgString { get; set; }
        public DateTime Date { get; set; }

        public Msg(int uId, string msg, DateTime date)
        {
            UserId = uId;
            MsgString = msg;
            Date = date;
        }

    }
}
