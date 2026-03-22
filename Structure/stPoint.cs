
namespace SocialApp.Structure
{
    public struct stPoint
    {
        public int X { get; }
        public int Y { get; }
        public int Z { get; }

        public stPoint(int x, int y, int z = 0)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

}
