using SocialApp.Interfaces;
using SocialApp.Services;
using SocialApp.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Abstractions
{
    public abstract class AbScrollPage : AbPage
    {
        public int Start { get; protected set; }

        public virtual void ResetStart()
        {
            Start = 0;
        }

        public virtual void ScrollDown()
        {
            if (Start + 3 < GetContentRows().Count)
                Start++;
        }

        public virtual void ScrollUp()
        {
            if (Start > 0)
                Start--;
        }

        public sealed override void SetPageContent()
        {
            SetPageHeader();

            List<stPageRow> content = GetContentRows();
            int rowCount = content.Count;

            if(rowCount == 0)
            {
                ContentGrids[4] = DefaultMassage;
            }

            if (Start < rowCount)
            {
                ContentGrids[3] = content[Start].LeftContent;
                ContentGrids[4] = content[Start].CenterContent;
                ContentGrids[5] = content[Start].RightContent;
            }
            if (Start + 1 < rowCount)
            {
                ContentGrids[6] = content[Start + 1].LeftContent;
                ContentGrids[7] = content[Start + 1].CenterContent;
                ContentGrids[8] = content[Start + 1].RightContent;

            }
            if (Start + 2 < rowCount)
            {
                ContentGrids[9] = content[Start + 2].LeftContent;
                ContentGrids[10] = content[Start + 2].CenterContent;
                ContentGrids[11] = content[Start + 2].RightContent;
            }
        }
        public abstract List<stPageRow> GetContentRows();

    }
}
