using SocialApp.Abstractions;
using SocialApp.Interfaces;
using SocialApp.Structure;


namespace SocialApp.Pages
{
    public class clsPostsPage : absScrollCursor, IRootPage, IPageCollector
    {
        public override string PageName { get; } = "Posts Page";
        protected override string EmptyRowsMessage { get; } = "There is no pages";
        public List<absPage> Pages { get; } = new();

        public void AddSubPage(absPage page)
        {
            Pages.Add(page);
        }

        public absPage Next()
        {
            return (Pages.Count > 0 && Cursor >= 0 && Cursor < Pages.Count) ? Pages[Cursor] : null!;
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
