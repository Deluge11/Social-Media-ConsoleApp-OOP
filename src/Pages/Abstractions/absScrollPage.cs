using SocialApp.Enums;
using SocialApp.Structure;


namespace SocialApp.Pages.Abstractions
{
    public abstract class absScrollPage : absBasePage
    {
        public static int PAGE_ROWS_LIMIT => 3;

        public int StartCursor { get; protected set; }
        protected abstract string EmptyRowsMessage { get; }
        protected abstract enResetCursor CursorResetCommand { get; }

        protected abstract stPageRow GetHeaderRow();
        protected abstract List<stPageRow> GetContentRows();

        public virtual void ResetCursors()
        {
            switch (CursorResetCommand)
            {
                case enResetCursor.Up:
                    MoveCursorUp();
                    return;

                case enResetCursor.Down:
                    MoveCursorDown();
                    return;

                default:
                    MoveCursorUp();
                    return;
            }
        }

        public int GetRowCount()
        {
            return GetContentRows().Count();
        }

        protected virtual void MoveCursorUp()
        {
            StartCursor = 0;
        }

        protected virtual void MoveCursorDown()
        {
            StartCursor = GetRowCount() - PAGE_ROWS_LIMIT;
            if (StartCursor < 0)
                StartCursor = 0;
        }

        public virtual void ScrollDown()
        {
            if (StartCursor + PAGE_ROWS_LIMIT < GetRowCount())
                StartCursor++;
        }

        public virtual void ScrollUp()
        {
            if (StartCursor > 0)
                StartCursor--;
        }

        public sealed override void SetContent()
        {
            stPageRow headers = GetHeaderRow();
            List<stPageRow> content = GetContentRows();

            ContentStrings[0] = headers.LeftContent;
            ContentStrings[1] = headers.CenterContent;
            ContentStrings[2] = headers.RightContent;

            if (content.Count == 0)
            {
                ContentStrings[4] = EmptyRowsMessage;
                return;
            }

            if (StartCursor < content.Count)
            {
                ContentStrings[3] = content[StartCursor].LeftContent;
                ContentStrings[4] = content[StartCursor].CenterContent;
                ContentStrings[5] = content[StartCursor].RightContent;
            }
            if (StartCursor + 1 < content.Count)
            {
                ContentStrings[6] = content[StartCursor + 1].LeftContent;
                ContentStrings[7] = content[StartCursor + 1].CenterContent;
                ContentStrings[8] = content[StartCursor + 1].RightContent;

            }
            if (StartCursor + 2 < content.Count)
            {
                ContentStrings[9] = content[StartCursor + 2].LeftContent;
                ContentStrings[10] = content[StartCursor + 2].CenterContent;
                ContentStrings[11] = content[StartCursor + 2].RightContent;
            }
        }
    }
}
