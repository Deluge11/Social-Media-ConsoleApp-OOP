

namespace SocialApp.Model
{
    public class clsMessage
    {
        public int UserId { get; set; }
        public string MessageString { get; set; }
        public DateTime Date { get; set; }

        public clsMessage(int userId, string message, DateTime date)
        {
            UserId = userId;
            MessageString = message;
            Date = date;
        }

    }
}
