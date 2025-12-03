using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Interfaces
{
    public interface IScrollPage
    {
        int Start { get; }
        void ScrollUp();
        void ScrollDown();
    }
}
