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
        public virtual string DefaultMassage { get; init; } = "( Default Message )";
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
