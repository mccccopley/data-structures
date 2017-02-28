using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_structures.src.minesweeper
{
    class BombCell : Cell
    {
        public BombCell(Board board, int row, int column)
            : base(board, row, column)
        {
        }

        public override char GetDisplayChar()
        {
            return '*';
        }

        public override bool IsBomb()
        {
            return true;
        }

        public override ShowResult Show()
        {
            ShowResult showResult = new ShowResult();
            if (this.state == CellState.Hidden)
            {
                showResult.success = true;
                this.state = CellState.Showing;
            }
            return showResult;
        }
    }
}
