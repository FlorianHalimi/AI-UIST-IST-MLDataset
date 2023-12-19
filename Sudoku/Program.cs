using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class Program
    {
        static void Main(string[] args)
        {
            SudokuSolverB_DFS sudokuSolverB_DFS = new SudokuSolverB_DFS();

            SudokuDifficulty level = SudokuDifficulty.Medium;
            SudokuGenerator sudoku = new SudokuGenerator(level);
            int[,] board = sudoku.GenerateSudoku();

            Console.WriteLine("Test board:");
            static void PrintGrid(int[,] board)
            {
                for (int row = 0; row < board.GetLength(0); row++)
                {
                    for (int col = 0; col < board.GetLength(1); col++)
                    {
                        Console.Write(board[row, col] + " ");
                    }
                    Console.WriteLine();
                }
            }

            PrintGrid(board);

            Console.WriteLine("The solution using B_DFS is:");
            sudokuSolverB_DFS.Solve(board);            
            sudokuSolverB_DFS.Print(board);

        }
    }
}
