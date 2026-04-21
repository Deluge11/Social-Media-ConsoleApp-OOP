using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.HelperTools
{
    public static class clsConsoleInput
    {
        public static string GetStringInput(string message)
        {
            Console.Write($"| {message} : ");
            string text = Console.ReadLine()!;
            return text != null ? text.Trim() : "";
        }
    }
}
