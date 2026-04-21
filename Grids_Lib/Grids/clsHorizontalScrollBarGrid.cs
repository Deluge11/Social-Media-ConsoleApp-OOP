
namespace Grids
{
    public class clsHorizontalScrollBarGrid : absScrollBarGrid
    {
        protected override int ScrollBarBoxLength { get; }
        protected override int ScrollBarBoxWidth { get; }
        protected override char ScrollBarShape { get; }

        public clsHorizontalScrollBarGrid(int scrollBarBoxWidth, int scrollBarBoxLength, char scrollBarShape) 
            : base(scrollBarBoxLength, scrollBarBoxWidth)
        {
            ScrollBarBoxWidth = scrollBarBoxWidth;
            ScrollBarBoxLength = scrollBarBoxLength;
            ScrollBarShape = scrollBarShape;
        }


        protected sealed override void SetContent()
        {
            int barLength = GetBarLength();
            int skippedLength = GetSkippedLength();
            int endPoint = skippedLength + barLength;

            for (int length = skippedLength; length < ScrollBarBoxLength && length < endPoint; length++)
            {
                for (int width = 0; width < ScrollBarBoxWidth; width++)
                {
                    ContentBoard[width][length] = ScrollBarShape;
                }
            }
        }
    }
}
