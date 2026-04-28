

namespace Grids
{
    public abstract class absBaseGrid
    {
        public delegate void OnContentUpdate();

        public event OnContentUpdate? OnGridUpdateEvent;
        public int ContentBoardHeight { get; protected set; }
        public int ContentBoardWidth { get; protected set; }
        protected stBoarderInfo BorderInfo { get; set; }
        protected stPaddingInfo PaddingInfo { get; set; }

        protected char[][] ContentBoard { get; }
        public char[][] BaseBoard { get; }

        private enBorderShape _borderShape = enBorderShape.Full;

        private bool _visible = true;

        public enBorderShape BorderShape
        {
            get => _borderShape;
            set
            {
                _borderShape = value;
                UpdateBoard();
            }
        }
        public bool Visible
        {
            get => _visible;
            set
            {
                _visible = value;
                UpdateBoard();
            }
        }

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
            // UpdateBoard();

            if (!Visible)
                return;

            int baseBoardHeight = ContentBoardHeight + PaddingInfo.Top + PaddingInfo.Bottom;

            for (int height = 0; height < baseBoardHeight; height++)
            {
                Console.WriteLine(new string(BaseBoard[height]));
            }
        }

        protected void UpdateBoard()
        {
            ClearContent(BaseBoard);
            ClearContent(ContentBoard);

            SetBorderOnBaseGrid();
            SetContent();
            SetContentBoardOnBaseBoard();

            OnGridUpdateEvent?.Invoke();
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

            short maxVerticalLine = 1;
            short maxHorizontalLine = 3;

            short count = 0;

            for (int height = 1; height < baseBoardHeight - 1; height++)
            {
                if (count == maxVerticalLine)
                {
                    count = 0;
                    continue;
                }
                count++;

                BaseBoard[height][0] = BorderInfo.Vertical;
                BaseBoard[height][baseBoardWidth - 1] = BorderInfo.Vertical;
            }

            count = 0;

            for (int width = 1; width < baseBoardWidth - 1; width++)
            {
                if (count == maxHorizontalLine)
                {
                    count = 0;
                    continue;
                }
                count++;

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
