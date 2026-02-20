using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Structure
{
    public struct stPageRow
    {
        public string LeftContent;
        public string CenterContent;
        public string RightContent;

        public stPageRow(string leftContent = "", string centerContent = "", string rightContent = "")
        {
            LeftContent = leftContent;
            CenterContent = centerContent;
            RightContent = rightContent;
        }
    }
}
