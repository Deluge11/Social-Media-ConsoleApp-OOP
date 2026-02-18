using SocialApp.Abstractions;
using SocialApp.Interfaces;
using SocialApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Pages
{
    public class HomePage : AbScrollCursor, IRootPage, IManagePages
    {
        public override string PageName { get; init; } = "Home Page";
        public override string DefaultMassage => "There is no pages!";
        public List<AbPage> Pages { get; } = new();
        public AppState AppState { get; }


        public HomePage(AppState appState)
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

        public override int GetScrollContentCount()
        {
            return Pages.Count;
        }

        public override List<string> GetScrollContent()
        {
            return Pages.Select(p=> p.PageName).ToList();
        }
    }
}
