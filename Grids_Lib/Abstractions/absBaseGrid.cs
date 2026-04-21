

namespace Grids
{
    public abstract class absBaseGrid
    {
        public int ContentBoardHeight { get; protected set; }
        public int ContentBoardWidth { get; protected set; }
        protected stBoarderInfo BorderInfo { get; set; }
        protected stPaddingInfo PaddingInfo { get; set; }
        public enBorderShape BorderShape { get; set; } = enBorderShape.Full;
        protected char[][] ContentBoard { get; }
        public char[][] BaseBoard { get; }
        public bool Visible { get; set; } = true;

        protected abstract void SetContent();


        public absBaseGrid(int width, int height, stPaddingInfo padding, stBoarderInfo border)
        {
            PaddingInfo = padding;
            BorderInfo = border;

            ContentBoardHeight = height;
            ContentBoardWidth = width;

            int baseBoardHeight = ContentBoardHeight + PaddingInfo.Top + PaddingInfo.Bottom;
            int baseBoardWidth = ContentBoardWidth + PaddingInfo.Left + PaddingInfo.Right;

            ContentBoard = InitBoard(ContentBoardWidth, ContentBoardHeight);
            BaseBoard = InitBoard(baseBoardWidth, baseBoardHeight);

            ClearContent(ContentBoard);
            ClearContent(BaseBoard);
        }


        public void Print()
        {
            UpdateBoard();

            int baseBoardHeight = ContentBoardHeight + PaddingInfo.Top + PaddingInfo.Bottom;

            for (int height = 0; height < baseBoardHeight; height++)
            {
                Console.WriteLine(new string(BaseBoard[height]));
            }
        }

        public void UpdateBoard()
        {
            ClearContent(BaseBoard);
            ClearContent(ContentBoard);

            SetBorderOnBaseGrid();
            SetContent();
            SetContentBoardOnBaseBoard();
        }

        private void ClearContent(char[][] board)
        {
            for (int height = 0; height < board.Length; height++)
            {
                for (int width = 0; width < board[height].Length; width++)
                {
                    board[height][width] = ' ';
                }
            }
        }

        private void SetContentBoardOnBaseBoard()
        {
            for (int height = 0; height < ContentBoardHeight; height++)
            {
                for (int width = 0; width < ContentBoardWidth; width++)
                {
                    BaseBoard[height + PaddingInfo.Top][width + PaddingInfo.Left] = ContentBoard[height][width];
                }
            }
        }

        private void SetBorderOnBaseGrid()
        {
            switch (BorderShape)
            {
                case enBorderShape.Dash:
                    SetDashBorderOnBoard();
                    break;

                case enBorderShape.Full:
                    SetFullBorderOnBoard();
                    return;
            }
        }

        private void SetFullBorderOnBoard()
        {
            int baseBoardHeight = ContentBoardHeight + PaddingInfo.Top + PaddingInfo.Bottom;
            int baseBoardWidth = ContentBoardWidth + PaddingInfo.Left + PaddingInfo.Right;

            for (int height = 0; height < baseBoardHeight; height++)
            {
                BaseBoard[height][0] = BorderInfo.Vertical;
                BaseBoard[height][baseBoardWidth - 1] = BorderInfo.Vertical;

            }
            for (int width = 0; width < baseBoardWidth; width++)
            {
                BaseBoard[0][width] = BorderInfo.Horizontal;
                BaseBoard[baseBoardHeight - 1][width] = BorderInfo.Horizontal;
            }

            BaseBoard[0][0] = BorderInfo.Corner;
            BaseBoard[0][baseBoardWidth - 1] = BorderInfo.Corner;
            BaseBoard[baseBoardHeight - 1][0] = BorderInfo.Corner;
            BaseBoard[baseBoardHeight - 1][baseBoardWidth - 1] = BorderInfo.Corner;
        }

        private void SetDashBorderOnBoard()
        {
            int baseBoardHeight = ContentBoardHeight + PaddingInfo.Top + PaddingInfo.Bottom;
            int baseBoardWidth = ContentBoardWidth + PaddingInfo.Left + PaddingInfo.Right;

            bool toggle = true;

            for (int height = 1; height < baseBoardHeight - 1; height++, toggle = !toggle)
            {
                if (!toggle) continue;

                BaseBoard[height][0] = BorderInfo.Vertical;
                BaseBoard[height][baseBoardWidth - 1] = BorderInfo.Vertical;
            }

            for (int width = 1; width < baseBoardWidth - 1; width++, toggle = !toggle)
            {
                if (!toggle) continue;

                BaseBoard[0][width] = BorderInfo.Horizontal;
                BaseBoard[baseBoardHeight - 1][width] = BorderInfo.Horizontal;
            }
        }

        private char[][] InitBoard(int width, int height)
        {
            char[][] board = new char[height][];
            for (int i = 0; i < height; i++)
            {
                board[i] = new char[width];
            }
            return board;
        }

        protected bool IsExceedBoard(int width, int height)
        {
            return height >= ContentBoardHeight || width >= ContentBoardWidth || height < 0 || width < 0;
        }

    }
}
