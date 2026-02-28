using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp
{
    public static class clsConsoleInput
    {
        public static string GetStringInput(string message)
        {
            Console.WriteLine($"| {message}");
            Console.Write(" => ");
            string text = Console.ReadLine()!;
            return text != null ? text.Trim() : "";
        }
    }
}
