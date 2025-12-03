using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Interfaces
{
    public interface IPage
    {
        string PageName { get; }
        string[] ContentGrids { get; }
        void SetPageContent();
    }
}
