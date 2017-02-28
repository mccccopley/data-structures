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
                List<BoardCoordinate> neighborCoordinates = new List<BoardCoordinate>();
                this.board.GetCellNeighborCoordinates(this.row, this.column, neighborCoordinates);
                foreach (BoardCoordinate coordinate in neighborCoordinates)
                {
                    ShowResult neighborShowResult = this.board.ShowCell(coordinate.row, coordinate.column);
                    showResult.numberNormalCellsRevealed += neighborShowResult.numberNormalCellsRevealed;
                }
                showResult.success = true;
                this.state = CellState.Showing;
            }
            return showResult;
        }
    }
}
