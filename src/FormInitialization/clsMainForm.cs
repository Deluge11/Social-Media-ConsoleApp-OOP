using Grids;
using SocialApp.HelperTools;

namespace SocialApp.Forms
{
    // Form Components
    public partial class clsMainForm
    {
        clsTextBoxGrid tbAppName = null;
        clsTextBoxGrid tbPageNavigation = null;
        clsTextBoxGrid tbControlKeys = null;
        clsTextBoxGrid tbScrollingRange = null;
        clsTextBoxGrid tbPageRowCount = null;
        clsGridManager gmPageContentContainer = null;
        clsGridManager gmHeaderBar = null;
        clsGridManager gmScrollBarContainer = null;
        clsGridManager gmCenter = null;
        clsTextBoxGrid[] tbPageContents = null;
        clsVerticalScrollBarGrid vsbPageScrollBar = null;
        clsVerticalContentGrid tbScrollBarText = null;
        clsHorizontalLineGrid Line = null;

        private void InitializeComponent()
        {
            tbAppName = new clsTextBoxGrid(11, 1, new stPaddingInfo(2, 1, 2, 1));
            tbPageNavigation = new clsTextBoxGrid(62, 1, new stPaddingInfo(3, 1, 3, 1));
            tbControlKeys = new clsTextBoxGrid(50, 7, new stPaddingInfo(0, 0, 0, 0));
            tbScrollingRange = new clsTextBoxGrid(6, 2, new stPaddingInfo(2, 1, 1, 1));
            tbPageRowCount = new clsTextBoxGrid(6, 2, new stPaddingInfo(2, 1, 1, 1));
            gmPageContentContainer = new clsGridManager(68, 29, new stPaddingInfo(1, 1, 1, 1));
            gmHeaderBar = new clsGridManager(84, 3, new stPaddingInfo(0, 0, 0, 0));
            gmScrollBarContainer = new clsGridManager(7, 21, new stPaddingInfo(1, 1, 1, 1));
            gmCenter = new clsGridManager(82, 31, new stPaddingInfo(1, 1, 1, 1));
            tbPageContents = new clsTextBoxGrid[12];
            vsbPageScrollBar = new clsVerticalScrollBarGrid(2, 19, '=');
            tbScrollBarText = new clsVerticalContentGrid(17);
            Line = new clsHorizontalLineGrid(68);

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

            int[] Rows = { 0, 8, 15, 22 };
            int[] Cols = { 0, 23, 46 };

            for (int row = 0, count = 0; row < Rows.Length; row++)
                for (int col = 0; col < Cols.Length; col++, count++)
                    gmPageContentContainer.AddGrid(new stGridInfo(tbPageContents[count], new stPoint(Cols[col], Rows[row])));

            FormOnLoad();
        }

        public void Print()
        {
            UpdateComponents();

            gmHeaderBar.Print();
            gmCenter.Print();
            tbControlKeys.Print();
        }
    }
}
