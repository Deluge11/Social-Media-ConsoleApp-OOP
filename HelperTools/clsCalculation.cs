


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
    }
}
