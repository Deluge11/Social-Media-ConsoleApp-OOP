
namespace Grids
{
    public abstract class absScrollBarGrid : absBaseGrid
    {
        protected abstract int ScrollBarBoxLength { get; }
        protected abstract int ScrollBarBoxWidth { get; }

        protected bool IsVisibleLessThanTotal => VisibleItems < TotalItems;

        protected abstract char ScrollBarShape { get; }


        private int _skippedItems = 0;
        private int _totalItems = 1;
        private int _visibleItems = 1;

        protected absScrollBarGrid(int width, int height) 
            : base(width, height, new stPaddingInfo(1, 1, 1, 1), new stBoarderInfo('-', '|', '+'))
        {

        }

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

        protected int GetBarLength()
        {
            int scrollBarLength = ScrollBarBoxLength * VisibleItems / Math.Max(TotalItems, 1);
            return Math.Max(scrollBarLength, 3);
        }

        protected int GetSkippedLength()
        {
            int maxStartCursor = Math.Max(TotalItems - VisibleItems, 1);
            int maxOffset = ScrollBarBoxLength - GetBarLength();
            return SkippedItems * maxOffset / maxStartCursor;
        }

        public bool IsScrollBarNeeded()
        {
            return TotalItems > VisibleItems;
        }
    }
}
