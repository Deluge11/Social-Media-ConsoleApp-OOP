using SocialApp.Abstractions;
using SocialApp.Interfaces;
using SocialApp.Model;
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
        public override string PageName { get; init; } = "Profile Page";
        public override string DefaultMassage { get; init; } = $"Login / Register";
        public AppState AppState { get; }


        public ProfilePage(AppState appState)
        {
            AppState = appState;
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

            ContentGrids[3] = $"Username : {user.Name}";
            ContentGrids[6] = $"Friends count : {user.Friends.Count}";
            ContentGrids[9] = $"Posts count : {user.PostsId.Count}";
        }

   
    }
}
