using SocialApp.Abstractions;
using SocialApp.Abstractions.Base;
using SocialApp.Enums;
using SocialApp.Interfaces;
using SocialApp.Structure;


namespace SocialApp.Pages
{
    public class clsPageCollector : absScrollSelection, IRootPage
    {
        public override string PageName { get; }
        protected override string EmptyRowsMessage { get; } = "There is no pages";

        public override enPermission AccessPermission { get; }
        protected override enResetCursor CursorResetCommand => enResetCursor.Up;

        public List<absBasePage> Pages { get; } = new List<absBasePage>();


        public clsPageCollector(string pageName, enPermission accessPermission = enPermission.None)
        {
            PageName = pageName;
            AccessPermission = accessPermission;
        }

        public void AddSubPage(absBasePage page)
        {
            if (page == null || page == this)
                return;

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

