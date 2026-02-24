
using SocialApp.Abstractions;
using SocialApp.Interfaces;


namespace SocialApp.Controllers
{
    public class RendererController
    {
        private const int Col1 = 5;
        private const int Col2 = 28;
        private const int Col3 = 51;

        private const int Row1 = 2;
        private const int Row2 = 8;
        private const int Row3 = 14;
        private const int Row4 = 20;

        private const int Width = 75;

        private const int GridWidth = 20;
        private const int GridHeight = 5;

        private char[][] Board = new char[26][];

        public AppState AppState { get; }
        public NavigationController NavigationController { get; }


        public RendererController(AppState appState, NavigationController navigationController)
        {
            for (int i = 0; i < Board.Length; i++)
            {
                Board[i] = new char[Width];
            }

            NavigationController = navigationController;
            AppState = appState;
            SetBoardDefault();
        }

        public void Print()
        {
            Console.Clear();
            BoardProcessing();
            PrintPagesStackBox();
            PrintBoard();
            PrintControlKeys();
        }

        protected void BoardProcessing()
        {
            SetBoardDefault();
            SetPageContentOnBoardGrids();
            SetCursorOnBoard();
            SetHorizontalLineOnBoard(6);
        }

        protected void PrintPagesStackBox()
        {
            var pagesName = NavigationController.GetPagesNames();
            string separator = " -> ";

            int pagesNameTotalLength = GetStringListLengthWithSeparation(pagesName, separator.Length) + 5;

            PrintHorizontalLine(pagesNameTotalLength);
            Console.Write("| ");
            PrintPagesStackWithSeparator(pagesName, separator);
            Console.Write("  |");
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

        protected int GetStringListLengthWithSeparation(List<string> array, int separate)
        {
            int total = separate * array.Count;
            for (int i = 0; i < array.Count; i++)
            {
                total += array[i].Length;
            }
            return total;
        }

        protected void PrintControlKeys()
        {
            AbPage currentPage = NavigationController.GetCurrentPage();

            if (currentPage is AbScrollPage)
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
            if (AppState.IsAuthenticated)
            {
                Console.WriteLine($"| Press L To Logout");
            }
            {
                Console.WriteLine($"| Press E to Save and Exit");
            }
        }

        protected void SetPageContentOnBoardGrids()
        {
            AbPage currentPage = NavigationController.GetCurrentPage();

            currentPage.ResetContent();
            currentPage.SetPageContent();

            var content = currentPage.ContentGrids;

            SetContentOnGrid(content[0], Row1, Col1);
            SetContentOnGrid(content[1], Row1, Col2);
            SetContentOnGrid(content[2], Row1, Col3);
            SetContentOnGrid(content[3], Row2, Col1);
            SetContentOnGrid(content[4], Row2, Col2);
            SetContentOnGrid(content[5], Row2, Col3);
            SetContentOnGrid(content[6], Row3, Col1);
            SetContentOnGrid(content[7], Row3, Col2);
            SetContentOnGrid(content[8], Row3, Col3);
            SetContentOnGrid(content[9], Row4, Col1);
            SetContentOnGrid(content[10], Row4, Col2);
            SetContentOnGrid(content[11], Row4, Col3);
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

        protected void SetCursorOnBoard()
        {
            if (NavigationController.GetCurrentPage() is AbScrollCursor)
            {
                SetCursorOnBoard(GetRowByCursorPosition(GetCursorPosition()), 1);
            }
        }

        protected int GetCursorPosition()
        {
            if (NavigationController.GetCurrentPage() is AbScrollCursor dynamicPage)
            {
                return dynamicPage.Cursor - dynamicPage.Start + 1;
            }
            return -1;
        }

        int GetRowByCursorPosition(int cursorPosition)
        {
            switch (cursorPosition)
            {
                case 0:
                    return Row1;
                case 1:
                    return Row2;
                case 2:
                    return Row3;
                case 3:
                    return Row4;
            }
            return Row1;
        }

        protected void SetCursorOnBoard(int row, int col)
        {
            int endContent = col + GridWidth;

            int i = col + GridWidth;
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
            if (content == null || content == "") return;

            int startWidth = width;
            int startHeight = height;
            int maxWidth = width + GridWidth;

            string[] words = content.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                SetWord(words[i], ref width, ref height, startHeight, startWidth, maxWidth);

                if (i + 1 < words.Length)
                {
                    SetWord(" ", ref width, ref height, startHeight, startWidth, maxWidth);
                }
            }
        }

        protected void SetWord(string word, ref int width, ref int height, int startHeight, int startWidth, int maxWidth)
        {
            if (word == " " && width == startWidth)
            {
                return;
            }

            if (word.Length + width >= maxWidth && word.Length < GridWidth)
            {
                height++;
                width = startWidth;
            }

            int wordIndex = 0;

            while (wordIndex < word.Length && height - startHeight < GridHeight)
            {
                if (height - startHeight == GridHeight - 1 &&
                    maxWidth < width + 5 &&
                    maxWidth - width < word.Length - wordIndex)
                {
                    word = "...";
                    wordIndex = 0;
                }

                while (SubWordExists(word, "#h", wordIndex))
                {
                    height++;
                    width = startWidth;
                    wordIndex += 2;
                }

                if (wordIndex >= word.Length)
                    return;

                if (width + 1 == maxWidth &&
                    wordIndex + 1 < word.Length &&
                    word[wordIndex] != ' ')
                {
                    Board[height][width] = '-';
                    height++;
                    width = startWidth;
                }

                if (width == maxWidth)
                {
                    height++;
                    width = startWidth;
                }

                Board[height][width] = word[wordIndex];

                wordIndex++;
                width++;
            }
        }

        protected bool SubWordExists(string word, string subWord, int wordIndex)
        {
            int subWordIndex = 0;
            while (wordIndex < word.Length)
            {
                if (word[wordIndex++] != subWord[subWordIndex++]) return false;
            }
            return subWordIndex == subWord.Length;
        }

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

        protected void SetHorizontalLineOnBoard(int row)
        {
            for (int i = 0; i < Board[row].Length; i++)
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
                        if (w == 0 || w == Width - 1)
                        {
                            Console.Write('*');
                        }
                        else
                        {
                            Console.Write('-');
                        }
                    }
                    else if (w == 0 || w == Width - 1)
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
