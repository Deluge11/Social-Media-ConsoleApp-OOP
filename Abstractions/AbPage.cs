
using SocialApp.Structure;

namespace SocialApp.Abstractions
{
    public abstract class AbPage
    {
        public abstract string PageName { get; }
        public abstract string DefaultMassage { get; } // Display When There An Odd Behavior (0 Rows , UnAuthenticated ,etc)
        public string[] ContentGrids { get; } = new string[12];

        public abstract void SetPageContent();
        public abstract stPageRow GetPageHeaders();
        public void ResetContent()
        {
            for (int i = 0; i < ContentGrids.Length; i++)
                ContentGrids[i] = "";
        }
        protected void SetPageHeader()
        {
            stPageRow headers = GetPageHeaders();
            ContentGrids[0] = headers.LeftContent;
            ContentGrids[1] = headers.CenterContent;
            ContentGrids[2] = headers.RightContent;
        }
    }
}
