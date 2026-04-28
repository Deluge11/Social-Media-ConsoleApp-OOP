
namespace Grids
{
    public abstract class absScrollBarGrid : absBaseGrid
    {
        private short MINIMUM_SCROLLBAR_LENGTH = 3;
        protected abstract int ScrollBarBoxLength { get; }
        protected abstract int ScrollBarBoxWidth { get; }
        protected abstract char ScrollBarShape { get; }
        protected bool IsVisibleLessThanTotal => VisibleItems < TotalItems;
        public bool IsScrollBarNeeded => IsVisibleLessThanTotal;


        private int _skippedItems = 0;
        private int _totalItems = 1;
        private int _visibleItems = 1;

        protected absScrollBarGrid(int width, int height) :
            base(width, height, new stPaddingInfo(1, 1, 1, 1), new stBoarderInfo('-', '|', '+'))
        {

        }

        public int SkippedItems
        {
            get => Math.Max(_skippedItems, 0);
            set
            {
                _skippedItems = Math.Max(value, 0);
                UpdateBoard();
            }
        }

        public int TotalItems
        {
            get => Math.Max(_totalItems, 1);
            set
            {
                _totalItems = Math.Max(value, 1);
                UpdateBoard();
            }
        }

        public int VisibleItems
        {
            get => Math.Max(_visibleItems, 1);
            set
            {
                _visibleItems = Math.Max(value, 1);
                UpdateBoard();
            }
        }

        protected int GetBarLength()
        {
            int scrollBarLength = ScrollBarBoxLength * VisibleItems / TotalItems;
            return Math.Max(scrollBarLength, MINIMUM_SCROLLBAR_LENGTH);
        }

        protected int GetSkippedLength()
        {
            int maxStartCursor = Math.Max(TotalItems - VisibleItems, 1);
            int maxOffset = ScrollBarBoxLength - GetBarLength();
            return SkippedItems * maxOffset / maxStartCursor;
        }

    }
}
