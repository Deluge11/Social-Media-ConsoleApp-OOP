using Grids;

namespace SocialApp.Forms
{
    // Form Components
    public partial class clsMainForm
    {
        bool IsLazyLoading;

        int[] Rows = { 0, 8, 15, 22 };
        int[] Cols = { 0, 23, 46 };

        public clsTextGrid AppNameContentGrid { get; } = new clsTextGrid(11, 1, new stPaddingInfo(2, 1, 2, 1));
        public clsTextGrid PageStackContentGrid { get; } = new clsTextGrid(60, 1, new stPaddingInfo(3, 1, 3, 1));

        clsHorizontalLineGrid Line = new clsHorizontalLineGrid(68);


        public clsGridManager PageGridManager { get; } = new clsGridManager(68, 29, new stPaddingInfo(1, 1, 1, 1));
        public clsGridManager HeaderBarManager { get; } = new clsGridManager(82, 3, new stPaddingInfo(0, 0, 0, 0));
        public clsGridManager ScrollBarManager { get; } = new clsGridManager(6, 21, new stPaddingInfo(1, 1, 1, 1));
        public clsGridManager CenterGridManager { get; } = new clsGridManager(80, 31, new stPaddingInfo(1, 1, 1, 1));

        public clsTextGrid[] ContentGrids { get; } = new clsTextGrid[12];

        public clsVerticalScrollBarGrid PageScrollBar { get; } = new clsVerticalScrollBarGrid(2, 19, '=');
        public clsVerticalContentGrid ScrollBarText { get; } = new clsVerticalContentGrid(17);

      

        private void InitializeForm(bool isLazyLoading = true)
        {
            IsLazyLoading = isLazyLoading;


            HeaderBarManager.AddGrid(new stGridInfo(AppNameContentGrid, new stPoint(0, 0)));
            Print([HeaderBarManager]);

            HeaderBarManager.AddGrid(new stGridInfo(PageStackContentGrid, new stPoint(16, 0)));
            Print([HeaderBarManager]);

            AppNameContentGrid.Text = "Social App";
            Print([HeaderBarManager]);

            for (int i = 0; i < ContentGrids.Length; i++)
            {
                ContentGrids[i] = new clsTextGrid(20, 5, new stPaddingInfo(1, 1, 1, 1));
            }

            PageGridManager.AddGrid(new stGridInfo(Line, new stPoint(0, 7)));

            Print([HeaderBarManager, CenterGridManager]);

            CenterGridManager.AddGrid(new stGridInfo(PageGridManager, new stPoint(0, 0)));
            Print([HeaderBarManager, CenterGridManager], 300);

            for (int row = 0, count = 0; row < Rows.Length; row++)
            {
                for (int col = 0; col < Cols.Length; col++, count++)
                {
                    Print([HeaderBarManager, CenterGridManager]);
                    PageGridManager.AddGrid(new stGridInfo(ContentGrids[count], new stPoint(Cols[col], Rows[row])));
                }
            }
            Print([HeaderBarManager, CenterGridManager]);

            PageScrollBar.SetScrollBarInformation(0, 0, 0);

 
            CenterGridManager.AddGrid(new stGridInfo(ScrollBarManager, new stPoint(72, 8)));
            Print([HeaderBarManager, CenterGridManager], 300);


            ScrollBarManager.AddGrid(new stGridInfo(PageScrollBar, new stPoint(0, 0)));
            Print([HeaderBarManager, CenterGridManager], 300);


            ScrollBarText.Text = $"S C R O L L {clsCustomTags.InvisibleChar} B A R";
            ScrollBarManager.AddGrid(new stGridInfo(ScrollBarText, new stPoint(5, 4)));
            Print([HeaderBarManager, CenterGridManager], 300);

            Thread.Sleep(800);


            for(int i = 0; i < 5; i++)
            {
                Console.WriteLine("| Press ? To `???`");
                Thread.Sleep(200);
            }
          
            Thread.Sleep(800);
        }

        private void Print(absBaseGrid[] grids, int sleepMS = 160)
        {
            if (!IsLazyLoading) return;
            Console.Clear();

            foreach(absBaseGrid grid in grids)
            {
                grid.Print();
            }

            Thread.Sleep(sleepMS);
        }

    }
}
