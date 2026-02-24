using SocialApp.Abstractions;
using SocialApp.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Pages
{
    public class NotFoundPage : AbPage
    {
        public override string PageName => "Page Not Found!";

        public override string DefaultMessage => "Error 404";

        public override stPageRow GetPageHeader()
        {
            return new stPageRow(centerContent:PageName);
        }

        protected override void SetPageBody()
        {
            ContentGrids[4] = DefaultMessage;
        }
    }
}
