using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_structures.src.minesweeper
{
    public class BoardGenerator
    {
        private Random random;

        public BoardGenerator(int seed)
        {
            this.random = new Random(seed);
        }

        public Board Generate(int numberOfBombs, int rows, int columns)
        {
            rows = rows < 1 ? 1 : rows;
            columns = columns < 1 ? 1 : columns;
            numberOfBombs = numberOfBombs < 0 ? 0 : numberOfBombs;

            int numberOfCells = rows * columns;

            numberOfBombs = numberOfBombs > numberOfCells ?
                numberOfCells :
                numberOfBombs;

            List<BoardCoordinate> possibleCoordinatesForBombs = new List<BoardCoordinate>();
            for (int row = 0; row < rows; ++row)
            {
                for (int column = 0; column < columns; ++column)
                {
                    BoardCoordinate coordinate = new BoardCoordinate(row, column);
                    possibleCoordinatesForBombs.Add(coordinate);
                }
            }

            Board board = new Board(numberOfBombs, rows, columns);

            int bombCellsToCreate = numberOfBombs;
            while (bombCellsToCreate > 0)
            {
                int randomCoordinateIndex = this.random.Next(possibleCoordinatesForBombs.Count);
                BoardCoordinate coordinate = possibleCoordinatesForBombs[randomCoordinateIndex];
                possibleCoordinatesForBombs.RemoveAt(randomCoordinateIndex);
                BombCell bombCell = new BombCell(board, coordinate.row, coordinate.column);
                board.SetCell(coordinate.row, coordinate.column, bombCell);
                --bombCellsToCreate;
            }

            int normalCellsToCreate = numberOfCells - numberOfBombs;
            while (normalCellsToCreate > 0)
            {
                for (int row = 0; row < rows; ++row)
                {
                    for (int column = 0; column < columns; ++column)
                    {
                        if (!board.IsCellBomb(row, column))
                        {
                            Cell normalCell = null;
                            int neighboringBombCount = board.GetNeighboringBombCount(row, column);
                            if (0 < neighboringBombCount)
                            {
                                normalCell = new NumberCell(board, row, column, neighboringBombCount);
                            }
                            else 
                            {
                                normalCell = new BlankCell(board, row, column);
                            }
                            board.SetCell(row, column, normalCell);
                            --normalCellsToCreate;
                        }
                    }
                }
            }

            return board;
        }
    }
}
