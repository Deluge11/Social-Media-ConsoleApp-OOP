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
        public override string PageName { get; init; } = "About";

        public override List<string> GetScrollContent()
        {
            List<string> content =
        [
            "Scroll Down",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "Hire Me :)"
        ];
            return content;
        }

        public override int GetScrollContentCount()
        {
            return GetScrollContent().Count;
        }
    }
}
