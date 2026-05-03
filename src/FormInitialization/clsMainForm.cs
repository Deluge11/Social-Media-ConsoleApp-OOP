using Grids;
using Grids_Lib.Enums;

namespace SocialApp.Forms
{
    // Form Components
    public partial class clsMainForm
    {
        clsGridManager gmCenter = null;
        clsTextBoxGrid tbAppName = null;
        clsHorizontalLineGrid Line = null;
        clsGridManager gmHeaderBar = null;
        clsTextBoxGrid tbControlKeys = null;
        clsTextBoxGrid tbPageRowCount = null;
        clsTextBoxGrid tbPageNavigation = null;
        clsTextBoxGrid tbScrollingRange = null;
        clsTextBoxGrid[] tbPageContents = null;
        clsGridManager gmScrollBarContainer = null;
        clsGridManager gmPageContentContainer = null;
        clsVerticalContentGrid tbScrollBarText = null;
        clsVerticalScrollBarGrid vsbPageScrollBar = null;

        private void InitializeComponent()
        {
            Line = new clsHorizontalLineGrid(70);
            tbPageContents = new clsTextBoxGrid[12];
            tbScrollBarText = new clsVerticalContentGrid(17);
            vsbPageScrollBar = new clsVerticalScrollBarGrid(2, 19, '=');
            gmCenter = new clsGridManager(84, 31, new stPaddingInfo(1, 1, 1, 1));
            tbAppName = new clsTextBoxGrid(11, 1, new stPaddingInfo(2, 1, 2, 1));
            gmHeaderBar = new clsGridManager(86, 3, new stPaddingInfo(0, 0, 0, 0));
            tbPageRowCount = new clsTextBoxGrid(6, 2, new stPaddingInfo(2, 1, 1, 1));
            tbControlKeys = new clsTextBoxGrid(50, 7, new stPaddingInfo(0, 0, 0, 0));
            tbScrollingRange = new clsTextBoxGrid(6, 2, new stPaddingInfo(2, 1, 1, 1));
            tbPageNavigation = new clsTextBoxGrid(64, 1, new stPaddingInfo(3, 1, 3, 1));
            gmScrollBarContainer = new clsGridManager(7, 21, new stPaddingInfo(1, 1, 1, 1));
            gmPageContentContainer = new clsGridManager(70, 29, new stPaddingInfo(1, 1, 1, 1));

            gmHeaderBar.AddGrid(tbAppName, new stPoint(0, 0));
            gmCenter.AddGrid(tbPageRowCount, new stPoint(74, 0));
            gmCenter.AddGrid(tbScrollingRange, new stPoint(74, 4));
            gmPageContentContainer.AddGrid(Line, new stPoint(0, 7));
            gmHeaderBar.AddGrid(tbPageNavigation, new stPoint(16, 0));
            gmCenter.AddGrid(gmScrollBarContainer, new stPoint(74, 8));
            gmCenter.AddGrid(gmPageContentContainer, new stPoint(1, 0));
            gmScrollBarContainer.AddGrid(tbScrollBarText, new stPoint(5, 4));
            gmScrollBarContainer.AddGrid(vsbPageScrollBar, new stPoint(0, 0));

            for (int i = 0; i < tbPageContents.Length; i++)
                tbPageContents[i] = new clsTextBoxGrid(20, 5, new stPaddingInfo(1, 1, 1, 1));

            for (int i = 0; i < tbPageContents.Length; i++)
                tbPageContents[i].Alignment = enAlignment.Center;

            int[] Rows = { 0, 8, 15, 22 };
            int[] Cols = { 1, 24, 47 };

            for (int row = 0, count = 0; row < Rows.Length; row++)
                for (int col = 0; col < Cols.Length; col++, count++)
                    gmPageContentContainer.AddGrid(tbPageContents[count], new stPoint(Cols[col], Rows[row]));

            //PrintComponents();
            //Console.ReadKey();


            FormOnLoad();
        }

        public void Print()
        {
            UpdateComponents();
            PrintComponents();
        }

        private void PrintComponents()
        {
            gmHeaderBar.Print();
            gmCenter.Print();
            tbControlKeys.Print();
        }
    }
}
