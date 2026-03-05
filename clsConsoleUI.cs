



namespace SocialApp
{
    public static class clsConsoleUI
    {
        public static void PrintMessage(string message)
        {
            Console.WriteLine(new string('-', message.Length + 15));
            Console.WriteLine($"\t{message}");
            Console.WriteLine(new string('-', message.Length + 15));
        }

        public static void PressKeyToContinue()
        {
            Console.WriteLine("| Press Any Key To Continue");
            Console.ReadKey();
        }

    }
}
