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
        public override string PageName { get; init; } = "Authentication Page";
        public override string DefaultMassage { get; init; } = "There is no scripts";
        public List<IAction> Actions { get; } = new();
        public string ActionName { get; init; } = "Take action";


        public void Action()
        {
            if (Cursor >= 0 && Cursor < Actions.Count)
                Actions[Cursor].Action();
        }

        public void AddAction(IAction action)
        {
            Actions.Add(action);
        }
        public override List<string> GetScrollContent()
        {
            return Actions.Select(a => a.ActionName).ToList();
        }
        public override int GetScrollContentCount()
        {
            return Actions.Count;
        }

    }
}
