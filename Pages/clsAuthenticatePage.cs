using SocialApp.Abstractions;
using SocialApp.Interfaces;
using SocialApp.Structure;


namespace SocialApp.Pages
{
    public class clsAuthenticatePage : absScrollCursor, IAction, IActionCollector
    {
        public override string PageName => "Authentication Page";
        protected override string EmptyRowsMessage => "There is no scripts";
        public string ActionName => Actions.Count > 0 ? Actions[Cursor].ActionName : "Take action";
        public List<IAction> Actions { get; } = new();


        public void AddAction(IAction action)
        {
            Actions.Add(action);
        }

        public void Execute()
        {
            if (Actions.Count == 0) return;
            Actions[Cursor].Execute();
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
