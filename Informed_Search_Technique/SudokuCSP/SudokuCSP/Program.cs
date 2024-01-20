using System;

namespace SudokuCSP
{
    class Program
    {
        const int N = 9;

        const int empty = 0;

        static bool IsValidInRow(int[,] grid, int row, int value)
        {
            for (int col = 0; col < N; col++)
            {
                if (grid[row, col] == value)
                {
                    return false;
                }
            }
            return true;
        }

        static bool IsValidInColumn(int[,] grid, int col, int value)
        {
            for (int row = 0; row < N; row++)
            {
                if (grid[row, col] == value)
                {
                    return false;
                }
            }
            return true;
        }

        static bool IsValidInSubgrid(int[,] grid, int row, int col, int value)
        {
            int startRow = row - row % 3;
            int startCol = col - col % 3;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (grid[startRow + i, startCol + j] == value)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        static bool IsValid(int[,] grid, int row, int col, int value)
        {
            return IsValidInRow(grid, row, value) && IsValidInColumn(grid, col, value) && IsValidInSubgrid(grid, row, col, value);
        }

        static void PrintGrid(int[,] grid)
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

        static bool FindUnassignedCell(int[,] grid, out int row, out int col)
        {
            row = -1;
            col = -1;

            int maxDegree = -1;

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (grid[i, j] == empty)
                    {
                        int degree = 0;
                        for (int k = 0; k < N; k++)
                        {
                            if (grid[i, k] != empty)
                            {
                                degree++;
                            }
                            if (grid[k, j] != empty)
                            {
                                degree++;
                            }
                        }
                        int startRow = i - i % 3;
                        int startCol = j - j % 3;
                        for (int k = 0; k < 3; k++)
                        {
                            for (int l = 0; l < 3; l++)
                            {
                                if (grid[startRow + k, startCol + l] != empty)
                                {
                                    degree++;
                                }
                            }
                        }

                        if (degree > maxDegree)
                        {
                            row = i;
                            col = j;
                            maxDegree = degree;
                        }
                    }
                }
            }

            return row != -1 && col != -1;
        }

        static bool SolveSudoku(int[,] grid)
        {
            int row, col;
            if (!FindUnassignedCell(grid, out row, out col))
            {
                return true;
            }

            for (int value = 1; value <= 9; value++)
            {
                if (IsValid(grid, row, col, value))
                {
                    grid[row, col] = value;

                    if (SolveSudoku(grid))
                    {
                        return true;
                    }

                    grid[row, col] = empty;
                }
            }

            return false;
        }

        static int[,] grid = new int[,]
        {
            { 5, 3, 0, 0, 7, 0, 0, 0, 0 },
            { 6, 0, 0, 1, 9, 5, 0, 0, 0 },
            { 0, 9, 8, 0, 0, 0, 0, 6, 0 },
            { 8, 0, 0, 0, 6, 0, 0, 0, 3 },
            { 4, 0, 0, 8, 0, 3, 0, 0, 1 },
            { 7, 0, 0, 0, 2, 0, 0, 0, 6 },
            { 0, 6, 0, 0, 0, 0, 2, 8, 0 },
            { 0, 0, 0, 4, 1, 9, 0, 0, 5 },
            { 0, 0, 0, 0, 8, 0, 0, 7, 9 }
        };

        static void Main(string[] args)
        {
            Console.WriteLine("Initial grid:");
            PrintGrid(grid);

            // degree heuristic
            if (SolveSudoku(grid))
            {
                Console.WriteLine("Solved grid:");
                PrintGrid(grid);
            }
            else
            {
                Console.WriteLine("This sudoku is unsolvable.");
            }
        }
    }
}
