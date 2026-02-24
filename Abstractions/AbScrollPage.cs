
using SocialApp.Structure;


namespace SocialApp.Abstractions
{
    public abstract class AbScrollPage : AbPage
    {
        protected int Start { get; set; }
        protected abstract List<stPageRow> GetContentRows();

        public virtual void Reset()
        {
            Start = 0;
        }

        public virtual void ScrollDown()
        {
            if (Start + 3 < GetContentRows().Count)
                Start++;
        }

        public virtual void ScrollUp()
        {
            if (Start > 0)
                Start--;
        }

        protected sealed override void SetPageBody()
        {
            List<stPageRow> content = GetContentRows();

            if(content.Count == 0)
            {
                ContentGrids[4] = DefaultMessage;
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
