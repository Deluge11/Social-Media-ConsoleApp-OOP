using SocialApp.Abstractions;
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
    public class MyPostsPage : AbScrollPage, IAction
    {
        public override string PageName { get; init; } = "My Posts";
        public override string DefaultMassage { get; init; } = "You have no posts!";
        public string ActionName { get; init;     } = "Add new post";
        public PostServices PostServices { get; }
        public AppState AppState { get; }

        public MyPostsPage(AppState appState, PostServices postServices)
        {
            PostServices = postServices;
            AppState = appState;
        }

        public override void SetPageContent()
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

        public override int GetScrollContentCount()
        {
            return PostServices.GetMyPostsCount(AppState.User.Name);
        }

        public override List<string> GetScrollContent()
        {
            return new List<string>();
        }
    }
}
