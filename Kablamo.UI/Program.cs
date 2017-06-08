using Kablamo.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kablamo.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            Display.DisplayBoard(board);
            while(true)
            {
                string coord = Console.ReadLine();
                string[] array = coord.Split(',');
                Coordinate test = new Coordinate(int.Parse(array[0]), int.Parse(array[1]));
                Display.DisplayBoard(board.MakeMove(test));
            }
            
        }
    }
}
