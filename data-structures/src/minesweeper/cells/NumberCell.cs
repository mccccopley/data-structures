using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_structures.src.minesweeper
{
    public class NumberCell : Cell
    {
        private static char[] NumberToChar = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        int numberOfNeighboringBombs;

        public NumberCell(Board board, int row, int column, int neighboringBombCount)
            : base(board, row, column)
        {
            this.numberOfNeighboringBombs = neighboringBombCount;
        }

        public override char GetDisplayChar()
        {
            return NumberToChar[this.numberOfNeighboringBombs];
        }

        public override bool IsBomb()
        {
            return false;
        }

        public override ShowResult Show()
        {
            ShowResult showResult = new ShowResult();
            if (this.state == CellState.Hidden)
            {
                showResult.numberNormalCellsRevealed = 1;
                showResult.success = true;
                this.state = CellState.Showing;
            }
            return showResult;
        }
    }
}
