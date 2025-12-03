using SocialApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private char[][] Board = new char[26][];



        public RendererController(INavigationController navigationController)
        {
            for (int i = 0; i < Board.Length; i++)
            {
                Board[i] = new char[Width];
            }

            NavigationController = navigationController;
            SetBoardDefault();
        }

        public INavigationController NavigationController { get; }



        public void Print()
        {
            Console.Clear();

            SetBoardDefault();
            SetBoardContent();
            SetHorizontalLine(6);

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
            PrintControlKeys();
        }
        protected void PrintControlKeys()
        {
            IPage currentPage = NavigationController.GetCurrentPage();

            if(currentPage is IScrollPage)
            {
                Console.WriteLine("| Press W to scroll up");
                Console.WriteLine("| Press S to scroll down");
            }
            if (currentPage is IAction action)
            {
                Console.WriteLine($"| Press X to {action.ActionName}");
            }
            else if (currentPage is IRootPage)
            {
                Console.WriteLine($"| Press X to go next page");
            }
            Console.WriteLine($"| Press Q key to return");
        }
        protected void SetBoardContent()
        {
            IPage currentPage = NavigationController.GetCurrentPage();

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

            if (currentPage is IScrollCursor dynamicPage && currentPage is IScrollPage scrollingPage)
            {
                int rowPos = dynamicPage.Cursor - scrollingPage.Start + 1;
                SetCursor(content[3 * rowPos], rowPos);
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
        protected void SetCursor(string content, int pos)
        {
            if (string.IsNullOrEmpty(content))
            {
                return;
            }

            int h = 2;

            switch (pos)
            {
                case 0:
                    h = 2;
                    break;
                case 1:
                    h = 8;
                    break;
                case 2:
                    h = 14;
                    break;
                case 3:
                    h = 20;
                    break;
            }

            Board[h][3] = '<';

            int endContent = 6 + content.Length > 25 ? 26 : 6 + content.Length;

            Board[h][endContent] = '>';
        }
        protected void SetGrid(string content, int h, int w)
        {
            int startW = w;
            int startH = h;
            int nextW = w + 20;

            for (int x = 0; x < content.Length; x++, w++)
            {
                if (h - startH == 5) continue;

                if (h - startH == 4 && nextW - w == 4)
                {
                    content = "...";
                    x = 0;
                }

                if (x < content.Length - 1 && content[x] == '#' && content[x + 1] == 'h')
                {
                    h++;
                    w = startW;
                    x += 2;
                }
                else if (w == nextW)
                {
                    h++;
                    w = startW;

                    if (content[x] != ' ')
                    {
                        Board[h][w] = '-';
                        w++;
                    }
                }

                Board[h][w] = content[x];
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
    }
}
