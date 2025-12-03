using SocialApp.Interfaces;
using SocialApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Pages
{
    public class HomePage : IPage, IScrollCursor, IRootPage, IManagePages
    {
        public string PageName { get; private set; } = "Home Page";
        public string DefaultMassage { get; private set; } = "There is no pages!";
        public string[] ContentGrids { get; private set; } = new string[12];
        public int Start { get; private set; }
        public int Cursor { get; private set; }

        public List<IPage> Pages { get; } = new();
        public AppState AppState { get; }

        public HomePage(AppState appState)
        {
            SetPageContent();
            AppState = appState;
        }

        public void SetPageContent()
        {
            for (int i = 0; i < ContentGrids.Length; i++)
            {
                ContentGrids[i] = "";
            }

            ContentGrids[1] = PageName;

            if (Pages.Count == 0)
            {
                ContentGrids[4] = DefaultMassage;
                return;
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
        public void ScrollDown()
        {
            if (Cursor < Pages.Count - 1)
                Cursor++;
            if (Cursor > Start + 2)
                Start++;
        }
        public void ScrollUp()
        {
            if (Cursor > 0)
                Cursor--;
            if (Cursor < Start)
                Start--;
        }

        public void AddPage(IPage page)
        {
            Pages.Add(page);
        }

        public IPage Next()
        {
            return Pages[Cursor];
        }
        public void ResetCursor()
        {
            Cursor = 0;
        }
        public void ResetStart()
        {
            Start = 0;
        }
    }
}
