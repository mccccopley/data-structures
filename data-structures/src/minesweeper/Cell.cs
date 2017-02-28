using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_structures.src.minesweeper
{
    public abstract class Cell
    {
        protected Board board;
        protected int row;
        protected int column;
        protected CellState state;

        protected Cell(Board board, int row, int column)
        {
            this.board = board;
            this.row = row;
            this.column = column;
            this.state = CellState.Hidden;
        }

        public abstract bool IsBomb();
        public abstract ShowResult Show();
        public abstract char GetDisplayChar();

        public void Flag()
        {
            if (this.state == CellState.Hidden)
            {
                this.state = CellState.Flagged;
            }
        }

        public void Unflag()
        {
            if (this.state == CellState.Flagged)
            {
                this.state = CellState.Hidden;
            }
        }

        public bool IsFlagged()
        {
            return this.state == CellState.Flagged;
        }

        public bool IsShowing()
        {
            return this.state == CellState.Showing;
        }
    }
}
