
using SocialApp.Structure;


namespace SocialApp.Abstractions
{
    public abstract class absScrollPage : absPage
    {
        protected const int PAGE_ROWS_LIMIT = 3;
        protected abstract string EmptyRowsMessage { get; }
        public int Start { get; protected set; }


        protected abstract stPageRow GetHeaderRow();
        protected abstract List<stPageRow> GetContentRows();


        public sealed override void SetContent()
        {
            SetHeader();
            SetBody();
        }

        public virtual void ResetPointers()
        {
            Start = 0;
        }

        public virtual void ScrollDown()
        {
            if (Start + PAGE_ROWS_LIMIT < GetContentRows().Count)
                Start++;
        }

        public virtual void ScrollUp()
        {
            if (Start > 0)
                Start--;
        }


        protected void SetHeader()
        {
            stPageRow headers = GetHeaderRow();

            ContentGrids[0] = headers.LeftContent;
            ContentGrids[1] = headers.CenterContent;
            ContentGrids[2] = headers.RightContent;
        }

        protected void SetBody()
        {
            List<stPageRow> content = GetContentRows();

            if (content.Count == 0)
            {
                ContentGrids[4] = EmptyRowsMessage;
            }

            if (Start < content.Count)
            {
                ContentGrids[3] = content[Start].LeftContent;
                ContentGrids[4] = content[Start].CenterContent;
                ContentGrids[5] = content[Start].RightContent;
            }
            if (Start + 1 < content.Count)
            {
                ContentGrids[6] = content[Start + 1].LeftContent;
                ContentGrids[7] = content[Start + 1].CenterContent;
                ContentGrids[8] = content[Start + 1].RightContent;

            }
            if (Start + 2 < content.Count)
            {
                ContentGrids[9] = content[Start + 2].LeftContent;
                ContentGrids[10] = content[Start + 2].CenterContent;
                ContentGrids[11] = content[Start + 2].RightContent;
            }
        }
    }
}
