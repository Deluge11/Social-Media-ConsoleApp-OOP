
using SocialApp.Abstractions;
using SocialApp.HelperTools;
using SocialApp.Interfaces;


namespace SocialApp.Controllers
{
    public class clsRendererController
    {
        private const int Col1 = 5;
        private const int Col2 = 28;
        private const int Col3 = 51;

        private const int Row1 = 2;
        private const int Row2 = 8;
        private const int Row3 = 14;
        private const int Row4 = 20;

        private const int Width = 80;
        private const int BoarderWidth = 76;

        private const int GridWidth = 20;
        private const int GridHeight = 5;

        private char[][] Board = new char[27][];

        public clsAppState AppState { get; }
        public clsNavigationController NavigationController { get; }


        public clsRendererController(clsAppState appState, clsNavigationController navigationController)
        {
            for (int i = 0; i < Board.Length; i++)
            {
                Board[i] = new char[Width];
            }

            NavigationController = navigationController;
            AppState = appState;
            SetBoardDefault();
        }

        protected void PrintPointers()
        {
            if (NavigationController.GetCurrentPage() is not absScrollPage)
            {
                return;
            }

            Console.WriteLine("*---------------------------");
            if (NavigationController.GetCurrentPage() is absScrollPage scrollPage)
            {
                Console.WriteLine($"| Start Cursor Value: {scrollPage.StartCursor}");
            }
            if (NavigationController.GetCurrentPage() is absScrollSelection scrollCursor)
            {
                Console.WriteLine($"| Selection Cursor Value: {scrollCursor.SelectionCursor}");
            }
            Console.WriteLine("*---------------------------");
        }

        public void Print()
        {
            Console.Clear();
            BoardProcessing();
            PrintPagesStackBox();
            PrintBoard();
            PrintControlKeys();

            //Testing
            //PrintPointers();
        }

        protected void BoardProcessing()
        {
            SetBoardDefault();
            SetPageContentOnBoardGrids();
            SetCursorOnBoard();
            SetScrollBarOnBoard();
            SetHorizontalLineOnBoard(6);
        }

        protected void PrintPagesStackBox()
        {
            var pagesName = NavigationController.GetPagesNames();
            string separator = " -> ";
            string leftBorder = "| ";
            string rightBorder = "  |";

            int pagesNameTotalLength =
                clsCalculation.GetStringListLengthWithSeparation(pagesName, separator.Length) +
                leftBorder.Length + rightBorder.Length + separator.Length;

            PrintHorizontalLine(pagesNameTotalLength);
            Console.Write(leftBorder);
            PrintPagesStackWithSeparator(pagesName, separator);
            Console.Write(rightBorder);
            PrintHorizontalLine(pagesNameTotalLength);
        }

        protected void PrintPagesStackWithSeparator(List<string> pagesName, string separator)
        {
            for (int i = pagesName.Count - 1; i >= 0; i--)
            {
                Console.Write(separator);
                Console.Write(pagesName[i]);
            }
        }

        protected void PrintControlKeys()
        {
            absBasePage currentPage = NavigationController.GetCurrentPage();

            if (currentPage is absScrollPage)
            {
                Console.WriteLine("| Press W To Scroll Up");
                Console.WriteLine("| Press S To Scroll Down");
            }
            if (currentPage is IAction action)
            {
                Console.WriteLine($"| Press X To {action.ActionName}");
            }
            else if (currentPage is IRootPage)
            {
                Console.WriteLine($"| Press X To Go Next page");
            }
            if (NavigationController.GetCurrentStackCount() != 1)
            {
                Console.WriteLine($"| Press B To Back Previous Page");
            }
            if (AppState.IsAuthenticated() || AppState.IsGuest)
            {
                Console.WriteLine($"| Press L To Logout");
            }
            {
                Console.WriteLine($"| Press E to Save and Exit");
            }
        }

