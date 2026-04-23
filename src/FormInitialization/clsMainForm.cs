using Grids;
using SocialApp.HelperTools;

namespace SocialApp.Forms
{
    // Form Components
    public partial class clsMainForm
    {
        int[] Rows = { 0, 8, 15, 22 };
        int[] Cols = { 0, 23, 46 };

        private clsTextBoxGrid tbAppName { get; } = new clsTextBoxGrid(11, 1, new stPaddingInfo(2, 1, 2, 1));
        private clsTextBoxGrid tbPageNavigation { get; } = new clsTextBoxGrid(62, 1, new stPaddingInfo(3, 1, 3, 1));
        private clsTextBoxGrid tbControlKeys { get; } = new clsTextBoxGrid(50, 7, new stPaddingInfo(0, 0, 0, 0));
        private clsTextBoxGrid tbScrollingRange { get; } = new clsTextBoxGrid(6, 2, new stPaddingInfo(2, 1, 1, 1));
        private clsTextBoxGrid tbPageRowCount { get; } = new clsTextBoxGrid(6, 2, new stPaddingInfo(2, 1, 1, 1));

        private clsGridManager gmPageContentContainer { get; } = new clsGridManager(68, 29, new stPaddingInfo(1, 1, 1, 1));
        private clsGridManager gmHeaderBar { get; } = new clsGridManager(84, 3, new stPaddingInfo(0, 0, 0, 0));
        private clsGridManager gmScrollBarContainer { get; } = new clsGridManager(7, 21, new stPaddingInfo(1, 1, 1, 1));
        private clsGridManager gmCenter { get; } = new clsGridManager(82, 31, new stPaddingInfo(1, 1, 1, 1));

        private clsTextBoxGrid[] tbPageContents { get; } = new clsTextBoxGrid[12];

        private clsVerticalScrollBarGrid vsbPageScrollBar { get; } = new clsVerticalScrollBarGrid(2, 19, '=');
        private clsVerticalContentGrid tbScrollBarText { get; } = new clsVerticalContentGrid(17);

        private clsHorizontalLineGrid Line { get; } = new clsHorizontalLineGrid(68);

        public void Print()
        {
            UpdateComponents();
            gmHeaderBar.Print();
            gmCenter.Print();
            tbControlKeys.Print();
        }

        private void InitializeComponent(bool isLazyLoading = false)
        {
            if (isLazyLoading)
            {
                InitializeComponentLazy();
            }
            else
            {
                InitializeComponentQuick();
            }

            FormOnLoad();
        }

        private void Print(absBaseGrid[] grids)
        {
            Console.SetCursorPosition(0, 0);

            foreach (absBaseGrid grid in grids)
            {
                grid.Print();
            }

            Thread.Sleep(300);
        }

        private void InitializeComponentLazy()
        {
            Thread.Sleep(2000);

            gmHeaderBar.AddGrid(new stGridInfo(tbAppName, new stPoint(0, 0)));
            Print([gmHeaderBar]);

            gmHeaderBar.AddGrid(new stGridInfo(tbPageNavigation, new stPoint(16, 0)));
            Print([gmHeaderBar]);

            for (int i = 0; i < tbPageContents.Length; i++)
            {
                tbPageContents[i] = new clsTextBoxGrid(20, 5, new stPaddingInfo(1, 1, 1, 1));
            }

            Print([gmHeaderBar, gmCenter]);

            gmCenter.AddGrid(new stGridInfo(gmPageContentContainer, new stPoint(1, 0)));
            Print([gmHeaderBar, gmCenter]);

            gmPageContentContainer.AddGrid(new stGridInfo(Line, new stPoint(0, 7)));
            Print([gmHeaderBar, gmCenter]);

            for (int row = 0, count = 0; row < Rows.Length; row++)
            {
                for (int col = 0; col < Cols.Length; col++, count++)
                {
                    gmPageContentContainer.AddGrid(new stGridInfo(tbPageContents[count], new stPoint(Cols[col], Rows[row])));
                    Print([gmHeaderBar, gmCenter]);
                }
            }

            vsbPageScrollBar.SetScrollBarInformation(0, 0, 0);

            gmCenter.AddGrid(new stGridInfo(tbPageRowCount, new stPoint(72, 0)));
            Print([gmHeaderBar, gmCenter]);

            gmCenter.AddGrid(new stGridInfo(tbScrollingRange, new stPoint(72, 4)));
            Print([gmHeaderBar, gmCenter]);

            gmCenter.AddGrid(new stGridInfo(gmScrollBarContainer, new stPoint(72, 8)));
            Print([gmHeaderBar, gmCenter]);

            gmScrollBarContainer.AddGrid(new stGridInfo(tbScrollBarText, new stPoint(5, 4)));
            gmScrollBarContainer.AddGrid(new stGridInfo(vsbPageScrollBar, new stPoint(0, 0)));

            Console.SetCursorPosition(0, 0);
            gmHeaderBar.Print();
            gmCenter.Print();
            Thread.Sleep(800);

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("| Press ? To `???`");
                Thread.Sleep(200);
            }

            Thread.Sleep(800);
        }

        private void InitializeComponentQuick()
        {
            gmHeaderBar.AddGrid(new stGridInfo(tbAppName, new stPoint(0, 0)));
            gmHeaderBar.AddGrid(new stGridInfo(tbPageNavigation, new stPoint(16, 0)));
            gmPageContentContainer.AddGrid(new stGridInfo(Line, new stPoint(0, 7)));
            gmCenter.AddGrid(new stGridInfo(tbPageRowCount, new stPoint(72, 0)));
            gmCenter.AddGrid(new stGridInfo(tbScrollingRange, new stPoint(72, 4)));
            gmCenter.AddGrid(new stGridInfo(gmPageContentContainer, new stPoint(1, 0)));
            gmCenter.AddGrid(new stGridInfo(gmScrollBarContainer, new stPoint(72, 8)));
            gmScrollBarContainer.AddGrid(new stGridInfo(tbScrollBarText, new stPoint(5, 4)));
            gmScrollBarContainer.AddGrid(new stGridInfo(vsbPageScrollBar, new stPoint(0, 0)));

            for (int i = 0; i < tbPageContents.Length; i++)
                tbPageContents[i] = new clsTextBoxGrid(20, 5, new stPaddingInfo(1, 1, 1, 1));

            for (int row = 0, count = 0; row < Rows.Length; row++)
                for (int col = 0; col < Cols.Length; col++, count++)
                    gmPageContentContainer.AddGrid(new stGridInfo(tbPageContents[count], new stPoint(Cols[col], Rows[row])));
        }

    }
}
