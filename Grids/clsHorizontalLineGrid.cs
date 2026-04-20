using SocialApp.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Grids
{
    public class clsHorizontalLineGrid : absLineGrid
    {
        public override int Length { get; }
        public clsHorizontalLineGrid(int length) : base(length, 1)
        {
            Length = length;
        }

        protected override void SetContent()
        {
            for (int i = 0; i < Length; i++)
            {
                ContentBoard[0][i] = '-';
            }
        }
    }
}
