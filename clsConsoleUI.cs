



namespace SocialApp
{
    public static class clsConsoleUI
    {
        public static void PrintMessage(string message)
        {
            Console.Clear();
            Console.WriteLine("-----------------------------------");
            Console.WriteLine($"\t{message}");
            Console.WriteLine("-----------------------------------");
        }

        public static void PressKeyToContinue()
        {
            Console.WriteLine("| Press Any Key To Continue");
            Console.ReadKey();
        }

    }
}
