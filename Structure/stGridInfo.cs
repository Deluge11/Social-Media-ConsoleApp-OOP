using SocialApp.Grids.Abstractions;

namespace SocialApp.Structure
{
    public struct stGridInfo
    {
        public absBaseGrid Grid { get; }
        public stPoint Point { get; }

        public stGridInfo(absBaseGrid grid, stPoint point)
        {
            Grid = grid;
            Point = point;
        }
    }
}
