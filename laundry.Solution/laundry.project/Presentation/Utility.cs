using laundry.project.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laundry.project.Presentation
{
    public enum ConsoleZone { Left, Right };
    internal static class Utility
    {
        static int _leftSide;
        static int _rightSide = Console.WindowWidth / 2;
        static int _leftTop;
        static int _rightTop;
        static int _lastLeftStringWrittenLength;

        public static object _consoleLock = new object();
        public static object _consoleLockRight = new object();

        public static void ClearHalfConsole(ConsoleZone consoleZone, int column, ref int line)
        {
            if (line < 50) return; // Avoid clearing if the line is less than 50

            int startColumn;
            if (consoleZone == ConsoleZone.Right)
                startColumn = Console.WindowWidth / 2;
            else
                startColumn = 0;

            for (int row = 0; row < line; row++)
            {
                Console.SetCursorPosition(startColumn, row);
                Console.Write(new string(' ', Console.WindowWidth / 2 - startColumn));
            }
            line = 1;

            Console.SetCursorPosition(++_lastLeftStringWrittenLength, line);
        }
        public static void WriteLeft(this string message, ConsoleColor color)
        {
            lock (_consoleLock)
            {
                _leftTop = (_leftTop + 1) % Console.WindowHeight;
                ClearHalfConsole(ConsoleZone.Left, _leftSide, ref _leftTop);
                Console.SetCursorPosition(_leftSide, _leftTop);
                Console.ForegroundColor = color;
                Console.Write(message);
                _lastLeftStringWrittenLength = message.Length;
            }
        }
        public static void WriteRight(this string message, ConsoleColor color)
        {
            lock (_consoleLockRight)
            {
                ClearHalfConsole(ConsoleZone.Left, _rightSide, ref _rightTop);
                Console.SetCursorPosition(_rightSide, ++_rightTop);
                Console.ForegroundColor = color;
                Console.Write(message);
                Console.SetCursorPosition(++_lastLeftStringWrittenLength, _leftTop);
            }
        }
        public static void WriteLineLeft(this string message, ConsoleColor color)
        {
            lock (_consoleLock)
            {
                ClearHalfConsole(ConsoleZone.Left, _leftSide, ref _leftTop);
                _leftTop = (_leftTop + 1) % Console.WindowHeight; 
                Console.SetCursorPosition(_leftSide, _leftTop);
                Console.ForegroundColor = color;
                Console.Write(message);
                _lastLeftStringWrittenLength = message.Length;
            }
        }
        public static void WriteLineRight(this string message, ConsoleColor color)
        {
            lock (_consoleLockRight)
            {
                ClearHalfConsole(ConsoleZone.Right, _rightSide, ref _rightTop);
                Console.SetCursorPosition(_rightSide, ++_rightTop);
                Console.ForegroundColor = color;
                Console.Write(message);
                Console.SetCursorPosition(++_lastLeftStringWrittenLength, _leftTop);
            }
        }
        public static string ReadLineLeft(ConsoleColor color)
        {
            lock (_consoleLock)
            {
                ClearHalfConsole(ConsoleZone.Left, _leftSide, ref _leftTop);
                Console.SetCursorPosition(_lastLeftStringWrittenLength + 1, _leftTop);
                Console.ForegroundColor = color;
                return Console.ReadLine();
            }
        }
    }
}
