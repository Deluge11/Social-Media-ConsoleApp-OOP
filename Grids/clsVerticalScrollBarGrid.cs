namespace SocialApp.Grids
{
    public class clsVerticalScrollBarGrid : absScrollBarGrid
    {
        protected override int ScrollBarBoxLength { get; }
        protected override int ScrollBarBoxWidth { get; }
        protected override char ScrollBarShape { get; }

        public clsVerticalScrollBarGrid(int scrollBarBoxWidth, int scrollBarBoxLength, char scrollBarShape) 
            : base(scrollBarBoxWidth, scrollBarBoxLength)
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
                    ContentBoard[length][width] = ScrollBarShape;
                }
            }
        }
    }
}
