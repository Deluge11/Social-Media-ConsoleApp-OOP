using SocialApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Pages
{
    public class About : IPage
    {
        public string PageName => "About";

        public string DefaultMassage => throw new NotImplementedException();

        public string[] ContentGrids => throw new NotImplementedException();

        public void ResetContent()
        {
            throw new NotImplementedException();
        }

        public void SetPageContent()
        {
            throw new NotImplementedException();
        }
    }
}
