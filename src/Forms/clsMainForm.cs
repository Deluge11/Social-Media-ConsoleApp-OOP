using Grids;
using SocialApp.HelperTools;

namespace SocialApp.Forms
{
    // Form Components
    public partial class clsMainForm
    {
        bool IsLazyLoading;

        int[] Rows = { 0, 8, 15, 22 };
        int[] Cols = { 0, 23, 46 };

        private clsTextGrid AppNameContentGrid { get; } = new clsTextGrid(11, 1, new stPaddingInfo(2, 1, 2, 1));
        private clsTextGrid PageStackContentGrid { get; } = new clsTextGrid(62, 1, new stPaddingInfo(3, 1, 3, 1));

        private clsGridManager PageGridManager { get; } = new clsGridManager(68, 29, new stPaddingInfo(1, 1, 1, 1));
        private clsGridManager HeaderBarManager { get; } = new clsGridManager(84, 3, new stPaddingInfo(0, 0, 0, 0));
        private clsGridManager ScrollBarManager { get; } = new clsGridManager(7, 21, new stPaddingInfo(1, 1, 1, 1));
        private clsGridManager CenterGridManager { get; } = new clsGridManager(82, 31, new stPaddingInfo(1, 1, 1, 1));

        private clsTextGrid[] ContentGrids { get; } = new clsTextGrid[12];

        private clsVerticalScrollBarGrid PageScrollBar { get; } = new clsVerticalScrollBarGrid(2, 19, '=');
        private clsVerticalContentGrid ScrollBarText { get; } = new clsVerticalContentGrid(17);

        private clsHorizontalLineGrid Line { get; } = new clsHorizontalLineGrid(68);



        private void InitializeComponent(bool isLazyLoading = true)
        {
            IsLazyLoading = isLazyLoading;

            HeaderBarManager.AddGrid(new stGridInfo(AppNameContentGrid, new stPoint(0, 0)));
            Print([HeaderBarManager]);

            HeaderBarManager.AddGrid(new stGridInfo(PageStackContentGrid, new stPoint(16, 0)));
            Print([HeaderBarManager]);

            for (int i = 0; i < ContentGrids.Length; i++)
            {
                ContentGrids[i] = new clsTextGrid(20, 5, new stPaddingInfo(1, 1, 1, 1));
            }

            Print([HeaderBarManager, CenterGridManager]);

            CenterGridManager.AddGrid(new stGridInfo(PageGridManager, new stPoint(1, 0)));
            Print([HeaderBarManager, CenterGridManager]);

            PageGridManager.AddGrid(new stGridInfo(Line, new stPoint(0, 7)));
            Print([HeaderBarManager, CenterGridManager]);

            for (int row = 0, count = 0; row < Rows.Length; row++)
            {
                for (int col = 0; col < Cols.Length; col++, count++)
                {
                    PageGridManager.AddGrid(new stGridInfo(ContentGrids[count], new stPoint(Cols[col], Rows[row])));
                    Print([HeaderBarManager, CenterGridManager]);
                }
            }

            PageScrollBar.SetScrollBarInformation(0, 0, 0);


            CenterGridManager.AddGrid(new stGridInfo(ScrollBarManager, new stPoint(72, 8)));
            Print([HeaderBarManager, CenterGridManager]);

            ScrollBarManager.AddGrid(new stGridInfo(ScrollBarText, new stPoint(5, 4)));
            ScrollBarManager.AddGrid(new stGridInfo(PageScrollBar, new stPoint(0, 0)));


            if (isLazyLoading)
            {
                PrintFinalSkeleton();
            }

            AfterComponentInitialization();
        }

        private void Print(absBaseGrid[] grids)
        {
            if (!IsLazyLoading) return;
            Console.Clear();

            foreach (absBaseGrid grid in grids)
            {
                grid.Print();
            }

            Thread.Sleep(300);
        }

        private void PrintFinalSkeleton()
        {
            Console.Clear();
            HeaderBarManager.Print();
            CenterGridManager.Print();
            Thread.Sleep(800);

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("| Press ? To `???`");
                Thread.Sleep(200);
            }

            Thread.Sleep(800);
        }

        private void PrintComponents()
        {
            HeaderBarManager.Print();
            CenterGridManager.Print();
        }
    }
}
