
namespace SocialApp.Services
{
    public class clsAuthenticationServices
    {
        public static bool Login(string username, string password)
        {
            clsAppState.User = clsUserServices.GetUserByUsernameAndPassword(username, password);
            clsAppState.IsGuest = false;
            return clsAppState.User != null;
        }

        public static bool Register(string username, string password)
        {
            clsAppState.User = clsUserServices.AddUser(username, password);
            clsAppState.IsGuest = false;
            return clsAppState.User != null;
        }

        public static void RegisterAsGuest()
        {
            clsAppState.User = null!;
            clsAppState.IsGuest = true;
        }

    }
}
