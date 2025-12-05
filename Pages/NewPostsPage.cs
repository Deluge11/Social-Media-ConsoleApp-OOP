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
    public class NewPostsPage : IPage, IScrollCursor, IAction
    {
        public string PageName { get; private set; } = "New Posts";
        public string DefaultMassage { get; } = "There is no posts!" + "#h" + "Add new post/friend";
        public string[] ContentGrids { get; private set; } = new string[12];
        public PostServices PostServices { get; }
        public AppState AppState { get; }
        public int Cursor { get; private set; }
        public int Start { get; private set; }
        public string ActionName { get; } = "Like";

        public NewPostsPage(AppState appState, PostServices postServices)
        {
            PostServices = postServices;
            AppState = appState;
        }


        public void ScrollDown()
        {
            var postsCount = PostServices.GetNewPosts(AppState.User.Name).Count;

            if (Cursor < postsCount - 1)
                Cursor++;
            if (Cursor > Start + 2)
                Start++;
        }
        public void ScrollUp()
        {
            if (Cursor > 0)
                Cursor--;
            if (Cursor < Start)
                Start--;
        }

        public void SetPageContent()
        {
            for (int i = 0; i < ContentGrids.Length; i++)
            {
                ContentGrids[i] = "";
            }

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
        public void ResetCursor()
        {
            Cursor = 0;
        }
        public void ResetStart()
        {
            Start = 0;
        }

    }
}
