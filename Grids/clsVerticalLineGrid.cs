

namespace SocialApp.Grids
{
    public class clsVerticalLineGrid : absLineGrid
    {
        public override int Length { get; }
        public clsVerticalLineGrid(int length) : base(1, length)
        {
            Length = length;
        }

        protected override void SetContent()
        {
            for (int i = 0; i < Length; i++)
            {
                ContentBoard[i][0] = '|';
            }
        }
    }
}
