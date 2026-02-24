using SocialApp.Abstractions;
using SocialApp.Model;
using SocialApp.Services;
using SocialApp.Structure;


namespace SocialApp.Pages
{
    public class ProfilePage : AbPage
    {
        public override string PageName { get; } = "Profile Page";
        public override string DefaultMessage { get; } = $"Login / Register";
        public FriendServices FriendService { get; }
        public PostServices PostServices { get; }
        public AppState AppState { get; }

        public ProfilePage(AppState appState, FriendServices friendService, PostServices postServices)
        {
            AppState = appState;
            FriendService = friendService;
            PostServices = postServices;
            SetPageContent();
        }

        protected override void SetPageBody()
        {
            User user = AppState.User;

            if (user == null)
            {
                ContentGrids[4] = DefaultMessage;
                return;
            }

            ContentGrids[4] = $"Username : {user.Name}";
            ContentGrids[6] = $"Friends Count : {user.Friends.Count}";
            ContentGrids[8] = $"Friend Requests: {FriendService.GetFriendRequestsUsers(user.Name).Count}";
            ContentGrids[9] = $"Posts Count : {user.PostsId.Count}";
            ContentGrids[11] = $"Posts Likes : {PostServices.GetPostsTotalLikes(user.Name)}";

        }

        public override stPageRow GetPageHeader()
        {
            return new stPageRow(centerContent: PageName);
        }
    }
}
