using SocialApp.Abstractions;

using SocialApp.Structure;


namespace SocialApp.Pages
{
    public class About : AbScrollPage
    {
        public override string PageName { get; } = "About";
        protected override string DefaultMessage { get; } = "There is No Content";

        List<string> content =
         [
            "Scroll Down",
            ".",
            ".",
            ".",
            "Put Like On This Video :)",
            "Share This Video",
            "Write A Comment below",
            ".",
            ".",
            ".",
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

        protected override stPageRow GetPageHeader()
        {
            return new stPageRow(centerContent:PageName);
        }
    }
}
