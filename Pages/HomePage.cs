using SocialApp.Abstractions;
using SocialApp.Interfaces;
using SocialApp.Structure;


namespace SocialApp.Pages
{
    public class HomePage : AbScrollCursor, IRootPage, IPageCollector
    {
        public override string PageName { get; } = "Home Page";
        protected override string DefaultMessage { get; } = "There is no pages";
        public List<AbPage> Pages { get; } = new();


        public void AddPage(AbPage page)
        {
            Pages.Add(page);
        }

        public AbPage Next()
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
