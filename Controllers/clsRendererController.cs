
using SocialApp;
using SocialApp.Abstractions;
using SocialApp.Abstractions.Base;
using SocialApp.Enums;
using SocialApp.Grids;
using SocialApp.Interfaces;
using SocialApp.Structure;


namespace SocialApp.Controllers
{
    public class clsRendererController
    {
        public clsAppState AppState { get; }
        public clsNavigationController NavigationController { get; }

        //===============================================================

        int[] Rows = { 0, 8, 15, 22 };
        int[] Cols = { 0, 23, 46 };



        clsTextGrid appNameContentGrid = new clsTextGrid(11, 1, new stPaddingInfo(2, 1, 2, 1));
        clsTextGrid pageStackContentGrid = new clsTextGrid(50, 1, new stPaddingInfo(3, 1, 3, 1));

        clsHorizontalLineGrid line = new clsHorizontalLineGrid(68);


        clsGridManager pageGridManager = new clsGridManager(68, 29, new stPaddingInfo(1, 1, 1, 1));
        clsGridManager headerBarManager = new clsGridManager(75, 3, new stPaddingInfo(0, 0, 0, 0));
        clsGridManager scrollBarManager = new clsGridManager(6, 21, new stPaddingInfo(1, 1, 1, 1));
        clsGridManager centerGrid = new clsGridManager(80, 31, new stPaddingInfo(1, 1, 1, 1));


        clsTextGrid[] contentGrids = new clsTextGrid[12];

        clsVerticalScrollBarGrid pageScrollBar = new clsVerticalScrollBarGrid(2, 19, '=');
        clsVerticalContentGrid scrollBarText = new clsVerticalContentGrid(17);

        //===============================================================

        public clsRendererController(clsAppState appState, clsNavigationController navigationController)
        {

            for (int i = 0; i < contentGrids.Length; i++)
            {
                contentGrids[i] = new clsTextGrid(20, 5, new stPaddingInfo(1, 1, 1, 1));
            }

            for (int row = 0, count = 0; row < Rows.Length; row++)
            {
                for (int col = 0; col < Cols.Length; col++, count++)
                {
                    pageGridManager.AddGrid(new stGridInfo(contentGrids[count], new stPoint(Cols[col], Rows[row])));
                }
            }


            pageScrollBar.SetScrollBarInformation(10, 2, 8);

            appNameContentGrid.Text = "Social App";
            scrollBarText.Text = $"S C R O L L {clsCustomTags.InvisibleChar} B A R";


            centerGrid.AddGrid(new stGridInfo(pageGridManager, new stPoint(0, 0)));
            centerGrid.AddGrid(new stGridInfo(scrollBarManager, new stPoint(72, 8)));

            scrollBarManager.AddGrid(new stGridInfo(pageScrollBar, new stPoint(0, 0)));
            scrollBarManager.AddGrid(new stGridInfo(scrollBarText, new stPoint(5, 4)));

            headerBarManager.AddGrid(new stGridInfo(appNameContentGrid, new stPoint(0, 0)));
            headerBarManager.AddGrid(new stGridInfo(pageStackContentGrid, new stPoint(16, 0)));

            pageGridManager.AddGrid(new stGridInfo(line, new stPoint(0, 7)));



            ResetGrids();


            NavigationController = navigationController;
            AppState = appState;
        }

        protected void ResetGrids()
        {
            for (int row = 0, count = 0; row < Rows.Length; row++)
            {
                for (int col = 0; col < Cols.Length; col++, count++)
                {
                    contentGrids[count].BorderShape = enBorderShape.None;
                }
            }
        }

        protected void PrintPointers()
        {
            if (NavigationController.GetCurrentPage() is not absScrollPage)
            {
                return;
            }

            Console.WriteLine("*---------------------------");
            if (NavigationController.GetCurrentPage() is absScrollPage scrollPage)
            {
                Console.WriteLine($"| Start Cursor Value: {scrollPage.StartCursor}");
            }
            if (NavigationController.GetCurrentPage() is absScrollSelection scrollCursor)
            {
                Console.WriteLine($"| Selection Cursor Value: {scrollCursor.SelectionCursor}");
            }
            Console.WriteLine("*---------------------------");
        }

        public void Print()
        {
            ResetGrids();

            Console.Clear();
            BoardProcessing();
            headerBarManager.Print();
            centerGrid.Print();
            PrintControlKeys();

            //Testing
            //PrintPointers();
        }

        protected void BoardProcessing()
        {
            SetPagesStackGrid();
            SetPageContentOnBoardGrids();
            SetCursorOnBoard();
            SetScrollBarOnBoard();
        }

        protected void SetPagesStackGrid()
        {
            var pagesName = NavigationController.GetPagesNames();
            pageStackContentGrid.Text = "Pages: " + string.Join(" -> ", pagesName);
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

        protected void SetPageContentOnBoardGrids()
        {
            absBasePage currentPage = NavigationController.GetCurrentPage();
            currentPage.ResetContent();
            string[] content = currentPage.ContentGrids;


            for (int r = 0, count = 0; r < Rows.Length; r++)
            {
                for (int c = 0; c < Cols.Length; c++, count++)
                {
                    contentGrids[count].Text = content[count];
                }
            }
        }

        protected void SetScrollBarOnBoard()
        {
            scrollBarManager.Visible = false;

            if (NavigationController.GetCurrentPage() is not absScrollPage scrollPage)
            {
                return;
            }

            pageScrollBar.SetScrollBarInformation(scrollPage.GetRowCount(), absScrollPage.PAGE_ROWS_LIMIT, scrollPage.StartCursor);

            scrollBarManager.Visible = pageScrollBar.IsScrollBarNeeded();
        }

        protected void SetCursorOnBoard()
        {
            if (NavigationController.GetCurrentPage() is absScrollSelection page)
            {
                if(page.GetRowCount() > 0)
                {
                    SetCursorOnBoard(GetCursorPosition(), 1);
                }
            }
        }

        protected int GetCursorPosition()
        {
            if (NavigationController.GetCurrentPage() is absScrollSelection dynamicPage)
            {
                return dynamicPage.SelectionCursor - dynamicPage.StartCursor + 1;
            }
            return -1;
        }

        protected void SetCursorOnBoard(int row, int col)
        {
            contentGrids[(row * 3) * col].BorderShape = enBorderShape.Dash;
        }

    }
}
