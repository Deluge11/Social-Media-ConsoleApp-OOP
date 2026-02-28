using SocialApp.Abstractions;
using SocialApp.Interfaces;
using SocialApp.Model;
using SocialApp.Services;
using SocialApp.Structure;


namespace SocialApp.Pages
{
    public class MyPostsPage : AbScrollPage, IAction
    {
        public override string PageName { get; } = "My Posts";
        protected override string DefaultMessage { get; } = "You have no posts!";
        public string ActionName { get; } = "Add new post";
        public PostServices PostServices { get; }
        public AppState AppState { get; }

        public MyPostsPage(AppState appState, PostServices postServices)
        {
            PostServices = postServices;
            AppState = appState;
        }

        public void Action()
        {
            if (!AppState.IsAuthenticated)
                return;

            clsConsoleUI.PrintMessage("Post Screen");
            string post = clsConsoleInput.GetStringInput("Write New Post");

            if (clsInputValidation.IsValidPost(post))
            {
                PostServices.AddNewPost(AppState.User.Name, post);
            }
            else
            {
                clsConsoleUI.PrintMessage("The post should have 5 letters atleast");
            }
        }

        protected override List<stPageRow> GetContentRows()
        {
            return PostServices
                .GetUserPosts(AppState.User.Name)
                .Select(
                p => new stPageRow(
                    p.PostContent,
                    p.Likes.Count.ToString(),
                    p.Date.ToShortDateString()
                    ))
                .ToList();
        }

        protected override stPageRow GetPageHeader()
        {
            return new stPageRow("Content", "Likes", "Created Date");
        }

    }
}
