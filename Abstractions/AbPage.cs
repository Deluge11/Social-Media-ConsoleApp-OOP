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
        public virtual string PageName { get; } = "('Page Name')";
        public virtual string DefaultMassage { get; } = "('Default Message')";
        public string[] ContentGrids { get; } = new string[12];

        public abstract void SetPageContent();
        public void ResetContent()
        {
            for (int i = 0; i < ContentGrids.Length; i++)
            {
                ContentGrids[i] = "";
            }
        }
    }
}
