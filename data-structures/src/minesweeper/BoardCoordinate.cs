using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_structures.src.minesweeper
{
    public class BoardCoordinate
    {
        public int row;
        public int column;

        public BoardCoordinate(int row, int column)
        {
            this.row = row;
            this.column = column;
        }
    }
}
