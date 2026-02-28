using SocialApp.Model;

namespace SocialApp
{
    public class AppState
    {
        public User User { get; set; }
        public bool IsAuthenticated { get; set; }
        public int Permissions { get; set; } //Not Used (Yet) :)
    }
}
