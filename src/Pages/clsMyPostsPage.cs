using SocialApp.Enums;
using SocialApp.HelperTools;
using SocialApp.Interfaces;
using SocialApp.Interfaces.Page;
using SocialApp.Pages.Abstractions;
using SocialApp.Services;
using SocialApp.Structure;


namespace SocialApp.Pages
{
    public class clsMyPostsPage : absScrollPage, IAction, INeedAuthentication
    {
        public override string PageName { get; } = "My Posts";
        protected override string EmptyRowsMessage { get; } = "You have no posts!";
        public string ActionName { get; } = "Add new post";

        public enPermission ActionPermission => enPermission.None;
        public override enPermission AccessPermission => enPermission.None;
        protected override enResetCursor CursorResetCommand => enResetCursor.Up;

        public void Execute()
        {
            Console.Clear();
            clsConsoleUI.PrintMessage("Post Screen");
            string post = clsConsoleInput.GetStringInput("Write New Post");
            Console.Clear();

            if (clsValidation.IsValidPost(post))
            {
                clsPostServices.AddNewPost(clsAppState.User.Name, post);
                clsConsoleUI.PrintMessage("Post Added Successfully");
                ResetCursors();
            }
            else
            {
                clsConsoleUI.PrintMessage("The post should have 5 letters atleast");
            }
            clsConsoleUI.PressKeyToContinue();
        }

        protected override List<stPageRow> GetContentRows()
        {
            return clsPostServices
                .GetUserPosts(clsAppState.User.Name)
                .Select(p => new stPageRow(
                    p.PostContent,
                    p.Likes.Count.ToString(),
                    p.Date.ToShortDateString()))
                .ToList();
        }

        protected override stPageRow GetHeaderRow()
        {
            return new stPageRow("Content", "Likes", "Created Date");
        }

    }
}
