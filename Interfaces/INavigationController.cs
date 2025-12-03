using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Interfaces
{
    public interface INavigationController
    {
        void GoNext(IPage next);
        void GoBack();
        IPage GetCurrentPage();
        int GetStackCount();
    }
}
