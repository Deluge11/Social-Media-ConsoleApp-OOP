using SocialApp.Enums;

namespace SocialApp.Pages.Abstractions
{
    public abstract class absBasePage
    {
        public abstract string PageName { get; }
        public string[] ContentStrings { get; } = new string[12];
        public abstract enPermission AccessPermission { get; }

        public abstract void SetContent();
        public void ResetContent()
        {
            ClearContent();
            SetContent();
        }

        private void ClearContent()
        {
            for (int i = 0; i < ContentStrings.Length; i++)
            {
                ContentStrings[i] = "";
            }
        }

    }
}
