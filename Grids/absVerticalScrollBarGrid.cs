using SocialApp.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Grids
{
    public abstract class absVerticalScrollBarGrid : absScrollBarGrid
    {
        protected override int ContentBoardWidth => 1;

        protected override void SetContent()
        {
            int scrollBarLength = GetScrollBarLength(ContentBoardHeight);
            int skippedLength = GetSkippedLength(ContentBoardHeight);

            for (int i = skippedLength; i < ContentBoardHeight && scrollBarLength-- > 0; i++)
            {
                ContentBoard[i][0] = '=';
            }
        }
    }
}
