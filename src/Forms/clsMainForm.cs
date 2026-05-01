using Grids;
using SocialApp.Controllers;
using SocialApp.HelperTools;
using SocialApp.Interfaces.Form;
using SocialApp.Interfaces.Page;
using SocialApp.Pages.Abstractions;


namespace SocialApp.Forms
{
    public partial class clsMainForm : IForm
    {
        private clsNavigationController NavigationController { get; }
        public clsMainForm(clsNavigationController navigationController)
        {
            InitializeComponent();

            NavigationController = navigationController;
        }

        private void FormOnLoad()
        {
            SetDefaultValues();
        }

        private void SetDefaultValues()
        {
            tbAppName.Text = "Social App";
            tbScrollBarText.Text = $"SCROLL=BAR";
        }

        private void UpdateComponents()
        {
            ResetPageContent();

            DisableContentGridsBorder();

            UpdateScrollBar();
            UpdateControlKeys();
            UpdateContentGrids();
            UpdateRowCountText();
            UpdatePagesNavigation();
            UpdateScrollingRangeText();
            UpdateSelectedContentGrid();
        }

        private void ResetPageContent()
        {
            NavigationController.GetCurrentPage().ResetContent();
        }

        private void UpdatePagesNavigation()
        {
            tbPageNavigation.Text = "Pages: " + string.Join(" -> ", NavigationController.GetPagesNames());
        }

        private void UpdateControlKeys()
        {
            tbControlKeys.Text = "";

            absBasePage currentPage = NavigationController.GetCurrentPage();

            if (currentPage is absScrollPage)
            {
                tbControlKeys.Text += "| Press W To Scroll Up" + clsCustomTags.LineBreak;
                tbControlKeys.Text += "| Press S To Scroll Down" + clsCustomTags.LineBreak;
            }
            if (currentPage is IAction action)
            {
                tbControlKeys.Text += $"| Press X To {action.ActionName}" + clsCustomTags.LineBreak;
            }
            else if (currentPage is IRootPage)
            {
                tbControlKeys.Text += $"| Press X To Go Next page" + clsCustomTags.LineBreak;
            }
            if (NavigationController.GetCurrentStackCount() > 1)
            {
                tbControlKeys.Text += $"| Press B To Back Previous Page" + clsCustomTags.LineBreak;
            }
            if (clsAppState.IsAuthenticated() || clsAppState.IsGuest)
            {
                tbControlKeys.Text += $"| Press L To Logout" + clsCustomTags.LineBreak;
            }
            {
                tbControlKeys.Text += $"| Press E to Exit" + clsCustomTags.LineBreak;
            }
        }

        private void DisableContentGridsBorder()
        {
            for (int i = 0; i < tbPageContents.Length; i++)
            {
                tbPageContents[i].BorderShape = enBorderShape.None;
            }
        }

        private void UpdateContentGrids()
        {
            string[] content = NavigationController.GetCurrentPage().ContentStrings;

            for (int i = 0; i < tbPageContents.Length; i++)
            {
                tbPageContents[i].Text = content[i];
            }
        }

        private void UpdateScrollBar()
        {
            if (NavigationController.GetCurrentPage() is absScrollPage page)
            {
                vsbPageScrollBar.VisibleItems = absScrollPage.PAGE_ROWS_LIMIT;
                vsbPageScrollBar.TotalItems = page.GetRowCount();
                vsbPageScrollBar.SkippedItems = page.StartCursor;

                gmScrollBarContainer.Visible = vsbPageScrollBar.IsScrollBarNeeded;
            }
            else
            {
                gmScrollBarContainer.Visible = false;
            }
        }

        private void UpdateScrollingRangeText()
        {
            tbScrollingRange.Visible = false;

            if (NavigationController.GetCurrentPage() is not absScrollPage page)
                return;

            int rowCount = page.GetRowCount();

            if (rowCount <= 0)
                return;

            int lastVisibleRow =
            page.StartCursor + absScrollPage.PAGE_ROWS_LIMIT < rowCount ?
            page.StartCursor + absScrollPage.PAGE_ROWS_LIMIT :
            rowCount;

            string startRow = clsCalculation.FormatNumberAtSize(page.StartCursor + 1, 3);
            string endRow = clsCalculation.FormatNumberAtSize(lastVisibleRow, 3);

            tbScrollingRange.Text = $"[{startRow}]{clsCustomTags.LineBreak}[{endRow}]";
            tbScrollingRange.Visible = true;
        }

        private void UpdateRowCountText()
        {
            if (NavigationController.GetCurrentPage() is absScrollPage page)
            {
                string rowCount = clsCalculation.FormatNumberAtSize(page.GetRowCount(), 3);

                tbPageRowCount.Text = $"Rows{clsCustomTags.LineBreak}({rowCount})";
                tbPageRowCount.Visible = true;
            }
            else
            {
                tbPageRowCount.Visible = false;
            }
        }

        private void UpdateSelectedContentGrid()
        {
            if (NavigationController.GetCurrentPage() is not absScrollSelection page)
                return;

            if (page.GetRowCount() == 0)
                return;

            tbPageContents[(page.SelectionCursor - page.StartCursor + 1) * 3].BorderShape = enBorderShape.Dash;
        }
    }
}