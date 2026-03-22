
namespace SocialApp.Structure
{
    public struct stPaddingInfo
    {
        public int Left { get; }
        public int Top { get; }
        public int Right { get; }
        public int Bottom { get; }

        public stPaddingInfo(int left, int top, int right, int bottom)
        {
            Left = left < 0 ? 0 : left;
            Top = top < 0 ? 0 : top;
            Right = right < 0 ? 0 : right;
            Bottom = bottom < 0 ? 0 : bottom;
        }
    }
}
