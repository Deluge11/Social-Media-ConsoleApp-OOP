using SocialApp.Abstractions;
using SocialApp.Interfaces;
using SocialApp.Structure;


namespace SocialApp.Pages
{
    public class AuthenticatePage : AbScrollCursor, IAction, IActionCollector
    {
        public override string PageName { get; } = "Authentication Page";
        protected override string DefaultMessage { get; } = "There is no scripts";
        public string ActionName { get; } = "Take action";
        public List<IAction> Actions { get; } = new();


        public void AddAction(IAction action)
        {
            Actions.Add(action);
        }

        public void Action()
        {
            if (Cursor >= 0 && Cursor < Actions.Count)
                Actions[Cursor].Action();
        }

        protected override List<stPageRow> GetContentRows()
        {
            return Actions
                .Select(a => new stPageRow(a.ActionName))
                .ToList();
        }

        protected override stPageRow GetPageHeader()
        {
            return new stPageRow(centerContent: PageName);
        }

       
    }
}
