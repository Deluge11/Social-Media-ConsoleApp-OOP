
using SocialApp.Structure;

namespace SocialApp.Abstractions
{
    public abstract class absPage
    {
        public abstract string PageName { get; }
        public string[] ContentGrids { get; } = new string[12];


        public void SetPageContent()
        {
            ClearContent();
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

        private void ClearContent()
        {
            for (int i = 0; i < ContentGrids.Length; i++)
                ContentGrids[i] = "";
        }

        protected abstract stPageRow GetPageHeader();
        protected abstract void SetPageBody();

    }
}
