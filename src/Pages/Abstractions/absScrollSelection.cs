namespace SocialApp.Pages.Abstractions
{
    public abstract class absScrollSelection : absScrollPage
    {
        public int SelectionCursor { get; protected set; }

        protected sealed override void MoveCursorUp()
        {
            SelectionCursor = 0;
            StartCursor = 0;
        }

        protected sealed override void MoveCursorDown()
        {
            int rowCounts = GetContentRows().Count;

            StartCursor = rowCounts - PAGE_ROWS_LIMIT;
            if (StartCursor < 0)
                StartCursor = 0;

            SelectionCursor = rowCounts - 1;
            if (SelectionCursor < 0)
                SelectionCursor = 0;
        }

        public sealed override void ScrollDown()
        {
            if (SelectionCursor < GetContentRows().Count - 1)
                SelectionCursor++;
            if (SelectionCursor > StartCursor + 2)
                StartCursor++;
        }

        public sealed override void ScrollUp()
        {
            if (SelectionCursor > 0)
                SelectionCursor--;
            if (SelectionCursor < StartCursor)
                StartCursor--;
        }
    }
}
