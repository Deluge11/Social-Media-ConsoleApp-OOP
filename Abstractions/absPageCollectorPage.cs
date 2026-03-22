using SocialApp.Abstractions.Base;
using SocialApp.Enums;
using SocialApp.Interfaces;
using SocialApp.Structure;

namespace SocialApp.Abstractions
{
    public abstract class absPageCollectorPage : absScrollSelection, IRootPage
    {
        protected override string EmptyRowsMessage { get; } = "There is no pages";
        public List<absBasePage> Pages { get; } = new List<absBasePage>();


        public void AddSubPage(absBasePage page)
        {
            if (page != null)
                Pages.Add(page);
        }

        public absBasePage Next()
        {
            return Pages.Count > 0 ? Pages[SelectionCursor] : null!;
        }

        protected override List<stPageRow> GetContentRows()
        {
            return Pages
                .Select(p => new stPageRow(p.PageName))
                .ToList();
        }

        protected override stPageRow GetHeaderRow()
        {
            return new stPageRow(centerContent: PageName);
        }
    }
}
