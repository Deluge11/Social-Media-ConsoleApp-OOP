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
            if (Start + 3 < GetScrollContentCount())
                Start++;
        }

        public virtual void ScrollUp()
        {
            if (Start > 0)
                Start--;
        }

        public abstract int GetScrollContentCount();

    }
}
