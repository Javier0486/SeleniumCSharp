using System;

namespace MiProyectoPruebas.Utils
{
    public static class Logger
    {
        public static void LogAction(string message)
        {
            Console.WriteLine($"[{DateTime.Now}] {message}");
        }
    }
}