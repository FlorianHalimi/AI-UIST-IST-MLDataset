using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class SudokuSolverBFS
    {

        const int N = 9;

        struct GridInfo
        {
            public int[,] grid;
            public int row, col;
        }

        static bool IsValidInRow(int[,] grid, int row, int num)
        {
            for (int col = 0; col < N; col++)
            {
                if (grid[row, col] == num)
                {
                    return false;
                }
            }
            return true;
        }

        static bool IsValidInCol(int[,] grid, int col, int num)
        {
            for (int row = 0; row < N; row++)
            {
                if (grid[row, col] == num)
                {
                    return false;
                }
            }
            return true;
        }

        static bool IsValidInSubgrid(int[,] grid, int startRow, int startCol, int num)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (grid[startRow + i, startCol + j] == num)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        static bool IsValid(int[,] grid, int row, int col, int num)
        {
            int startRow = row - row % 3;
            int startCol = col - col % 3;
            return IsValidInRow(grid, row, num) &&
                   IsValidInCol(grid, col, num) &&
                   IsValidInSubgrid(grid, startRow, startCol, num);
        }

        static GridInfo FindEmptyPosition(int[,] grid)
        {
            for (int row = 0; row < N; row++)
            {
                for (int col = 0; col < N; col++)
                {
                    if (grid[row, col] == 0)
                    {
                        GridInfo info;
                        info.grid = grid;
                        info.row = row;
                        info.col = col;
                        return info;
                    }
                }
            }
            GridInfo invalid;
            invalid.row = invalid.col = -1;
            invalid.grid = null;
            return invalid;
        }

        public bool SolveUsingBFS(int[,] grid)
        {
            Queue<GridInfo> queue = new Queue<GridInfo>();
            queue.Enqueue(new GridInfo { grid = grid });

            while (queue.Count > 0)
            {
                GridInfo current = queue.Dequeue();
                GridInfo emptyPos = FindEmptyPosition(current.grid);

                if (emptyPos.row == -1 && emptyPos.col == -1)
                {
                    Array.Copy(current.grid, grid, current.grid.Length);
                    return true;
                }

                for (int num = 1; num <= 9; num++)
                {
                    if (IsValid(current.grid, emptyPos.row, emptyPos.col, num))
                    {
                        int[,] newGrid = (int[,])current.grid.Clone();
                        newGrid[emptyPos.row, emptyPos.col] = num;
                        queue.Enqueue(new GridInfo { grid = newGrid });
                    }
                }
            }
            return false;
        }

        public void Print(int[,] grid)
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Console.Write(grid[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

    }
}

