using SocialApp.Abstractions;
using SocialApp.Interfaces;
using SocialApp.Services;
using SocialApp.Structure;


namespace SocialApp.Pages
{
    public class MyPostsPage : AbScrollPage, IAction
    {
        public override string PageName { get; } = "My Posts";
        public override string DefaultMassage { get; } = "You have no posts!";
        public string ActionName { get; } = "Add new post";
        public PostServices PostServices { get; }
        public AppState AppState { get; }

        public MyPostsPage(AppState appState, PostServices postServices)
        {
            PostServices = postServices;
            AppState = appState;
        }

        public void Action()
        {
            if (AppState.IsAuthenticated)
                PostServices.AddNewPost(AppState.User.Name);
        }

        public override List<stPageRow> GetContentRows()
        {
            return PostServices
                .GetUserPosts(AppState.User.Name)
                .Select(
                p => new stPageRow(
                    p.PostMessage,
                    p.Likes.Count.ToString(),
                    p.Date.ToShortDateString()
                    ))
                .ToList();
        }

        public override stPageRow GetPageHeaders()
        {
            return new stPageRow("Content", "Likes", "Created Date");
        }

    }
}
