


namespace SocialApp.HelperTools
{
    public static class clsCalculation
    {
        public static int GetStringListLengthWithSeparation(List<string> array, int separate)
        {
            if (array.Count == 0)
            {
                return 0;
            }

            int total = separate * (array.Count - 1);

            for (int i = 0; i < array.Count; i++)
            {
                total += array[i].Length;
            }
            return total;
        }

        public static string FormatNumberAtSize(int number, int size)
        {
            string strNumber = number.ToString();
            return new string('0', strNumber.Length >= size ? 0 : size - strNumber.Length) + strNumber;
        }
    }
}
