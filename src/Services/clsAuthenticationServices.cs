using SocialApp.Model;


namespace SocialApp.Services
{
    public class clsAuthenticationServices
    {
        protected clsAppState AppState { get; }
        protected clsUserServices UserServices { get; }

        public clsAuthenticationServices(clsAppState appState, clsUserServices userServices)
        {
            AppState = appState;
            UserServices = userServices;
        }

        public bool Login(string username, string password)
        {
            AppState.User = UserServices.GetUserByUsernameAndPassword(username, password);
            AppState.IsGuest = false;
            return AppState.User != null;
        }

        public bool Register(string username, string password)
        {
            AppState.User = UserServices.AddUser(username, password);
            AppState.IsGuest = false;
            return AppState.User != null;
        }

        public void RegisterAsGuest()
        {
            AppState.User = null!;
            AppState.IsGuest = true;
        }

    }
}
