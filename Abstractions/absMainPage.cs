using SocialApp.Interfaces;
using SocialApp.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Abstractions
{
    public abstract class absMainPage : absScrollCursor, IRootPage
    {
        protected override string EmptyRowsMessage { get; } = "There is no pages";
        public List<absPage> Pages { get; } = new List<absPage>();

        public void AddSubPage(absPage page)
        {
            if (page != null)
                Pages.Add(page);
        }

        public absPage Next()
        {
            return Pages.Count > 0 ? Pages[Cursor] : null!;
        }

        protected override List<stPageRow> GetContentRows()
        {
            return Pages
                .Select(p => new stPageRow(p.PageName))
                .ToList();
        }

        protected override stPageRow GetPageHeader()
        {
            return new stPageRow(centerContent: PageName);
        }
    }
}
