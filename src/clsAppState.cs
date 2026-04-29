using SocialApp.Model;

namespace SocialApp
{
    public static class clsAppState
    {
        public static clsUser User { get; set; }
        public static bool IsGuest { get; set; }

        public static bool IsAuthenticated()
        {
            return User != null;
        }

        public static void Clear()
        {
            User = null!;
            IsGuest = false;
        }
    }
}
