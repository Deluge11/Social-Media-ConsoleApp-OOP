using SocialApp.Abstractions;
using SocialApp.Interfaces;
using SocialApp.Structure;


namespace SocialApp.Pages
{
    public class FriendsPage : AbScrollCursor, IRootPage, IManagePages
    {
        public override string PageName { get; } = "Friends Page";
        protected override string DefaultMessage { get; } = "There is no pages";
        public List<AbPage> Pages { get; } = new();
        public AppState AppState { get; }


        public FriendsPage(AppState appState)
        {
            SetPageContent();
            AppState = appState;
        }

        public void AddPage(AbPage page)
        {
            Pages.Add(page);
        }

        public AbPage Next()
        {
            if (Pages.Count == 0)
            {
                return null;
            }
            return Pages[Cursor];
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
