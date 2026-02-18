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
    public class PostsPage : AbScrollCursor, IRootPage, IManagePages
    {
        public override string PageName { get; init; } = "Posts Page";
        public override string DefaultMassage { get; init; } = "There is no pages";
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
            return Pages.Select(p => p.PageName).ToList();
        }
    }
}
