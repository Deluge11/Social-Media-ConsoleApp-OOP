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
    public class NewPostsPage : AbScrollCursor, IAction
    {
        public override string PageName { get; } = "New Posts";
        public override string DefaultMassage { get; } = "There is no posts!" + "#h" + "Add new post/friend";
        public string ActionName { get; } = "Like";
        public PostServices PostServices { get; }
        public AppState AppState { get; }

        public NewPostsPage(AppState appState, PostServices postServices)
        {
            PostServices = postServices;
            AppState = appState;
        }

        public override void SetPageContent()
        {
            string name = AppState.User.Name;

            var postsList = PostServices.GetNewPosts(name);

            ContentGrids[0] = "Friend name";
            ContentGrids[1] = "Post Content";
            ContentGrids[2] = "Info";

            if (postsList.Count == 0)
            {
                ContentGrids[4] = DefaultMassage;
                return;
            }

            if (Start < postsList.Count)
            {
                ContentGrids[3] = postsList[Start].PosterName == name ? " 'You' " : postsList[Start].PosterName;
                ContentGrids[4] = postsList[Start].PostMassage;
                ContentGrids[5] = "Likes: " + postsList[Start].Likes.Count.ToString() + "#h" + postsList[Start].Date.ToShortDateString();
            }
            if (Start + 1 < postsList.Count)
            {
                ContentGrids[6] = postsList[Start + 1].PosterName == name ? " 'You' " : postsList[Start + 1].PosterName;
                ContentGrids[7] = postsList[Start + 1].PostMassage;
                ContentGrids[8] = "Likes: " + postsList[Start + 1].Likes.Count.ToString() + "#h" + postsList[Start + 1].Date.ToShortDateString();
            }
            if (Start + 2 < postsList.Count)
            {
                ContentGrids[9] = postsList[Start + 2].PosterName == name ? " 'You' " : postsList[Start + 2].PosterName;
                ContentGrids[10] = postsList[Start + 2].PostMassage;
                ContentGrids[11] = "Likes: " + postsList[Start + 2].Likes.Count.ToString() + "#h" + postsList[Start + 2].Date.ToShortDateString();

            }
        }
        public void Action()
        {
            string username = AppState.User.Name;
            var postsIdList = PostServices.GetNewPosts(username);

            if (postsIdList.Count == 0)
            {
                return;
            }

            Post post = postsIdList[Cursor];

            PostServices.TogglePostLike(username, post.Id);
        }

        public override int GetScrollContentCount()
        {
            return PostServices.GetNewPosts(AppState.User.Name).Count;
        }
    }
}
