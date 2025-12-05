using SocialApp.Interfaces;
using SocialApp.Model;
using SocialApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Pages
{
    public class AuthenticatePage : IPage, IScrollCursor, IAction, IActionPage
    {
        public string PageName { get; } = "Authentication Page";
        public string DefaultMassage { get; } = "There is no scripts";
        public string[] ContentGrids { get; } = new string[12];
        public int Start { get; private set; }
        public int Cursor { get; private set; }
        public List<IAction> Actions { get; } = new();
        public string ActionName { get; } = "Take action";

        public void Action()
        {
            if (Actions.Count == 0)
                return;
  
            Actions[Cursor].Action();
        }
        public void ScrollDown()
        {
            if (Cursor < Actions.Count - 1)
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
        public void SetPageContent()
        {
            for (int i = 0; i < ContentGrids.Length; i++)
            {
                ContentGrids[i] = "";
            }

            ContentGrids[1] = PageName;

            if (Actions.Count == 0)
            {
                ContentGrids[4] = DefaultMassage;
                return;
            }

            if (Start < Actions.Count)
            {
                ContentGrids[3] = Actions[Start].ActionName;
            }
            if (Start + 1 < Actions.Count)
            {
                ContentGrids[6] = Actions[Start + 1].ActionName;
            }
            if (Start + 2 < Actions.Count)
            {
                ContentGrids[9] = Actions[Start + 2].ActionName;
            }
        }

        public void AddAction(IAction action)
        {
            Actions.Add(action);
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
