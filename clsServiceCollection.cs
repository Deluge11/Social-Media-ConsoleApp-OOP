using SocialApp.Services;

namespace SocialApp
{
    public class clsServiceCollection
    {
        public clsPostServices PostService { get; }
        public clsUserServices UserService { get; }
        public clsFriendServices FriendService { get; }
        public clsMessageServices MessageService { get; }
        public clsAuthenticationServices AuthenticationService { get; }
        public clsServiceCollection(clsDataManager dataManager, clsAppState appState)
        {
            PostService = new clsPostServices(dataManager);
            UserService = new clsUserServices(dataManager);
            FriendService = new clsFriendServices(dataManager);
            MessageService = new clsMessageServices(dataManager);
            AuthenticationService = new clsAuthenticationServices(appState, UserService);
        }
    }
}
