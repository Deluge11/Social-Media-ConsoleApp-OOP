using SocialApp.Abstractions;
using SocialApp.Interfaces;
using SocialApp.Model;
using SocialApp.Structure;


namespace SocialApp.Pages
{
    public class clsProfilePage : absPage, INeedAuthentication
    {
        public override string PageName { get; } = "Profile Page";
        public clsAppState AppState { get; }
        public clsServiceCollection Services { get; }

        public clsProfilePage(clsAppState appState, clsServiceCollection services)
        {
            AppState = appState;
            Services = services;
        }

        protected override void SetPageBody()
        {
            clsUser user = AppState.User;

            ContentGrids[4] = $"Username : {user.Name}";
            ContentGrids[6] = $"Friends Count : {user.Friends.Count}";
            ContentGrids[8] = $"Friend Requests: {Services.FriendService.GetFriendRequestsUsers(user.Name).Count}";
            ContentGrids[9] = $"Posts Count : {user.PostsId.Count}";
            ContentGrids[11] = $"Posts Likes : {Services.PostService.GetPostsTotalLikes(user.Name)}";
        }

        protected override stPageRow GetPageHeader()
        {
            return new stPageRow(centerContent: PageName);
        }
    }
}
