using SocialApp.Abstractions;
using SocialApp.Interfaces;
using SocialApp.Model;
using SocialApp.Services;
using SocialApp.Structure;

namespace SocialApp.Pages
{
    public class NewPostsPage : AbScrollCursor, IAction
    {
        public override string PageName { get; } = "New Posts";
        public override string DefaultMessage { get; } = "There is no posts! Add new post/friend";
        public string ActionName { get; } = "Like";
        public PostServices PostServices { get; }
        public AppState AppState { get; }

        public NewPostsPage(AppState appState, PostServices postServices)
        {
            PostServices = postServices;
            AppState = appState;
        }

        public void Action()
        {
            var postsIdList = PostServices.GetNewPosts(AppState.User.Name);

            if (postsIdList.Count != 0)
                PostServices.TogglePostLike(AppState.User.Name, postsIdList[Cursor].Id);
        }

        public override List<stPageRow> GetContentRows()
        {
            return PostServices
                .GetNewPosts(AppState.User.Name)
                .Select(p => new stPageRow(
                    p.PosterName == AppState.User.Name ? "`You`" : p.PosterName,
                    p.PostContent,
                    $"Likes: {p.Likes.Count}#hCreated At {p.Date.ToShortDateString()}"
                ))
                .ToList();
        }

        public override stPageRow GetPageHeader()
        {
            return new stPageRow("Poster", "Content", "Information");
        }
    }
}
