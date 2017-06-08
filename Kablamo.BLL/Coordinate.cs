using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kablamo.BLL
{
    public class Coordinate
    {
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }


        public Coordinate(int x, int y)
        {
            XCoordinate = x;
            YCoordinate = y;
        }
        public override bool Equals(object obj)
        {
            Coordinate otherCoordinate = obj as Coordinate;

            if (otherCoordinate == null)
                return false;

            return otherCoordinate.XCoordinate == this.XCoordinate &&
                   otherCoordinate.YCoordinate == this.YCoordinate;
        }

        public override int GetHashCode()
        {
            string uniqueHash = this.XCoordinate.ToString() + this.YCoordinate.ToString() + "00";
            return (Convert.ToInt32(uniqueHash));
        }

        public static Coordinate operator+ (Coordinate a, Coordinate b)
        {
            Coordinate toReturn = new Coordinate(a.XCoordinate, a.YCoordinate);
            toReturn.XCoordinate += b.XCoordinate;
            toReturn.YCoordinate += b.YCoordinate;
            return toReturn;
        }

        
    }
}
