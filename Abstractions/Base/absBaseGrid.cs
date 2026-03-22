using SocialApp.Structure;

namespace SocialApp.Abstractions.Base
{
    public abstract class absBaseGrid
    {
        protected abstract int ContentBoardHeight { get; }
        protected abstract int ContentBoardWidth { get; }
        protected abstract stBoarderInfo BoarderInfo { get; }
        protected abstract stPaddingInfo PaddingInfo { get; }
        protected char[][] ContentBoard { get; }
        public char[][] BaseBoard { get; }

        protected abstract void SetContent();


        public absBaseGrid()
        {
            int baseBoardHeight = ContentBoardHeight + PaddingInfo.Top + PaddingInfo.Bottom;
            int baseBoardWidth = ContentBoardWidth + PaddingInfo.Left + PaddingInfo.Right;

            ContentBoard = InitBoard(ContentBoardWidth, ContentBoardHeight);
            BaseBoard = InitBoard(baseBoardWidth, baseBoardHeight);

            ClearContent(ContentBoard);
            ClearContent(BaseBoard);

            SetBorderOnBaseGrid();
        }


        public void ResetContent()
        {
            ClearContent(ContentBoard);
            SetContent();
            SetContentBoardOnBaseBoard();
        }

        public void Print()
        {
            int baseBoardHeight = ContentBoardHeight + PaddingInfo.Top + PaddingInfo.Bottom;
            int baseBoardWidth = ContentBoardWidth + PaddingInfo.Left + PaddingInfo.Right;

            for (int height = 0; height < baseBoardHeight; height++)
            {
                for (int width = 0; width < baseBoardWidth; width++)
                {
                    Console.Write(BaseBoard[height][width]);
                }

                Console.WriteLine();
            }
        }

        public void PrintContentBoard()
        {

            for (int height = 0; height < ContentBoardHeight; height++)
            {
                for (int width = 0; width < ContentBoardWidth; width++)
                {
                    Console.Write(ContentBoard[height][width]);
                }

                Console.WriteLine();
            }
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
            int baseBoardHeight = ContentBoardHeight + PaddingInfo.Top + PaddingInfo.Bottom;
            int baseBoardWidth = ContentBoardWidth + PaddingInfo.Left + PaddingInfo.Right;


            for (int height = 0; height < baseBoardHeight; height++)
            {
                BaseBoard[height][0] = BoarderInfo.Vertical;
                BaseBoard[height][baseBoardWidth - 1] = BoarderInfo.Vertical;

            }
            for (int width = 0; width < baseBoardWidth; width++)
            {
                BaseBoard[0][width] = BoarderInfo.Horizontal;
                BaseBoard[baseBoardHeight - 1][width] = BoarderInfo.Horizontal;
            }

            BaseBoard[0][0] = BoarderInfo.Corner;
            BaseBoard[0][baseBoardWidth - 1] = BoarderInfo.Corner;
            BaseBoard[baseBoardHeight - 1][0] = BoarderInfo.Corner;
            BaseBoard[baseBoardHeight - 1][baseBoardWidth - 1] = BoarderInfo.Corner;
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

    }
}
