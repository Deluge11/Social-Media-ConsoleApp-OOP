


using SocialApp.Enums;
using SocialApp.Interfaces;
using SocialApp.Structure;

namespace SocialApp.Abstractions
{
    public abstract class absActionCollectorPage : absScrollSelection, IAction
    {
        protected override string EmptyRowsMessage => "There Is No Scripts";
        public string ActionName => Actions.Count > 0 ? 
            Actions[SelectionCursor].ActionName : "`Unknown`";

        public enPermission ActionPermission => Actions.Count > 0 ?
            Actions[SelectionCursor].ActionPermission : enPermission.None;
       
        public List<IAction> Actions { get; } = new();

        public void AddAction(IAction action)
        {
            Actions.Add(action);
        }

        public void Execute()
        {
            if (Actions.Count > 0)
                Actions[SelectionCursor].Execute();
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
