using SocialApp.Interfaces;
using SocialApp.Services;
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
            if (Start + 3 < this.GetScrollContentCount())
                Start++;
        }

        public virtual void ScrollUp()
        {
            if (Start > 0)
                Start--;
        }

        public override void SetPageContent()
        {
            List<string> content = GetScrollContent();
            ContentGrids[1] = PageName;

            if (content.Count == 0)
                ContentGrids[4] = DefaultMassage;
            if (content.Count > 0)
                ContentGrids[3] = content[Start];
            if (content.Count > 1)
                ContentGrids[6] = content[Start + 1];
            if (content.Count > 2)
                ContentGrids[9] = content[Start + 2];
        }
        public abstract int GetScrollContentCount();
        public abstract List<string> GetScrollContent();

    }
}
