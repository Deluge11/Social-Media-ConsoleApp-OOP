using SocialApp.Enums;
using SocialApp.Interfaces;
using SocialApp.Pages.Abstractions;
using SocialApp.Structure;

namespace SocialApp.Pages
{
    public class clsActionCollectorPage : absScrollSelection, IAction
    {
        public override string PageName { get; }
        protected override string EmptyRowsMessage => "There Is No Scripts";
        public string ActionName => Actions.Count > 0 ? Actions[SelectionCursor].ActionName : "`Unknown`";

        public enPermission ActionPermission => Actions.Count > 0 ? Actions[SelectionCursor].ActionPermission : enPermission.None;
        protected override enResetCursor CursorResetCommand => enResetCursor.Up;
        public override enPermission AccessPermission => enPermission.None;

        public List<IAction> Actions { get; } = new();

        public clsActionCollectorPage(string pageName)
        {
            PageName = pageName;
        }

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