        protected void SetPageContentOnBoardGrids()
        {
            absBasePage currentPage = NavigationController.GetCurrentPage();

            currentPage.ResetContent();

            string[] content = currentPage.ContentGrids;

            int[] rows = { Row1, Row2, Row3, Row4 };
            int[] cols = { Col1, Col2, Col3 };

            int index = 0;
            for (int r = 0; r < rows.Length; r++)
            {
                for (int c = 0; c < cols.Length; c++)
                {
                    if (index < content.Length)
                        SetContentOnGrid(content[index++], rows[r], cols[c]);
                }
            }
        }

        protected void SetBoardDefault()
        {
            for (int h = 0; h < Board.Length; h++)
            {
                for (int w = 0; w < Width; w++)
                {
                    Board[h][w] = ' ';
                }
            }
        }

        protected void SetScrollBarOnBoard()
        {
            if (NavigationController.GetCurrentPage() is not absScrollPage scrollPage)
            {
                return;
            }

            int rowCount = scrollPage.GetRowCount();

            if (rowCount == 0)
            {
                return;
            }

            int scrollBarLength = absScrollPage.PAGE_ROWS_LIMIT * 15 / rowCount;
            int scrollBarStartFromLength = scrollPage.StartCursor * (15 + scrollBarLength) / rowCount;

            if (scrollBarLength >= 15)
                return;

            SetScrollBarBoxOnBoard();

            scrollBarLength = scrollBarLength < 3 ? 3 : scrollBarLength;
            scrollBarStartFromLength = scrollBarStartFromLength > 14 ? 14 : scrollBarStartFromLength;

            int i = 8 + scrollBarStartFromLength;

            while (i < 25 && scrollBarLength-- > 0)
            {
                Board[i][78] = '=';
                i++;
            }

        }

        protected void SetScrollBarBoxOnBoard()
        {
            SetVerticalLineOnBoard(77, 7, 25);
            SetVerticalLineOnBoard(79, 7, 25);
            Board[7][78] = '-';
            Board[25][78] = '-';

            Board[7][77] = '+';
            Board[7][79] = '+';

            Board[25][77] = '+';
            Board[25][79] = '+';
        }

        protected void SetCursorOnBoard()
        {
            if (NavigationController.GetCurrentPage() is absScrollSelection)
            {
                SetCursorOnBoard(GetRowByCursorPosition(GetCursorPosition()), 1);
            }
        }

        protected int GetCursorPosition()
        {
            if (NavigationController.GetCurrentPage() is absScrollSelection dynamicPage)
            {
                return dynamicPage.SelectionCursor - dynamicPage.StartCursor + 1;
            }
            return -1;
        }

        int GetRowByCursorPosition(int cursorPosition)
        {
            int[] rows = { Row1, Row2, Row3, Row4 };

            return cursorPosition >= 0 && cursorPosition < rows.Length ?
               rows[cursorPosition] : Row1;
        }

        protected void SetCursorOnBoard(int row, int col)
        {
            int endContent = col + GridWidth;

            int i = col + GridWidth + 3;
            while (i > col)
            {
                if (Board[row][i] != ' ')
                {
                    endContent = i + 2;
                    break;
                }
                i--;
            }

            if (i == col)
                return;

            Board[row][3] = '{';
            Board[row][endContent] = '}';
        }

        protected void SetContentOnGrid(string content, int height, int width)
        {
            if (string.IsNullOrWhiteSpace(content)) return;

            int startWidth = width;
            int startHeight = height;
            int maxWidth = width + GridWidth;
            int maxHeight = height + GridHeight;

            string[] words = content.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                RenderWord(words[i], ref width, ref height, startHeight, startWidth, maxWidth, maxHeight);

                if (i < words.Length - 1 && width < maxWidth)
                {
                    RenderWord(" ", ref width, ref height, startHeight, startWidth, maxWidth, maxHeight);
                }
            }
        }

