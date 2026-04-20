using SocialApp.Grids;
using SocialApp.Grids.Abstractions;
using SocialApp.Structure;


namespace SocialApp.Forms
{
    public class clsMainForm
    {
        int[] Rows = { 0, 8, 15, 22 };
        int[] Cols = { 0, 23, 46 };

        public clsTextGrid AppNameContentGrid { get; } = new clsTextGrid(11, 1, new stPaddingInfo(2, 1, 2, 1));
        public clsTextGrid PageStackContentGrid { get; } = new clsTextGrid(50, 1, new stPaddingInfo(3, 1, 3, 1));

        clsHorizontalLineGrid Line = new clsHorizontalLineGrid(68);


        public clsGridManager PageGridManager { get; } = new clsGridManager(68, 29, new stPaddingInfo(1, 1, 1, 1));
        public clsGridManager HeaderBarManager { get; } = new clsGridManager(75, 3, new stPaddingInfo(0, 0, 0, 0));
        public clsGridManager ScrollBarManager { get; } = new clsGridManager(6, 21, new stPaddingInfo(1, 1, 1, 1));
        public clsGridManager CenterGrid { get; } = new clsGridManager(80, 31, new stPaddingInfo(1, 1, 1, 1));

        public clsTextGrid[] ContentGrids { get; } = new clsTextGrid[12];

        public clsVerticalScrollBarGrid PageScrollBar { get; } = new clsVerticalScrollBarGrid(2, 19, '=');
        public clsVerticalContentGrid ScrollBarText { get; } = new clsVerticalContentGrid(17);


        public clsMainForm()
        {
            for (int i = 0; i < ContentGrids.Length; i++)
            {
                ContentGrids[i] = new clsTextGrid(20, 5, new stPaddingInfo(1, 1, 1, 1));
            }

            PageGridManager.AddGrid(new stGridInfo(Line, new stPoint(0, 7)));
            Print(CenterGrid);

            for (int row = 0, count = 0; row < Rows.Length; row++)
            {
                for (int col = 0; col < Cols.Length; col++, count++)
                {
                    Print(PageGridManager);
                    PageGridManager.AddGrid(new stGridInfo(ContentGrids[count], new stPoint(Cols[col], Rows[row])));
                }
            }
            Print(PageGridManager);


            PageScrollBar.SetScrollBarInformation(0, 0, 0);
            AppNameContentGrid.Text = "Social App";
            ScrollBarText.Text = $"S C R O L L {clsCustomTags.InvisibleChar} B A R";

            CenterGrid.AddGrid(new stGridInfo(PageGridManager, new stPoint(0, 0)));
            Print(CenterGrid);

            CenterGrid.AddGrid(new stGridInfo(ScrollBarManager, new stPoint(72, 8)));
            Print(CenterGrid);

            ScrollBarManager.AddGrid(new stGridInfo(PageScrollBar, new stPoint(0, 0)));
            Print(CenterGrid);

            ScrollBarManager.AddGrid(new stGridInfo(ScrollBarText, new stPoint(5, 4)));
            Print(CenterGrid);

            HeaderBarManager.AddGrid(new stGridInfo(AppNameContentGrid, new stPoint(0, 0)));
            HeaderBarManager.AddGrid(new stGridInfo(PageStackContentGrid, new stPoint(16, 0)));
            Print(HeaderBarManager, 350);

            Console.Clear();
            HeaderBarManager.Print();
            CenterGrid.Print();

            Thread.Sleep(180);
            Console.WriteLine("| Press ? To `???`");
            Thread.Sleep(180);
            Console.WriteLine("| Press ? To `???`");
            Thread.Sleep(180);
            Console.WriteLine("| Press ? To `???`");
            Thread.Sleep(180);
            Console.WriteLine("| Press ? To `???`");
            Thread.Sleep(180);
            Console.WriteLine("| Press ? To `???`");
            Thread.Sleep(800);
        }

        public void Print()
        {
            HeaderBarManager.Print();
            CenterGrid.Print();
        }

        private void Print(absBaseGrid grid, int sleepMS = 160)
        {
            Console.Clear();
            grid.Print();
            Thread.Sleep(sleepMS);
        }

    }
}
