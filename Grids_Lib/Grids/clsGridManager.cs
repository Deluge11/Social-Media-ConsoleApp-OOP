
namespace Grids
{
    public class clsGridManager : absBaseGrid
    {
        private List<stGridInfo> Grids { get; set; } = new List<stGridInfo>();

        public clsGridManager(int width, int height, stPaddingInfo padding) : base(width, height, padding, new stBoarderInfo('-', '|', '*'))
        {

        }

        public void AddGrid(stGridInfo gridInfo)
        {
            // If Inner Component Get Updated Then Update This Grid
            gridInfo.Grid.OnGridUpdateEvent += UpdateBoard;
            Grids.Add(gridInfo);

            SortGrids();
            UpdateBoard();
        }

        protected override void SetContent()
        {
            foreach (stGridInfo gridInfo in Grids)
            {
                SetGridContentOnBoard(gridInfo);
            }
        }

        protected void SetGridContentOnBoard(stGridInfo gridInfo)
        {
            if (!gridInfo.Grid.Visible) return;

            for (int height = 0; height < gridInfo.Grid.BaseBoard.Length; height++)
            {
                for (int width = 0; width < gridInfo.Grid.BaseBoard[height].Length; width++)
                {
                    int pointX = width + gridInfo.Point.X;
                    int pointY = height + gridInfo.Point.Y;

                    if (IsExceedBoard(pointX, pointY)) continue;

                    char pixel = gridInfo.Grid.BaseBoard[height][width];

                    if (pixel != ' ')
                    {
                        ContentBoard[pointY][pointX] = pixel;
                    }
                }
            }
        }

        public void SortGrids() => Grids.Sort((a, b) => a.Point.Z.CompareTo(b.Point.Z));
    }
}
