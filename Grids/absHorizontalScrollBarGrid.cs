using SocialApp.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Grids
{
    public abstract class absHorizontalScrollBarGrid : absScrollBarGrid
    {
        protected override int ContentBoardHeight => 1;

        protected override void SetContent()
        {
            int scrollBarLength = GetScrollBarLength(ContentBoardWidth);
            int skippedLength = GetSkippedLength(ContentBoardWidth);

            for (int i = skippedLength; i < ContentBoardWidth && scrollBarLength-- > 0; i++)
            {
                ContentBoard[0][i] = '=';
            }
        }
    }
}
