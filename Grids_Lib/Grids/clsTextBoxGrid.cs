
using Grids_Lib.Enums;

namespace Grids
{
    public class clsTextBoxGrid : absBaseGrid
    {
        private string _text = "";
        public enAlignment Alignment { get; set; } = enAlignment.Left;

        public string Text
        {
            get => _text;
            set
            {
                if (_text == value) return;
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
                ApplyTextAlignmentOnArray(ContentBoard[y]);
            }
        }

        private void ApplyTextAlignmentOnArray(char[] charArr)
        {
            int leftGap = GetLeftGap(charArr);
            int rightGap = GetRightGap(charArr);

            switch (Alignment)
            {
                case enAlignment.Left:

                    while (leftGap > 0)
                    {
                        OffsetToLeft(charArr);
                        leftGap--;
                    }

                    break;

                case enAlignment.Center:

                    while (rightGap > leftGap)
                    {
                        OffsetToRight(charArr);
                        rightGap--;
                        leftGap++;
                    }

                    while (rightGap < leftGap)
                    {
                        OffsetToLeft(charArr);
                        rightGap++;
                        leftGap--;
                    }

                    break;

                case enAlignment.Right:

                    while (rightGap > 0)
                    {
                        OffsetToRight(charArr);
                        rightGap--;
                    }

                    break;

            }
            if (Alignment == enAlignment.Right)
            {

            }
            else if (Alignment == enAlignment.Center)
            {

            }
            else
            {

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

        private void OffsetToRight(char[] charArr)
        {
            for (int i = charArr.Length - 1; i > 0; i--)
            {
                charArr[i] = charArr[i - 1];
            }
            charArr[0] = ' ';
        }

        private void OffsetToLeft(char[] charArr)
        {
            for (int i = 0; i < charArr.Length - 1; i++)
            {
                charArr[i] = charArr[i + 1];
            }
            charArr[charArr.Length - 1] = ' ';
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
