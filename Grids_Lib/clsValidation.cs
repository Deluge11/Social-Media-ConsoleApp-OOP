

namespace Grids
{
    public static class clsValidation
    {
       public static bool SubWordExists(string word, string subWord, int wordIndex)
        {
            int subWordIndex = 0;
            while (wordIndex < word.Length && subWordIndex < subWord.Length)
            {
                if (word[wordIndex++] != subWord[subWordIndex++]) return false;
            }
            return subWordIndex == subWord.Length;
        }
    }
}
