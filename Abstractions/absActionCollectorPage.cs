


using SocialApp.Enums;
using SocialApp.Interfaces;
using SocialApp.Structure;

namespace SocialApp.Abstractions
{
    public abstract class absActionCollectorPage : absScrollCursor, IAction
    {
        protected override string EmptyRowsMessage => "There Is No Scripts";
        public string ActionName => Actions.Count > 0 ? Actions[Cursor].ActionName : "Take action";

        public enPermission ActionPermission => Actions[Cursor].ActionPermission;
        public override enPermission AccessPermission => enPermission.None;

        public List<IAction> Actions { get; } = new();


        public void AddAction(IAction action)
        {
            Actions.Add(action);
        }

        public void Execute()
        {
            if (Actions.Count > 0)
                Actions[Cursor].Execute();
        }

        protected override List<stPageRow> GetContentRows()
        {
            return Actions
                .Select(a => new stPageRow(a.ActionName))
                .ToList();
        }

        protected override stPageRow GetHeaderRow()
        {
            return new stPageRow(centerContent: PageName);
        }

    }
}
