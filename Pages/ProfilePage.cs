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
    public class ProfilePage : IPage
    {
        public string PageName { get; private set; } = "Profile Page";
        public string DefaultMassage { get; } = $"Login/Signin";
        public string[] ContentGrids { get; private set; } = new string[12];
        public AppState AppState { get; }


        public ProfilePage(AppState appState)
        {
            AppState = appState;
            SetPageContent();
        }

        public void SetPageContent()
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

        public void ResetContent()
        {
            for (int i = 0; i < ContentGrids.Length; i++)
            {
                ContentGrids[i] = "";
            }
        }
    }
}
