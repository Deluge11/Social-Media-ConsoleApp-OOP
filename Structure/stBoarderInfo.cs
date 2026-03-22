
namespace SocialApp.Structure
{
    public struct stBoarderInfo
    {
        public char Horizontal { get; }
        public char Vertical { get; }
        public char Corner { get; }

        public stBoarderInfo(char horizontal = ' ', char vertical = ' ', char corner = ' ')
        {
            Horizontal = horizontal;
            Vertical = vertical;
            Corner = corner;
        }
    }
}
