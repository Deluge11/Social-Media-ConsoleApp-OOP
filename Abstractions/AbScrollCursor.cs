

namespace SocialApp.Abstractions
{
    public abstract class AbScrollCursor : AbScrollPage
    {
        protected int Cursor { get; set; }
        public override void Reset()
        {
            Cursor = 0;
            Start = 0;
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
