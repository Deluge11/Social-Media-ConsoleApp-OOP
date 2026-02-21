using SocialApp.Abstractions;
using SocialApp.Interfaces;
using SocialApp.Structure;


namespace SocialApp.Pages
{
    public class AuthenticatePage : AbScrollCursor, IAction, IActionPage
    {
        public override string PageName { get; } = "Authentication Page";
        public override string DefaultMassage { get; } = "There is no scripts";
        public string ActionName { get; } = "Take action";
        public List<IAction> Actions { get; } = new();


        public void Action()
        {
            if (Cursor >= 0 && Cursor < Actions.Count)
                Actions[Cursor].Action();
        }

        public void AddAction(IAction action)
        {
            Actions.Add(action);
        }

        public override List<stPageRow> GetContentRows()
        {
            return Actions
                .Select(a=>new stPageRow(a.ActionName))
                .ToList();
        }

        public override stPageRow GetPageHeaders()
        {
            return new stPageRow(centerContent:PageName);
        }
    }
}
