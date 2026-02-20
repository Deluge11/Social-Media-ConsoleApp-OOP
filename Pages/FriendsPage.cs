using SocialApp.Abstractions;
using SocialApp.Interfaces;
using SocialApp.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Pages
{
    public class FriendsPage : AbScrollCursor, IRootPage, IManagePages
    {
        public override string PageName { get; init; } = "Friends Page";
        public override string DefaultMassage { get; init; } = "There is no pages";
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


        public override List<stPageRow> GetContentRows()
        {
            return Pages.Select(p => new stPageRow(p.PageName)).ToList();
        }

    }
}
