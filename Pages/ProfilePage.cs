using SocialApp.Abstractions;
using SocialApp.Interfaces;
using SocialApp.Model;
using SocialApp.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Pages
{
    public class ProfilePage : AbPage
    {
        public override string PageName { get; } = "Profile Page";
        public override string DefaultMassage { get; } = $"Login / Register";
        public AppState AppState { get; }
        public FriendServices FriendService { get; }

        public ProfilePage(AppState appState, FriendServices friendService)
        {
            AppState = appState;
            FriendService = friendService;
            SetPageContent();
        }

        public override void SetPageContent()
        {
            User user = AppState.User;

            ContentGrids[1] = PageName;

            if (user == null)
            {
                ContentGrids[4] = DefaultMassage;
                return;
            }

            ContentGrids[4] = $"Username : {user.Name}";
            ContentGrids[6] = $"Friends Count : {user.Friends.Count}";
            ContentGrids[8] = $"Friend Requests: {FriendService.GetFriendRequistsUsers(user.Name).Count}";
            ContentGrids[9] = $"Posts Count : {user.PostsId.Count}";

        }


    }
}
