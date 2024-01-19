using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class SudokuSolverB_DFS
    {
        const int N = 9;

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

        static bool IsValidInSubgrid(int[,] grid, int row, int col, int num)
        {
            int startRow = row - row % 3;
            int startCol = col - col % 3;
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
            return IsValidInRow(grid, row, num) && IsValidInCol(grid, col, num) && IsValidInSubgrid(grid, row, col, num);
        }

        static bool FindEmptyPosition(int[,] grid, out int row, out int col)
        {
            for (row = 0; row < N; row++)
            {
                for (col = 0; col < N; col++)
                {
                    if (grid[row, col] == 0)
                    {
                        return true;
                    }
                }
            }
            row = -1;
            col = -1;
            return false;
        }

        public bool Solve(int[,] grid)
        {
            int row, col;

            if (!FindEmptyPosition(grid, out row, out col))
            {
                return true;
            }
            for (int num = 1; num <= 9; num++)
            {
                if (IsValid(grid, row, col, num))
                {
                    grid[row, col] = num;
                    if (Solve(grid))
                    {
                        return true;
                    }
                    grid[row, col] = 0;
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

