using SocialApp.Abstractions;
using SocialApp.Interfaces;
using SocialApp.Model;
using SocialApp.Services;
using SocialApp.Structure;
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
        public override string PageName { get; init; } = "New Posts";
        public override string DefaultMassage { get; init; } = "There is no posts! Add new post/friend";
        public string ActionName { get; init; } = "Like";
        public PostServices PostServices { get; }
        public AppState AppState { get; }

        public NewPostsPage(AppState appState, PostServices postServices)
        {
            PostServices = postServices;
            AppState = appState;
        }

        public override string GetPageLeftHeaders() => "Poster";
        public override string GetPageCenterHeaders() => "Content";
        public override string GetPageRightHeaders() => "Information";
        public void Action()
        {
            string username = AppState.User.Name;
            var postsIdList = PostServices.GetNewPosts(username);

            if (postsIdList.Count == 0) return;

            Post post = postsIdList[Cursor];

            PostServices.TogglePostLike(username, post.Id);
        }

        public override List<stPageRow> GetContentRows()
        {
            string currentUserName = AppState.User.Name;
            var postsList = PostServices.GetNewPosts(currentUserName);

            return postsList
                .Select(p => new stPageRow(
                    p.PosterName == currentUserName ? "`Me`" : p.PosterName,
                    p.PostMessage,
                    $"Likes: {p.Likes.Count}#hCreated At {p.Date.ToShortDateString()}"
                ))
                .ToList();
        }
    }
}
