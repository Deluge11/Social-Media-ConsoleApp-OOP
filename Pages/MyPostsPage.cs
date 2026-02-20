using SocialApp.Abstractions;
using SocialApp.Interfaces;
using SocialApp.Model;
using SocialApp.Services;
using SocialApp.Structure;
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

        public override string GetPageCenterHeaders() => "Likes";
        public override string GetPageLeftHeaders() => "Content";
        public override string GetPageRightHeaders() => "Created Date";

        public override List<stPageRow> GetContentRows()
        {
            var myPostsList = PostServices.GetUserPosts(AppState.User.Name);

            return myPostsList.Select(
                p => new stPageRow(
                    p.PostMessage,
                    p.Likes.Count.ToString(),
                    p.Date.ToShortDateString()
                    ))
                .ToList();
        }
        public void Action()
        {
            if (!AppState.IsAuthenticated)
            {
                return;
            }
            PostServices.AddNewPost(AppState.User.Name);
        }

    }
}
