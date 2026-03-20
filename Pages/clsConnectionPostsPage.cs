using SocialApp.Abstractions;
using SocialApp.Enums;
using SocialApp.Interfaces;
using SocialApp.Structure;

namespace SocialApp.Pages
{
    public class clsConnectionPostsPage : absScrollSelection, IAction, INeedAuthentication
    {
        public override string PageName { get; } = "Connection Posts";
        protected override string EmptyRowsMessage { get; } = "There is no posts! Add new post/friend";
        public string ActionName { get; } = "Like";

        public clsAppState AppState { get; }
        public clsServiceCollection Services { get; }

        public override enPermission AccessPermission => enPermission.None;
        public enPermission ActionPermission => enPermission.Post_Like;
        protected override enResetCursor CursorResetCommand => enResetCursor.Up;


        public clsConnectionPostsPage(clsAppState appState, clsServiceCollection services)
        {
            AppState = appState;
            Services = services;
        }

        public void Execute()
        {
            var postsIdList = Services.PostService.GetNewPosts(AppState.User.Name);

            if (postsIdList.Count == 0)
                return;

            Services.PostService.TogglePostLike(AppState.User.Name, postsIdList[SelectionCursor].Id);
        }

        protected override List<stPageRow> GetContentRows()
        {
            return Services.PostService
                .GetNewPosts(AppState.User.Name)
                .Select(p => new stPageRow(
                    p.PosterName == AppState.User.Name ? "`You`" : p.PosterName,
                    p.PostContent,
                    $"Likes: {p.Likes.Count} {clsCustomTags.LineBreak} Created At {clsCustomTags.LineBreak} {p.Date.ToShortDateString()}"
                ))
                .ToList();
        }

        protected override stPageRow GetHeaderRow()
        {
            return new stPageRow("Poster", "Content", "Information");
        }
    }
}
