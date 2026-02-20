using SocialApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Abstractions
{
    public abstract class AbPage
    {
        public abstract string PageName { get; init; }
        public abstract string DefaultMassage { get; init; }
        public string[] ContentGrids { get; } = new string[12];

        public abstract void SetPageContent();
        public virtual string GetPageLeftHeaders() => "";
        public virtual string GetPageRightHeaders() => "";
        public virtual string GetPageCenterHeaders() => PageName;

        public void ResetContent()
        {
            for (int i = 0; i < ContentGrids.Length; i++)
            {
                ContentGrids[i] = "";
            }
        }
        public void SetPageHeader()
        {
            ContentGrids[0] = GetPageLeftHeaders();
            ContentGrids[1] = GetPageCenterHeaders();
            ContentGrids[2] = GetPageRightHeaders();
        }
    }
}
