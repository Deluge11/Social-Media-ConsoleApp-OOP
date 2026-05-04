
using Grids_Lib.Enums;

namespace Grids
{
    public class clsTextBoxGrid : absBaseGrid
    {
        private string _text = "";
        private enAlignment _alignment = enAlignment.Left;
        public enAlignment Alignment
        {
            get
            {
                return _alignment;
            }
            set
            {
                _alignment = value;
                UpdateBoard();
            }
        }

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                UpdateBoard();
            }
        }

        public clsTextBoxGrid(int width, int height, stPaddingInfo padding)
            : base(width, height, padding, new stBoarderInfo('-', '|', '*'))
        {
            UpdateBoard();
        }

        protected override void SetContent()
        {
            if (string.IsNullOrWhiteSpace(Text)) return;

            string[] words = Text.Split(' ');

            int currentHeight = 0;
            int currentWidth = 0;

            for (int i = 0; i < words.Length; i++)
            {
                RenderWord(words[i], ref currentWidth, ref currentHeight);

                if (i >= words.Length - 1) continue;

                RenderWord(" ", ref currentWidth, ref currentHeight);
            }

            ApplyTextAlignment();
        }

        private void RenderWord(string word, ref int currentWidth, ref int currentHeight)
        {
            if (ShouldWrapToNextLine(word, currentWidth))
            {
                StartFromNextLine(ref currentWidth, ref currentHeight);
            }

            int wordIndex = 0;

            while (wordIndex < word.Length && currentHeight < ContentBoardHeight)
            {
                while (IsLineStartWithSpace(currentWidth, word[wordIndex]))
                {
                    wordIndex++;

                    if (wordIndex == word.Length) return;
                }

                if (HandleSpecialTags(word, ref wordIndex, ref currentWidth, ref currentHeight))
                {
                    continue;
                }

                // HandleLongWordHyphen(ref currentWidth, ref currentHeight, word, wordIndex);

                if (currentHeight < ContentBoardHeight)
                {
                    ContentBoard[currentHeight][currentWidth] = word[wordIndex];
                    currentWidth++;
                    wordIndex++;
                }

                if (currentWidth >= ContentBoardWidth)
                {
                    StartFromNextLine(ref currentWidth, ref currentHeight);
                }
            }

            //HandleGridExceed(y, maxY, maxX);
        }

        private bool IsLineStartWithSpace(int currentX, char currentChar)
        {
            return currentX == 0 && currentChar == ' ';
        }

        private bool ShouldWrapToNextLine(string word, int currentX)
        {
            return word != " " &&
                   word.Length + currentX >= ContentBoardWidth &&
                   word.Length < ContentBoardWidth;
        }

        private bool HandleSpecialTags(string word, ref int wordIndex, ref int currentWidth, ref int currentHeight)
        {
            if (clsValidation.SubWordExists(word, clsCustomTags.LineBreak, wordIndex))
            {
                StartFromNextLine(ref currentWidth, ref currentHeight);
                wordIndex += clsCustomTags.LineBreak.Length;
                return true;
            }
            return false;
        }

        private void ApplyTextAlignment()
        {
            for (int y = 0; y < ContentBoard.Length; y++)
            {
                ApplyTextAlignment(ContentBoard[y]);
            }
        }

        private void ApplyTextAlignment(char[] charArr)
        {
            int leftGap = GetLeftGap(charArr);
            int rightGap = GetRightGap(charArr);

            switch (Alignment)
            {
                case enAlignment.Left:

                    OffsetToLeft(charArr, leftGap);
                    break;

                case enAlignment.Center:

                    if (rightGap > leftGap)
                    {
                        OffsetToRight(charArr, (rightGap - leftGap) / 2);
                    }
                    else
                    {
                        OffsetToLeft(charArr, (leftGap - rightGap) / 2);
                    }

                    break;

                case enAlignment.Right:

                    OffsetToRight(charArr, rightGap);
                    break;

            }
        }

        private int GetLeftGap(char[] charArr)
        {
            int leftGap = 0;
            for (int i = 0; i < charArr.Length && charArr[i] == ' '; i++)
            {
                leftGap++;
            }
            return leftGap;
        }

        private int GetRightGap(char[] charArr)
        {
            int rightGap = 0;
            for (int i = charArr.Length - 1; i >= 0 && charArr[i] == ' '; i--)
            {
                rightGap++;
            }
            return rightGap;
        }

        private void OffsetToRight(char[] charArr, int offset)
        {
            int i = charArr.Length - 1;
            while (i >= offset)
            {
                charArr[i] = charArr[i - offset];
                i--;
            }
            while (i >= 0)
            {
                charArr[i] = ' ';
                i--;
            }
        }

        private void OffsetToLeft(char[] charArr, int offset)
        {
            int i = 0;
            while (i < charArr.Length - 1 - offset)
            {
                charArr[i] = charArr[i + offset];
                i++;
            }
            while (i < charArr.Length - 1)
            {
                charArr[i] = ' ';
                i++;
            }
        }

        //private void HandleLongWordHyphen(ref int currentWidth, ref int currentHeight, string word, int wordIndex)
        //{
        //    if (currentWidth + 1 == ContentBoardWidth && wordIndex + 1 < word.Length && word[wordIndex] != ' ')
        //    {
        //        if (wordIndex > 0)
        //        {
        //            ContentBoard[currentHeight][currentWidth] = '-';
        //        }

        //        StartFromNextLine(ref currentWidth, ref currentHeight);
        //    }
        //}

        private void StartFromNextLine(ref int currentWidth, ref int currentHeight)
        {
            currentHeight++;
            currentWidth = 0;
        }



    }
}
