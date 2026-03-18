using SocialApp.Abstractions;
using SocialApp.Enums;
using SocialApp.Structure;


namespace SocialApp.Pages
{
    public class clsAboutPage : absScrollPage
    {
        public override string PageName { get; } = "About";
        protected override string EmptyRowsMessage { get; } = "There is No Content";
        public override enPermission AccessPermission => enPermission.None;


        List<string> content =
         [
            "Scroll Down",
            "o",
            "0",
            "O",
            "Put Like On This Video :)",
            "Share This Video",
            "Write A Comment below",
            "o",
            "0",
            "O",
            "Follow Me On LinkedIn :)",
            "Thank You Have A Nice Day :D",
            "Bye :)"
         ];

        protected override List<stPageRow> GetContentRows()
        {
            return content
                .Select(c => new stPageRow(centerContent: c))
                .ToList();
        }

        protected override stPageRow GetHeaderRow()
        {
            return new stPageRow(centerContent: PageName);
        }
    }
}
