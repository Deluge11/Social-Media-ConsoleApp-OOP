using SocialApp.Abstractions;
using SocialApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Pages
{
    public class About : AbPage
    {
        public override string PageName => "About";
        public override string DefaultMassage => "Default";
        public override void SetPageContent() {
            ContentGrids[1] = PageName;
            ContentGrids[4] = "Hello";
            ContentGrids[7] = "We are the GOATs";
            ContentGrids[10] = "Too Much Aura";
        }
    }
}
