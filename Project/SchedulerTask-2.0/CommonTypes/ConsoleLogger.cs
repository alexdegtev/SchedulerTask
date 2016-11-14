using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonTypes
{
    public class ConsoleLogger
    {
        private static bool isEnabled = false;

        public static void Enable()
        {
            isEnabled = true;
        }

        public static void Disable()
        {
            isEnabled = false;
        }

        public static void Log(string message)
        {
            if (isEnabled)
                Console.WriteLine(message);
        }
    }
}
