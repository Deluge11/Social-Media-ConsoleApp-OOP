using SocialApp.Abstractions;
using SocialApp.Interfaces;
using SocialApp.Model;
using SocialApp.Services;
using SocialApp.Structure;

namespace SocialApp.Pages
{
    public class clsNewPostsPage : absScrollCursor, IAction, INeedAuthentication
    {
        public override string PageName { get; } = "New Posts";
        protected override string EmptyRowsMessage { get; } = "There is no posts! Add new post/friend";
        public string ActionName { get; } = "Like";
        public clsAppState AppState { get; }
        public clsServiceCollection Services { get; }

        public clsNewPostsPage(clsAppState appState, clsServiceCollection services)
        {
            AppState = appState;
            Services = services;
        }

        public void Execute()
        {
            var postsIdList = Services.PostService.GetNewPosts(AppState.User.Name);

            if (postsIdList.Count == 0) return;

            Services.PostService.TogglePostLike(AppState.User.Name, postsIdList[Cursor].Id);
        }

        protected override List<stPageRow> GetContentRows()
        {
            return Services.PostService
                .GetNewPosts(AppState.User.Name)
                .Select(p => new stPageRow(
                    p.PosterName == AppState.User.Name ? "`You`" : p.PosterName,
                    p.PostContent,
                    $"Likes: {p.Likes.Count}{clsCustomTags.LineBreak}Created At {clsCustomTags.LineBreak} {p.Date.ToShortDateString()}"
                ))
                .ToList();
        }

        protected override stPageRow GetPageHeader()
        {
            return new stPageRow("Poster", "Content", "Information");
        }
    }
}
