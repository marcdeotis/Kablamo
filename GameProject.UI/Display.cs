using GameProject.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.UI
{
    public static class Display
    {
        public static void DisplayBoard(Board board)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int k = 0; k < 10; k++)
                {
                    SquareStatus temp = board.BoardState[new Coordinate(i, k)];

                    switch (temp.ToString())
                    {
                        case "Empty":
                            Console.Write("O");
                            break;
                        case "Wall":
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.Write("W");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        case "BlueBase":
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write("B");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        case "RedBase":
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("B");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        case "SingleBlue":
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write("S");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        case "DoubleBlue":
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write("D");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        case "SingleRed":
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("S");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        case "DoubleRed":
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("D");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
