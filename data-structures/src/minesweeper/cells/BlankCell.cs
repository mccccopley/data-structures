using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_structures.src.minesweeper
{
    public class BlankCell : Cell
    {
        public BlankCell(Board board, int row, int column) 
            : base(board, row, column)
        {
        }

        public override char GetDisplayChar()
        {
            return '_';
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
                this.state = CellState.Showing;
                List<BoardCoordinate> neighborCoordinates = new List<BoardCoordinate>();
                this.board.GetCellNeighborCoordinates(this.row, this.column, neighborCoordinates);
                foreach (BoardCoordinate coordinate in neighborCoordinates)
                {
                    if (!this.board.IsCellShowing(coordinate.row, coordinate.column) && !this.board.IsCellFlagged(coordinate.row, coordinate.column))
                    {
                        ShowResult neighborShowResult = this.board.ShowCell(coordinate.row, coordinate.column);
                        showResult.numberNormalCellsRevealed += neighborShowResult.numberNormalCellsRevealed;
                    }
                }
                showResult.success = true;
            }
            return showResult;
        }
    }
}
