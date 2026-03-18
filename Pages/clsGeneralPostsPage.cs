
using SocialApp.Abstractions;
using SocialApp.Enums;
using SocialApp.Structure;

namespace SocialApp.Pages
{
    public class clsGeneralPostsPage : absScrollPage
    {
        public override string PageName => "General Posts";
        protected override string EmptyRowsMessage => "There is no posts";
        public clsServiceCollection Services { get; }

        public override enPermission AccessPermission => enPermission.None;

        public clsGeneralPostsPage(clsServiceCollection services)
        {
            Services = services;
        }


        protected override List<stPageRow> GetContentRows()
        {
            return Services.PostService
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
