using Grids;
using SocialApp.Controllers;
using SocialApp.Interfaces;
using SocialApp.Pages.Abstractions;


namespace SocialApp.Forms
{
    public partial class clsMainForm
    {
        public clsAppState AppState { get; }
        public clsNavigationController NavigationController { get; }

        public clsMainForm(clsAppState appState, clsNavigationController navigationController)
        {
            NavigationController = navigationController;
            AppState = appState;

            InitializeForm();
        }

        public void Print()
        {
            Console.Clear();
            RefreshForm();
            HeaderBarManager.Print();
            CenterGridManager.Print();
            PrintControlKeys();
        }

        protected void RefreshForm()
        {
            DisableContentGridsBorder();

            UpdatePagesStackGrid();
            UpdateContentGrids();
            UpdateSelectedContentGrid();
            UpdateScrollBar();
        }

        protected void UpdatePagesStackGrid()
        {
            PageStackContentGrid.Text = "Pages: " + string.Join(" -> ", NavigationController.GetPagesNames());
        }

        protected void PrintControlKeys()
        {
            absBasePage currentPage = NavigationController.GetCurrentPage();

            if (currentPage is absScrollPage)
            {
                Console.WriteLine("| Press W To Scroll Up");
                Console.WriteLine("| Press S To Scroll Down");
            }
            if (currentPage is IAction action)
            {
                Console.WriteLine($"| Press X To {action.ActionName}");
            }
            else if (currentPage is IRootPage)
            {
                Console.WriteLine($"| Press X To Go Next page");
            }
            if (NavigationController.GetCurrentStackCount() != 1)
            {
                Console.WriteLine($"| Press B To Back Previous Page");
            }
            if (AppState.IsAuthenticated() || AppState.IsGuest)
            {
                Console.WriteLine($"| Press L To Logout");
            }
            {
                Console.WriteLine($"| Press E to Save and Exit");
            }
        }

        protected void DisableContentGridsBorder()
        {
            for (int i = 0; i < ContentGrids.Length; i++)
            {
                ContentGrids[i].BorderShape = enBorderShape.None;
            }
        }

        protected void UpdateContentGrids()
        {
            absBasePage currentPage = NavigationController.GetCurrentPage();
            currentPage.ResetContent();
            string[] content = currentPage.ContentGrids;

            for (int i = 0; i < ContentGrids.Length; i++)
            {
                ContentGrids[i].Text = content[i];
            }
        }

        protected void UpdateScrollBar()
        {
            ScrollBarManager.Visible = false;

            if (NavigationController.GetCurrentPage() is not absScrollPage scrollPage)
                return;

            PageScrollBar.SetScrollBarInformation(scrollPage.GetRowCount(), absScrollPage.PAGE_ROWS_LIMIT, scrollPage.StartCursor);

            ScrollBarManager.Visible = PageScrollBar.IsScrollBarNeeded();
        }

        protected void UpdateSelectedContentGrid()
        {
            if (NavigationController.GetCurrentPage() is not absScrollSelection page)
                return;

            if (page.GetRowCount() == 0)
                return;

            int curserRowNumber = page.SelectionCursor - page.StartCursor + 1;
            ConvertBorderShapeToDash(curserRowNumber, 1);

        }

        protected void ConvertBorderShapeToDash(int row, int col)
        {
            ContentGrids[(row * 3) * col].BorderShape = enBorderShape.Dash;
        }

    }
}
