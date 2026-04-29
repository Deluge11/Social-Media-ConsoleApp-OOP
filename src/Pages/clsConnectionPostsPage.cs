using SocialApp.Enums;
using SocialApp.Interfaces;
using SocialApp.Interfaces.Page;
using SocialApp.Pages.Abstractions;
using SocialApp.Services;
using SocialApp.Structure;

namespace SocialApp.Pages
{
    public class clsConnectionPostsPage : absScrollSelection, IAction, INeedAuthentication
    {
        public override string PageName { get; } = "Connection Posts";
        protected override string EmptyRowsMessage { get; } = "There is no posts! Add new post/friend";
        public string ActionName { get; } = "Like";

        public override enPermission AccessPermission => enPermission.None;
        public enPermission ActionPermission => enPermission.None;
        protected override enResetCursor CursorResetCommand => enResetCursor.Up;

        public void Execute()
        {
            var postsIdList = clsPostServices.GetNewPosts(clsAppState.User.Name);

            if (postsIdList.Count == 0) return;

            clsPostServices.TogglePostLike(clsAppState.User.Name, postsIdList[SelectionCursor].Id);
        }

        protected override List<stPageRow> GetContentRows()
        {
            return clsPostServices
                .GetNewPosts(clsAppState.User.Name)
                .Select(p => new stPageRow(
                    p.PosterName == clsAppState.User.Name ? "`You`" : p.PosterName,
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
