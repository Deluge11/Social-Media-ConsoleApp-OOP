
namespace Grids
{
    public abstract class absLineGrid : absBaseGrid
    {
        public abstract int Length { get; }
        public absLineGrid(int width, int height) : base(width, height, new stPaddingInfo(0, 0, 0, 0), new stBoarderInfo(' ', ' ', ' '))
        {
        }

    }
}
