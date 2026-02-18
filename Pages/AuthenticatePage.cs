using SocialApp.Abstractions;
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
    public class AuthenticatePage : AbScrollCursor, IAction, IActionPage
    {
        public override string PageName { get; } = "Authentication Page";
        public override string DefaultMassage { get; } = "There is no scripts";
        public List<IAction> Actions { get; } = new();
        public string ActionName { get; } = "Take action";


        public override int GetScrollContentCount() => Actions.Count;

        public void Action()
        {
            if (Cursor >= 0 && Cursor < Actions.Count)
                Actions[Cursor].Action();
        }

        public override void SetPageContent()
        {
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

    }
}
