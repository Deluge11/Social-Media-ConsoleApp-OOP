using SocialApp.Abstractions;
using SocialApp.Interfaces;
using SocialApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Controllers
{
    public class RendererController : IRendererController
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
        private INavigationController NavigationController { get; }


        public RendererController(AppState appState, INavigationController navigationController)
        {
            for (int i = 0; i < Board.Length; i++)
            {
                Board[i] = new char[Width];
            }

            AppState = appState;
            NavigationController = navigationController;
            SetBoardDefault();
        }

        public void Print()
        {
            Console.Clear();
            BoardProcessing();
            PrintPagesStack();
            PrintBoard();
            PrintControlKeys();
        }

        protected void BoardProcessing()
        {
            SetBoardDefault();
            SetBoardGrids();
            SetCursor();
            SetHorizontalLine(6);
        }
        protected void PrintPagesStack()
        {
            var pagesName = NavigationController.GetPagesStackNames();
            string separator = " -> ";

            int pagesNameTotalLength = GetStringListLengthWithSeparation(pagesName, separator.Length) + 5;

            PrintHorizontalLine(pagesNameTotalLength);
            Console.Write("| ");

            for (int i = pagesName.Count - 1; i >= 0; i--)
            {
                Console.Write(separator);
                Console.Write(pagesName[i]);

            }

            Console.Write("  |");
            PrintHorizontalLine(pagesNameTotalLength);

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
        protected void PrintControlKeys()
        {
            AbPage currentPage = NavigationController.GetCurrentPage();

            if (currentPage is AbScrollPage)
            {
                Console.WriteLine("| Press W to Scroll up");
                Console.WriteLine("| Press S to Scroll down");
            }
            if (currentPage is IAction action)
            {
                Console.WriteLine($"| Press X to {action.ActionName}");
            }
            else if (currentPage is IRootPage)
            {
                Console.WriteLine($"| Press X to Go Next page");
            }
            if (NavigationController.GetStackCount() != 1)
            {
                Console.WriteLine($"| Press B to Back Previous Page");
            }
            if (AppState.IsAuthenticated)
            {
                Console.WriteLine($"| Press L to Logout");
            }
            {
                Console.WriteLine($"| Press E to Save and Exit");
            }
        }
        protected void SetBoardGrids()
        {
            AbPage currentPage = NavigationController.GetCurrentPage();

            currentPage.ResetContent();
            currentPage.SetPageContent();

            var content = currentPage.ContentGrids;

            SetGrid(content[0], Row1, Col1);
            SetGrid(content[1], Row1, Col2);
            SetGrid(content[2], Row1, Col3);
            SetGrid(content[3], Row2, Col1);
            SetGrid(content[4], Row2, Col2);
            SetGrid(content[5], Row2, Col3);
            SetGrid(content[6], Row3, Col1);
            SetGrid(content[7], Row3, Col2);
            SetGrid(content[8], Row3, Col3);
            SetGrid(content[9], Row4, Col1);
            SetGrid(content[10], Row4, Col2);
            SetGrid(content[11], Row4, Col3);
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
        protected void SetCursor()
        {
            AbPage currentPage = NavigationController.GetCurrentPage();

            if (currentPage is not AbScrollCursor)
            {
                return;
            }

            int curserPosition = GetCursorPosition();
            string contentLength = GetContentLength(curserPosition);
            SetCursorOnBoard(contentLength, curserPosition);
        }
        protected string GetContentLength(int rowPosition)
        {
            AbPage currentPage = NavigationController.GetCurrentPage();
            var contents = currentPage.ContentGrids;
            return contents[3 * rowPosition];
        }
        protected int GetCursorPosition()
        {
            AbPage currentPage = NavigationController.GetCurrentPage();
            if (currentPage is not AbScrollCursor dynamicPage)
                return -1;

            return dynamicPage.Cursor - dynamicPage.Start + 1;
        }
        protected void SetCursorOnBoard(string content, int pos)
        {
            if (string.IsNullOrEmpty(content.Trim()))
            {
                return;
            }

            int h = Row1;

            switch (pos)
            {
                case 0:
                    h = Row1;
                    break;
                case 1:
                    h = Row2;
                    break;
                case 2:
                    h = Row3;
                    break;
                case 3:
                    h = Row4;
                    break;
            }

            int endContent = Col1 + GridWidth;

            for (int i = Col1 + GridWidth; i > Col1; i--)
            {
                if (Board[h][i] != ' ') break;
                endContent = i + 1;
            }


            Board[h][3] = '{';
            Board[h][endContent] = '}';
        }

        protected void SetGrid(string content, int height, int width)
        {
            if (content == null || content == "")
            {
                return;
            }

            int startWidth = width;
            int startHeight = height;
            int maxWidth = width + GridWidth;

            string[] words = content.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                SetWord(words[i], ref width, ref height, startHeight, startWidth, maxWidth);

                if (i + 1 < words.Length)
                    SetWord(" ", ref width, ref height, startHeight, startWidth, maxWidth);
            }
        }

        protected void SetWord(string word, ref int width, ref int height, int startHeight, int startWidth, int maxWidth)
        {
            if (word.Length + width >= maxWidth && word.Length < GridWidth)
            {
                height++;
                width = startWidth;
            }

            if (word == " " && width == startWidth)
            {
                return;
            }

            for (int x = 0; x < word.Length; x++, width++)
            {
                if (height - startHeight >= GridHeight) continue;

                if (height - startHeight == GridHeight - 1 && maxWidth < width + 5 && maxWidth - width < word.Length - x)
                {
                    word = "...";
                    x = 0;
                }

                if (x < word.Length - 1 && word[x] == '#' && word[x + 1] == 'h')
                {
                    height++;
                    width = startWidth;
                    x += 2;
                    continue;
                }

                else if (width == maxWidth)
                {
                    height++;
                    width = startWidth;

                    if (word[x - 1] != ' ' && word[x] != ' ')
                    {
                        Board[height][width] = '-';
                        width++;
                    }
                    if (word[x] == ' ')
                    {
                        x++;
                    }
                }

                Board[height][width] = word[x];
            }
        }

        protected void SetHorizontalLine(int row)
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
