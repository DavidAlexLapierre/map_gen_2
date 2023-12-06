using System;

namespace Engine.Utils {
    static class Logger {
        public static void Log(string msg) {
            Console.WriteLine("MESSAGE: " + msg);
        }

        public static void LogError(string msg) {
            Console.WriteLine("ERROR: " + msg);
        }
    }
}