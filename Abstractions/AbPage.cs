
using SocialApp.Structure;

namespace SocialApp.Abstractions
{
    public abstract class AbPage
    {
        public abstract string PageName { get; }
        protected abstract string DefaultMessage { get; } // Display When There An Odd Behavior (0 Rows ,etc)
        public string[] ContentGrids { get; } = new string[12];


        public void ClearContent()
        {
            for (int i = 0; i < ContentGrids.Length; i++)
                ContentGrids[i] = "";
        }
        public void SetPageContent()
        {
            SetPageBody();
            SetPageHeader();
        }

        protected void SetPageHeader()
        {
            stPageRow headers = GetPageHeader();
            ContentGrids[0] = headers.LeftContent;
            ContentGrids[1] = headers.CenterContent;
            ContentGrids[2] = headers.RightContent;
        }
        protected abstract stPageRow GetPageHeader();
        protected abstract void SetPageBody();

    }
}
