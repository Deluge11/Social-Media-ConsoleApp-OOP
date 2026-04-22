using SocialApp.Enums;
using SocialApp.Interfaces;
using SocialApp.Model;
using SocialApp.Pages.Abstractions;
using SocialApp.Structure;


namespace SocialApp.Pages
{
    public class clsProfilePage : absBasePage, INeedAuthentication
    {
        public override string PageName { get; } = "Profile Page";

        public override enPermission AccessPermission => enPermission.None;

        public clsAppState AppState { get; }
        public clsServiceCollection Services { get; }


        public clsProfilePage(clsAppState appState, clsServiceCollection services)
        {
            AppState = appState;
            Services = services;
        }

        public override void SetContent()
        {
            clsUser user = AppState.User;

            ContentStrings[1] = PageName;
            ContentStrings[4] = $"Username : {user.Name}";
            ContentStrings[6] = $"Friends Count : {user.Friends.Count}";
            ContentStrings[8] = $"Friend Requests: {Services.FriendService.GetFriendRequestsUsers(user.Name).Count}";
            ContentStrings[9] = $"Posts Count : {user.PostsId.Count}";
            ContentStrings[11] = $"Posts Likes : {Services.PostService.GetPostsTotalLikes(user.Name)}";
        }
    }
}
