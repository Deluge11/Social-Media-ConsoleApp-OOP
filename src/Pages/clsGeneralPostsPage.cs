using SocialApp.Enums;
using SocialApp.Model;
using SocialApp.Pages.Abstractions;
using SocialApp.Services;
using SocialApp.Structure;

namespace SocialApp.Pages
{
    public class clsGeneralPostsPage : absScrollPage
    {
        public override string PageName => "General Posts";
        protected override string EmptyRowsMessage => "There is no posts";

        public override enPermission AccessPermission => enPermission.None;
        protected override enResetCursor CursorResetCommand => enResetCursor.Up;

        protected override List<stPageRow> GetContentRows()
        {
            return clsPostServices
                .GetAllPosts()
                .Select(p => new stPageRow(
                     p.PosterName,
                     p.PostContent,
                     $"Likes: {p.Likes.Count}{clsCustomTags.LineBreak} Created At {clsCustomTags.LineBreak} {p.Date.ToShortDateString()}"
                 ))
                .ToList();
        }

        protected override stPageRow GetHeaderRow()
        {
            return new stPageRow("Creator", "Content", "Info");
        }
    }
}
