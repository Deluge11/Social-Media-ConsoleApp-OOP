using SocialApp.Model;


namespace SocialApp.Services
{
    public class AuthenticationServices
    {
        private AppState AppState { get; }
        private UserServices UserServices { get; }

        public AuthenticationServices(AppState appState, UserServices userServices)
        {
            AppState = appState;
            UserServices = userServices;
        }

        public bool Login(string username, string password)
        {
            AppState.User = UserServices.GetUserByUsernameAndPassword(username, password);
            return AppState.IsAuthenticated = AppState.User != null;
        }

        public bool Register(string username, string password)
        {
            AppState.User = UserServices.AddUser(username, password);
            return AppState.IsAuthenticated = AppState.User != null;
        }

        public void Logout()
        {
            AppState.User = null!;
            AppState.IsAuthenticated = false;
        }
    }
}
