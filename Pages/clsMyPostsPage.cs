using SocialApp.Abstractions;
using SocialApp.Interfaces;
using SocialApp.Structure;


namespace SocialApp.Pages
{
    public class clsMyPostsPage : absScrollPage, IAction, INeedAuthentication
    {
        public override string PageName { get; } = "My Posts";
        protected override string EmptyRowsMessage { get; } = "You have no posts!";
        public string ActionName { get; } = "Add new post";
        public clsAppState AppState { get; }
        public clsServiceCollection Services { get; }

        public clsMyPostsPage(clsAppState appState, clsServiceCollection services)
        {
            AppState = appState;
            Services = services;
        }

        public void Execute()
        {
            Console.Clear();
            clsConsoleUI.PrintMessage("Post Screen");
            string post = clsConsoleInput.GetStringInput("Write New Post");
            Console.Clear();

            if (clsValidation.IsValidPost(post))
            {
                Services.PostService.AddNewPost(AppState.User.Name, post);
            }
            else
            {
                clsConsoleUI.PrintMessage("The post should have 5 letters atleast");
            }
        }

        protected override List<stPageRow> GetContentRows()
        {
            return Services.PostService
                .GetUserPosts(AppState.User.Name)
                .Select(p => new stPageRow(
                    p.PostContent,
                    p.Likes.Count.ToString(),
                    p.Date.ToShortDateString()))
                .ToList();
        }

        protected override stPageRow GetPageHeader()
        {
            return new stPageRow("Content", "Likes", "Created Date");
        }

    }
}
