using SocialApp.Abstractions;
using SocialApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Pages
{
    public class FriendsPage : AbScrollCursor, IRootPage, IManagePages
    {
        public override string PageName { get; } = "Friends Page";
        public override string DefaultMassage { get; } = "There is no pages";
        public List<AbPage> Pages { get; } = new();
        public AppState AppState { get; }


        public FriendsPage(AppState appState)
        {
            SetPageContent();
            AppState = appState;
        }

        public override void SetPageContent()
        {
            ContentGrids[1] = PageName;

            if (Pages.Count == 0)
            {
                ContentGrids[4] = DefaultMassage;
            }

            if (Start < Pages.Count)
            {
                ContentGrids[3] = Pages[Start].PageName;
            }
            if (Start + 1 < Pages.Count)
            {
                ContentGrids[6] = Pages[Start + 1].PageName;
            }
            if (Start + 2 < Pages.Count)
            {
                ContentGrids[9] = Pages[Start + 2].PageName;
            }
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

        public override int GetScrollContentCount()
        {
            return Pages.Count;
        }
    }
}
