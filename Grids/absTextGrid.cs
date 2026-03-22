using SocialApp.Abstractions.Base;
using SocialApp.HelperTools;

namespace SocialApp.Grids
{
    public abstract class absTextGrid : absBaseGrid
    {
        public abstract string Content { get; set; }

        protected override void SetContent()
        {
            SetTextOnContentBoard(Content);
        }

        private void SetTextOnContentBoard(string content)
        {
            if (string.IsNullOrWhiteSpace(content)) return;

            string[] words = content.Split(' ');

            int currentHeight = 0;
            int currentWidth = 0;

            for (int i = 0; i < words.Length; i++)
            {
                RenderWord(words[i], ref currentWidth, ref currentHeight);

                if (i >= words.Length - 1) continue;

                RenderWord(" ", ref currentWidth, ref currentHeight);
            }
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

                HandleLongWordHyphen(ref currentWidth, ref currentHeight, word, wordIndex);

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

        private void HandleLongWordHyphen(ref int currentWidth, ref int currentHeight, string word, int wordIndex)
        {
            if (currentWidth + 1 == ContentBoardWidth && wordIndex + 1 < word.Length && word[wordIndex] != ' ')
            {
                if (wordIndex > 0)
                {
                    ContentBoard[currentHeight][currentWidth] = '-';
                }

                StartFromNextLine(ref currentWidth, ref currentHeight);
            }
        }

        private void StartFromNextLine(ref int currentWidth, ref int currentHeight)
        {
            currentHeight++;
            currentWidth = 0;
        }



    }
}
