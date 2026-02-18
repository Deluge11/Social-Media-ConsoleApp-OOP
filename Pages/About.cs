using SocialApp.Abstractions;
using SocialApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Pages
{
    public class About : AbScrollPage
    {
        public override string PageName => "About";
        public override string DefaultMassage => "Default";

        private string[] Texts { get; } =
        {
            "Scroll Down",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "Hire Me :)"
          
        };
        public override int GetScrollContentCount()
        {
            return Texts.Length;
        }

        public override void SetPageContent()
        {
            ContentGrids[1] = PageName;
            ContentGrids[4] = Texts[Start];
            ContentGrids[7] = Texts[Start + 1];
            ContentGrids[10] = Texts[Start + 2];
        }
    }
}
