using SocialApp.Abstractions;
using SocialApp.Interfaces;
using SocialApp.Structure;


namespace SocialApp.Pages
{
    public class PostsPage : AbScrollCursor, IRootPage, IManagePages
    {
        public override string PageName { get; } = "Posts Page";
        protected override string DefaultMessage { get; } = "There is no pages";
        public List<AbPage> Pages { get; } = new();

        public PostsPage(AppState appState)
        {
            SetPageContent();
        }
        public void AddPage(AbPage page)
        {
            Pages.Add(page);
        }

        public AbPage Next()
        {
            return Pages.Count > 0 ? Pages[Cursor] : null!;
        }

        protected override List<stPageRow> GetContentRows()
        {
            return Pages
                .Select(p => new stPageRow(p.PageName))
                .ToList();
        }

        protected override stPageRow GetPageHeader()
        {
            return new stPageRow(centerContent: PageName);
        }
    }
}
