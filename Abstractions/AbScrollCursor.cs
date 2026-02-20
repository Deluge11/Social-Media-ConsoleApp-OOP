using SocialApp.Interfaces;
using SocialApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Abstractions
{
    public abstract class AbScrollCursor : AbScrollPage
    {
        public int Cursor { get; protected set; }
        public virtual void ResetCursor()
        {
            Cursor = 0;
        }

        public sealed override void ScrollDown()
        {
            if (Cursor < GetContentRows().Count - 1)
                Cursor++;
            if (Cursor > Start + 2)
                Start++;
        }

        public sealed override void ScrollUp()
        {
            if (Cursor > 0)
                Cursor--;
            if (Cursor < Start)
                Start--;
        }
    }
}
