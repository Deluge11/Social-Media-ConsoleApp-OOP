using SocialApp.Abstractions;

using SocialApp.Structure;


namespace SocialApp.Pages
{
    public class About : AbScrollPage
    {
        public override string PageName { get; } = "About";
        public override string DefaultMassage { get; } = "There is No Aura";

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
            "Write me a job recommendation :)",
            "Thank You Have A Nice Day :D",
            "Bye :)"
         ];

        public override List<stPageRow> GetContentRows()
        {
            return content
                .Select(c => new stPageRow(centerContent: c))
                .ToList();
        }

        public override stPageRow GetPageHeaders()
        {
            return new stPageRow(centerContent:PageName);
        }
    }
}
