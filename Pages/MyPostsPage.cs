using SocialApp.Interfaces;
using SocialApp.Model;
using SocialApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Pages
{
    public class MyPostsPage : IPage, IScrollPage, IAction
    {
        public string PageName { get; private set; } = "My Posts";
        public string DefaultMassage { get; } = "You have no posts!";
        public string[] ContentGrids { get; private set; } = new string[12];
        public int Start { get; private set; }
        public PostServices PostServices { get; }
        public AppState AppState { get; }
        public string ActionName { get; } = "Add new post";

        public MyPostsPage(AppState appState, PostServices postServices)
        {
            PostServices = postServices;
            AppState = appState;
        }

        public void ScrollDown()
        {
            string username = AppState.User.Name;
            if (Start < PostServices.GetMyPostsCount(username) - 3)
                Start++;
        }
        public void ScrollUp()
        {
            if (Start > 0)
                Start--;
        }
        public void SetPageContent()
        {
          
            var postsList = PostServices.GetUserPosts(AppState.User.Name);

            ContentGrids[0] = "Post Content";
            ContentGrids[1] = "Likes";
            ContentGrids[2] = "Date Created";

            if (postsList.Count == 0)
            {
                ContentGrids[4] = DefaultMassage;
                return;
            }

            if (Start < postsList.Count)
            {
                ContentGrids[3] = postsList[Start].PostMassage;
                ContentGrids[4] = postsList[Start].Likes.Count.ToString();
                ContentGrids[5] = postsList[Start].Date.ToShortDateString();
            }
            if (Start + 1 < postsList.Count)
            {
                ContentGrids[6] = postsList[Start + 1].PostMassage;
                ContentGrids[7] = postsList[Start + 1].Likes.Count.ToString();
                ContentGrids[8] = postsList[Start + 1].Date.ToShortDateString();
            }
            if (Start + 2 < postsList.Count)
            {
                ContentGrids[9] = postsList[Start + 2].PostMassage;
                ContentGrids[10] = postsList[Start + 2].Likes.Count.ToString();
                ContentGrids[11] = postsList[Start + 2].Date.ToShortDateString();
            }
        }

        public void Action()
        {
            if (!AppState.IsAuthenticated)
            {
                return;
            }
            PostServices.AddNewPost(AppState.User.Name);
        }
        public void ResetStart()
        {
            Start = 0;
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
