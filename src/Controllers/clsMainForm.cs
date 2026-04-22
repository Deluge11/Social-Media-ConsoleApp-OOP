using Grids;
using SocialApp.Controllers;
using SocialApp.Interfaces;
using SocialApp.Pages.Abstractions;


namespace SocialApp.Forms
{
    public partial class clsMainForm : IForm
    {
        private clsAppState AppState { get; }
        private clsNavigationController NavigationController { get; }
        public clsMainForm(clsAppState appState, clsNavigationController navigationController)
        {
            InitializeComponent();

            NavigationController = navigationController;
            AppState = appState;
        }

        public void Print()
        {
            Console.Clear();
            RefreshComponents();
            PrintComponents();
            PrintControlKeys();
        }
        private void AfterComponentInitialization()
        {
            SetDefaultValues();
        }

        private void SetDefaultValues()
        {
            AppNameContentGrid.Text = "Social App";
            ScrollBarText.Text = $"SCROLL {clsCustomTags.InvisibleChar} BAR";
        }

        private void RefreshComponents()
        {
            DisableContentGridsBorder();

            UpdatePagesStackGrid();
            UpdateContentGrids();
            UpdateSelectedContentGrid();
            UpdateScrollBar();
        }

        private void UpdatePagesStackGrid()
        {
            var pagesName = NavigationController.GetPagesNames();
            PageStackContentGrid.Text = "Pages: " + string.Join(" -> ", pagesName);
        }

        private void PrintControlKeys()
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

        private void DisableContentGridsBorder()
        {
            for (int i = 0; i < ContentGrids.Length; i++)
            {
                ContentGrids[i].BorderShape = enBorderShape.None;
            }
        }

        private void UpdateContentGrids()
        {
            absBasePage currentPage = NavigationController.GetCurrentPage();
            currentPage.ResetContent();
            string[] content = currentPage.ContentGrids;

            for (int i = 0; i < ContentGrids.Length; i++)
            {
                ContentGrids[i].Text = content[i];
            }
        }

        private void UpdateScrollBar()
        {
            ScrollBarManager.Visible = false;

            if (NavigationController.GetCurrentPage() is not absScrollPage scrollPage)
                return;

            PageScrollBar.SetScrollBarInformation(scrollPage.GetRowCount(), absScrollPage.PAGE_ROWS_LIMIT, scrollPage.StartCursor);

            ScrollBarManager.Visible = PageScrollBar.IsScrollBarNeeded();
        }

        private void UpdateSelectedContentGrid()
        {
            if (NavigationController.GetCurrentPage() is not absScrollSelection page)
                return;

            if (page.GetRowCount() == 0)
                return;

            int curserRowNumber = page.SelectionCursor - page.StartCursor + 1;

            ContentGrids[curserRowNumber * 3].BorderShape = enBorderShape.Dash;
        }
    }
}