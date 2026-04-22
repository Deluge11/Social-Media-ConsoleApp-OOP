using SocialApp.Enums;
using SocialApp.Interfaces.Page;
using SocialApp.Pages.Abstractions;
using SocialApp.Structure;

namespace SocialApp.Pages
{
    public class TestPage : absScrollPage, IAction
    {
        public override string PageName => "Test Page";
        protected override string EmptyRowsMessage => "There is no Rows";
        public string ActionName => "Add New Row";

        public enPermission ActionPermission => enPermission.None;
        public override enPermission AccessPermission => enPermission.None;
        protected override enResetCursor CursorResetCommand => enResetCursor.Down;


        List<stPageRow> Rows = new List<stPageRow>();

        protected override List<stPageRow> GetContentRows()
        {
            return Rows;
        }

        protected override stPageRow GetHeaderRow()
        {
            return new stPageRow(centerContent: PageName);
        }

        public void Execute()
        {
            this.Rows.Add(new stPageRow("Row Number: " + Rows.Count));
            this.ResetCursors();
        }
    }
}
