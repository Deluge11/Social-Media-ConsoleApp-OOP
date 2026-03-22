using SocialApp.Abstractions.Base;
using SocialApp.Structure;

namespace SocialApp.Grids
{
    public abstract class absScrollBarGrid : absBaseGrid
    {
        protected override stBoarderInfo BoarderInfo => new stBoarderInfo('-', '|', '+');
        protected override stPaddingInfo PaddingInfo => new stPaddingInfo(1, 1, 1, 1);

        private int _skippedItems = 0;
        private int _totalItems = 1;
        private int _visibleItems = 1;

        public int SkippedItems
        {
            get => Math.Max(_skippedItems, 0);
            protected set => _skippedItems = Math.Max(value, 0);
        }

        public int TotalItems
        {
            get => Math.Max(_totalItems, 1);
            protected set => _totalItems = Math.Max(value, 1);
        }

        public int VisibleItems
        {
            get => Math.Max(_visibleItems, 1);
            protected set => _visibleItems = Math.Max(value, 1);
        }


        public void SetScrollBarInformation(int totalItems, int visibleItems, int skippedItems)
        {
            TotalItems = totalItems;
            VisibleItems = visibleItems;
            SkippedItems = skippedItems;
        }

        protected int GetScrollBarLength(int boxLength)
        {
            int scrollBarLength = boxLength * VisibleItems / TotalItems;
            return Math.Max(scrollBarLength, 3);
        }

        protected int GetSkippedLength(int boxLength)
        {
            int maxStartCursor = Math.Max(TotalItems - VisibleItems, 1);
            int maxOffset = boxLength - GetScrollBarLength(boxLength);
            return SkippedItems * maxOffset / maxStartCursor;
        }

    }
}
