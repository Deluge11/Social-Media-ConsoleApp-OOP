
using SocialApp.Enums;
using SocialApp.Structure;

namespace SocialApp.Abstractions
{
    public abstract class absBasePage
    {
        public abstract string PageName { get; }
        public string[] ContentGrids { get; } = new string[12];
        public abstract enPermission AccessPermission { get; }


        public abstract void SetContent();

        public void ResetContent()
        {
            ClearContent();
            SetContent();
        }

        private void ClearContent()
        {
            for (int i = 0; i < ContentGrids.Length; i++)
            {
                ContentGrids[i] = "";
            }
        }
    }
}
