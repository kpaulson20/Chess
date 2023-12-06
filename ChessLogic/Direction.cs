using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class Direction
    {                                                                   // Geographical      
        public readonly static Direction Up = new Direction(-1, 0);     //    North  
        public readonly static Direction Down = new Direction(1, 0);    //    South
        public readonly static Direction Right = new Direction(0, 1);   //    East
        public readonly static Direction Left = new Direction(0, -1);   //    West

        public readonly static Direction Diagonal1 = Up + Right;        //     NE
        public readonly static Direction Diagonal2 = Up + Left;         //     NW
        public readonly static Direction Diagonal3 = Down + Right;      //     SE
        public readonly static Direction Diagonal4 = Down + Left;       //     SW

        public int RowDelta {  get; }
        public int ColumnDelta { get; }

        public Direction(int rowDelta, int columnDelta)
        {
            RowDelta = rowDelta;
            ColumnDelta = columnDelta;
        }

        public static Direction operator +(Direction dir1, Direction dir2) 
        {
            return new Direction(dir1.RowDelta + dir2.RowDelta, dir1.ColumnDelta + dir2.ColumnDelta);
        }
        public static Direction operator *(int scalar, Direction dir)
        {
            return new Direction(scalar * dir.RowDelta, scalar * dir.ColumnDelta);
        }
    }
}
