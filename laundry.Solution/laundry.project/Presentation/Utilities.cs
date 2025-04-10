using laundry.project.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laundry.project.Presentation
{
    public enum ConsoleZone { Left, Right };


    internal static class Uitility
    {
        static int _leftSide;
        static int _rightSide = Console.WindowWidth / 2;
        static int _leftTop;
        static int _rigtTop;
        static int _lastLeftStringwrittenLength;
        public static object _consoleLock = new object();
        public static object _consoleLockDroite = new object();
        public static void ClearHalfConsole(ConsoleZone consoleZone, int colonne, ref int ligne)
        {
            if (ligne < 50) return;
            int startColumn;
            // Calculer la colonne de départ (milieu de la console)
            if (consoleZone == ConsoleZone.Right)
                startColumn = Console.WindowWidth / 2;
            else
                startColumn = 0;
            // Parcourir toutes les lignes de la console
            for (int row = 0; row < ligne; row++)
            {
                // Positionner le curseur au début de la moitié droite
                Console.SetCursorPosition(startColumn, row);

                // Écrire des espaces pour effacer la moitié droite
                Console.Write(new string(' ', Console.WindowWidth / 2 - startColumn));
            }
            ligne = 1;

            Console.SetCursorPosition(++_lastLeftStringwrittenLength, ligne);


        }
        public static void WriteLeft(this string chaine, ConsoleColor color)
        {

            lock (_consoleLock)
            {
                ClearHalfConsole(ConsoleZone.Left, _leftSide, ref _leftTop);

                // Déplacer le curseur
                Console.SetCursorPosition(_leftSide, ++_leftTop);


                Console.ForegroundColor = color;
                Console.Write(chaine);
                _lastLeftStringwrittenLength = chaine.Length;
            }

        }
        public static void WriteRight(this string chaine, ConsoleColor color)
        {

            lock (_consoleLockDroite)
            {
                ClearHalfConsole(ConsoleZone.Left, _rightSide, ref _rigtTop);
               
                
                Console.SetCursorPosition(_rightSide, ++_rigtTop);

                Console.ForegroundColor = color;
                Console.Write(chaine);
                Console.SetCursorPosition(++_lastLeftStringwrittenLength, _leftTop);
            }
        }
        public static void WriteLineLeft(this string chaine, ConsoleColor color)
        {

            lock (_consoleLock)
            {
                ClearHalfConsole(ConsoleZone.Left, _leftSide, ref _leftTop);


                Console.SetCursorPosition(_leftSide, ++_leftTop);
                Console.ForegroundColor = color;
                Console.Write(chaine);
                _lastLeftStringwrittenLength = chaine.Length;
            }

        }
        public static void WriteLineRight(this string chaine, ConsoleColor color)
        {
            lock (_consoleLockDroite)
            {
                ClearHalfConsole(ConsoleZone.Right, _rightSide, ref _rigtTop);


                Console.SetCursorPosition(_rightSide, ++_rigtTop);
                Console.ForegroundColor = color;
                Console.Write(chaine);
                Console.SetCursorPosition(++_lastLeftStringwrittenLength, _leftTop);
            }

        }
        public static string ReadLineLeft(ConsoleColor color)
        {

            lock (_consoleLock)
            {
                ClearHalfConsole(ConsoleZone.Left, _leftSide, ref _leftTop);
                
                Console.SetCursorPosition(_leftSide, _leftTop);
                // Positionner le curseur dans la zone gauche
                Console.SetCursorPosition(_lastLeftStringwrittenLength + 1, _leftTop);
                Console.ForegroundColor = color;
                return Console.ReadLine();
            }
        }

    }
}
