
using SocialApp;
using SocialApp.Enums;
using SocialApp.Grids;
using SocialApp.Grids.Abstractions;
using SocialApp.Interfaces;
using SocialApp.Pages.Abstractions;
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


        clsTextGrid[] ContentGrids = new clsTextGrid[12];

        clsVerticalScrollBarGrid pageScrollBar = new clsVerticalScrollBarGrid(2, 19, '=');
        clsVerticalContentGrid scrollBarText = new clsVerticalContentGrid(17);

        //===============================================================

        public clsRendererController(clsAppState appState, clsNavigationController navigationController)
        {
            for (int i = 0; i < ContentGrids.Length; i++)
            {
                ContentGrids[i] = new clsTextGrid(20, 5, new stPaddingInfo(1, 1, 1, 1));
            }

            pageGridManager.AddGrid(new stGridInfo(line, new stPoint(0, 7)));
            Print(centerGrid);

            for (int row = 0, count = 0; row < Rows.Length; row++)
            {
                for (int col = 0; col < Cols.Length; col++, count++)
                {

                    Print(pageGridManager);
                    pageGridManager.AddGrid(new stGridInfo(ContentGrids[count], new stPoint(Cols[col], Rows[row])));
                }
            }
            Print(pageGridManager);


            pageScrollBar.SetScrollBarInformation(0, 0, 0);
            appNameContentGrid.Text = "Social App";
            scrollBarText.Text = $"S C R O L L {clsCustomTags.InvisibleChar} B A R";

            centerGrid.AddGrid(new stGridInfo(pageGridManager, new stPoint(0, 0)));
            Print(centerGrid);

            centerGrid.AddGrid(new stGridInfo(scrollBarManager, new stPoint(72, 8)));
            Print(centerGrid);

            scrollBarManager.AddGrid(new stGridInfo(pageScrollBar, new stPoint(0, 0)));
            Print(centerGrid);

            scrollBarManager.AddGrid(new stGridInfo(scrollBarText, new stPoint(5, 4)));
            Print(centerGrid);

            headerBarManager.AddGrid(new stGridInfo(appNameContentGrid, new stPoint(0, 0)));
            headerBarManager.AddGrid(new stGridInfo(pageStackContentGrid, new stPoint(16, 0)));
            Print(headerBarManager,350);

            Console.Clear();
            headerBarManager.Print();
            centerGrid.Print();

            Thread.Sleep(150);
            Console.WriteLine("| Press ? To `???`'");
            Thread.Sleep(150);
            Console.WriteLine("| Press ? To `???`'");
            Thread.Sleep(150);   
            Console.WriteLine("| Press ? To `???`'");
            Thread.Sleep(150);
            Console.WriteLine("| Press ? To `???`'");
            Thread.Sleep(150);
            Console.WriteLine("| Press ? To `???`'");
            Thread.Sleep(800);


            ResetGrids();


            NavigationController = navigationController;
            AppState = appState;
        }

        private void Print(absBaseGrid grid,int sleepMS = 160)
        {
            Console.Clear();
            grid.Print();
            Thread.Sleep(sleepMS);
        }

        protected void ResetGrids()
        {
            for (int row = 0, count = 0; row < Rows.Length; row++)
            {
                for (int col = 0; col < Cols.Length; col++, count++)
                {
                    ContentGrids[count].BorderShape = enBorderShape.None;
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
                    ContentGrids[count].Text = content[count];
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
            if (NavigationController.GetCurrentPage() is not absScrollSelection page) return;
            if (page.GetRowCount() == 0) return;

            int curserRowNumber = page.SelectionCursor - page.StartCursor + 1;
            ConvertBorderShapeToDash(curserRowNumber, 1);

        }
        protected void ConvertBorderShapeToDash(int row, int col)
        {
            ContentGrids[(row * 3) * col].BorderShape = enBorderShape.Dash;
        }

    }
}