        protected void RenderWord(string word, ref int x, ref int y, int startY, int startX, int maxX, int maxY)
        {
            if (ShouldWrapToNextLine(word, x, maxX))
            {
                y++;
                x = startX;
            }

            int wordIndex = 0;

            while (wordIndex < word.Length && y < maxY)
            {
                while (IsLineStartWithSpace(x, startX, word[wordIndex]))
                {
                    wordIndex++;

                    if (wordIndex == word.Length) return;
                }

                if (HandleSpecialTags(word, ref wordIndex, ref x, ref y, startX))
                {
                    continue;
                }

                HandleLongWordHyphen(ref x, ref y, startX, maxX, word, wordIndex);

                if (y < maxY)
                {
                    Board[y][x] = word[wordIndex];
                    x++;
                    wordIndex++;
                }

                if (x >= maxX)
                {
                    y++;
                    x = startX;
                }
            }

            //HandleGridExceed(y, maxY, maxX);
        }

        protected bool IsLineStartWithSpace(int currentX, int startX, char currentChar)
        {
            return currentX == startX && currentChar == ' ';
        }

        protected bool ShouldWrapToNextLine(string word, int currentX, int maxX)
        {
            return word != " " &&
                   word.Length + currentX >= maxX &&
                   word.Length < GridWidth;
        }

        private bool HandleSpecialTags(string word, ref int wordIndex, ref int x, ref int y, int startX)
        {
            if (clsValidation.SubWordExists(word, clsCustomTags.LineBreak, wordIndex))
            {
                y++;
                x = startX;
                wordIndex += 2;
                return true;
            }
            return false;
        }

        private void HandleLongWordHyphen(ref int x, ref int y, int startX, int maxX, string word, int wordIndex)
        {
            if (x + 1 == maxX && wordIndex + 1 < word.Length && word[wordIndex] != ' ')
            {
                Board[y][x] = '-';
                y++;
                x = startX;
            }
        }

        //private void HandleGridExceed(int y, int maxY, int maxX)
        //{
        //    if (y >= maxY)
        //    {
        //        SetDotsOnLastOfGrid(maxX, maxY);
        //    }
        //}

        //private void SetDotsOnLastOfGrid(int maxX, int maxY)
        //{
        //    int startIndex = maxX;
        //    while (Board[maxY - 1][startIndex] == ' ')
        //    {
        //        startIndex--;
        //    }

        //    Board[maxY - 1][startIndex] = '.';
        //    Board[maxY - 1][startIndex + 1] = '.';
        //    Board[maxY - 1][startIndex + 2] = '.';
        //}

        protected void PrintHorizontalLine(int length)
        {
            Console.WriteLine();
            for (int i = 0; i < length; i++)
            {
                if (i == 0 || i == length - 1)
                {
                    Console.Write('*');
                }
                else
                {
                    Console.Write('-');
                }
            }
            Console.WriteLine();
        }

        protected void SetVerticalLineOnBoard(int col, int startRow, int endRow)
        {
            for (int i = startRow; i <= endRow; i++)
            {
                if (Board[i][col] == '-')
                {
                    Board[i][col] = '*';
                }
                else
                {
                    Board[i][col] = '|';
                }
            }
        }

        protected void SetHorizontalLineOnBoard(int row)
        {
            for (int i = 0; i < BoarderWidth; i++)
            {
                if (Board[row][i] == '|')
                {
                    Board[row][i] = '*';
                }
                else
                {
                    Board[row][i] = '-';
                }
            }
        }

        protected void PrintBoard()
        {
            for (int h = 0; h < Board.Length; h++)
            {
                for (int w = 0; w < Width; w++)
                {
                    if (h == 0 || h == Board.Length - 1)
                    {
                        if (w == 0 || w == BoarderWidth - 1)
                        {
                            Console.Write('*');
                        }
                        else if(w > 0 && w < BoarderWidth - 1)
                        {
                            Console.Write('-');
                        }
                    }
                    else if (w == 0 || w == BoarderWidth - 1)
                    {
                        Console.Write('|');
                    }
                    else
                    {
                        Console.Write(Board[h][w]);
                    }
                }
                Console.WriteLine();
            }
        }


    }
}
