using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_structures.src.minesweeper
{
    public class Board
    {
        Cell[] cells;
        int rows;
        int columns;
        int numberOfBombCells;
        int numberOfRemainingNormalCells;

        public Board(int numberOfBombs, int rows, int columns)
        {
            int numberOfCells = rows * columns;
            this.rows = rows;
            this.columns = columns;
            this.cells = new Cell[numberOfCells];
            this.numberOfBombCells = numberOfBombs;
            this.numberOfRemainingNormalCells = numberOfCells = numberOfBombs;
        }

        public void SetCell(int row, int column, Cell cell)
        {
            int cellIndex = this.GetCellIndex(row, column);
            this.cells[cellIndex] = cell;
        }

        public int GetRows()
        {
            return this.rows;
        }

        public int GetColumns()
        {
            return this.columns;
        }

        public bool IsCellShowing(int row, int column) 
        {
            int cellIndex = this.GetCellIndex(row, column);
            return this.cells[cellIndex] != null && this.cells[cellIndex].IsShowing();
        }

        public bool IsCellFlagged(int row, int column)
        {
            int cellIndex = this.GetCellIndex(row, column);
            return this.cells[cellIndex] != null && this.cells[cellIndex].IsFlagged();
        }

        public bool IsCellBomb(int row, int column)
        {
            int cellIndex = this.GetCellIndex(row, column);
            return this.cells[cellIndex] != null && this.cells[cellIndex].IsBomb();
        }

        public int GetNeighboringBombCount(int row, int column)
        {
            List<BoardCoordinate> neighborCoordinates = new List<BoardCoordinate>();
            this.GetCellNeighborCoordinates(row, column, neighborCoordinates);
            int bombCount = 0;
            foreach (BoardCoordinate coordinate in neighborCoordinates)
            {
                int cellIndex = this.GetCellIndex(coordinate.row, coordinate.column);
                if (this.cells[cellIndex] != null && this.cells[cellIndex].IsBomb())
                {
                    bombCount++;
                }
            }
            return bombCount;
        }

        public void GetCellNeighborCoordinates(int row, int column, IList<BoardCoordinate> coordinates)
        {
            coordinates.Clear();
            if (row > 0)
            {
                if (column > 0)
                {
                    coordinates.Add(new BoardCoordinate(row - 1, column - 1));
                }
                coordinates.Add(new BoardCoordinate(row - 1, column));
                if (column < this.columns - 1)
                {
                    coordinates.Add(new BoardCoordinate(row - 1, column + 1));
                }
            }
            if (column > 0)
            {
                coordinates.Add(new BoardCoordinate(row, column - 1));
            }
            if (column < this.columns - 1)
            {
                coordinates.Add(new BoardCoordinate(row, column + 1));
            }
            if (row < this.rows - 1)
            {
                if (column > 0)
                {
                    coordinates.Add(new BoardCoordinate(row + 1, column - 1));
                }
                coordinates.Add(new BoardCoordinate(row + 1, column));
                if (column < this.columns - 1)
                {
                    coordinates.Add(new BoardCoordinate(row + 1, column + 1));
                }
            }
        }

        // NOTE: should we allow an override for a flagged cell?  mainly for auto-showing by blank cells...
        // NOTE: nope! auto-showing should not override a blank cell!
        public ShowResult ShowCell(int row, int column)
        {
            int cellIndex = this.GetCellIndex(row, column);
            ShowResult showResult = this.cells[cellIndex].Show();
            return showResult;
        }

        public void FlagCell(int row, int column)
        {
            int cellIndex = this.GetCellIndex(row, column);
            this.cells[cellIndex].Flag();
        }

        public void UnflagCell(int row, int column)
        {
            int cellIndex = this.GetCellIndex(row, column);
            this.cells[cellIndex].Unflag();
        }

        public void DisplayBoard(IList<String> cellRows)
        {
            StringBuilder stringBuilder = new StringBuilder();
            cellRows.Clear();
            for (int row = 0; row < this.rows; ++row)
            {
                stringBuilder.Length = 0;

                for(int column = 0; column < this.columns; ++column)
                {
                    int cellIndex = this.GetCellIndex(row, column);
                    char cellChar = '?';
                    Cell cell = this.cells[cellIndex];
                    if (cell.IsFlagged()) cellChar = 'F';
                    if (cell.IsShowing()) cellChar = cell.GetDisplayChar();
                    stringBuilder.Append(cellChar);
                }

                String cellRow = stringBuilder.ToString();
                cellRows.Add(cellRow);
            }
        }

        private int GetCellIndex(int row, int column)
        {
            return (row * this.columns) + column;
        }
    }
}
