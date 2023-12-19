using System;
using System.Linq;

namespace Sudoku
{
    public enum SudokuDifficulty
    {
        Easy,
        Medium,
        Hard
    }

    public class SudokuGenerator
    {
        private const int GridSize = 9;
        private int[,] board;
        private int emptyCells;

        public SudokuGenerator(SudokuDifficulty difficulty)
        {
            board = new int[GridSize, GridSize];

            switch (difficulty)
            {
                case SudokuDifficulty.Easy:
                    emptyCells = 40;
                    break;
                case SudokuDifficulty.Medium:
                    emptyCells = 55;
                    break;
                case SudokuDifficulty.Hard:
                    emptyCells = 65;
                    break;
                default:
                    emptyCells = 40;
                    break;
            }
        }

        public void PrintBoard()
        {
            for (int row = 0; row < GridSize; row++)
            {
                for (int col = 0; col < GridSize; col++)
                {
                    Console.Write(board[row, col] + " ");
                }
                Console.WriteLine();
            }
        }

        private bool IsValid(int row, int col, int num)
        {
            for (int i = 0; i < GridSize; i++)
            {
                if (board[row, i] == num || board[i, col] == num || board[3 * (row / 3) + i / 3, 3 * (col / 3) + i % 3] == num)
                {
                    return false;
                }
            }
            return true;
        }

        public int[,] GenerateSudoku()
        {
            int[] distributedValues = Shuffle(Enumerable.Range(1, GridSize).ToArray());
            for (int i = 0; i < GridSize; i++)
            {
                board[0, i] = distributedValues[i];
            }

            FillSudoku(1, 0);
            RemoveNumbers();
            return board;
        }

        private void ShuffleArray(int[] array)
        {
            Random rand = new Random();
            int n = array.Length;
            for (int i = 0; i < n; i++)
            {
                int r = i + rand.Next(n - i);
                int temp = array[r];
                array[r] = array[i];
                array[i] = temp;
            }
        }

        private int[] Shuffle(int[] array)
        {
            int[] newArray = array.ToArray();
            ShuffleArray(newArray);
            return newArray;
        }

        private bool FillSudoku(int row, int col)
        {
            if (row == GridSize - 1 && col == GridSize)
            {
                return true;
            }

            if (col == GridSize)
            {
                row++;
                col = 0;
            }

            if (board[row, col] != 0)
            {
                return FillSudoku(row, col + 1);
            }

            for (int num = 1; num <= GridSize; num++)
            {
                if (IsValid(row, col, num))
                {
                    board[row, col] = num;

                    if (FillSudoku(row, col + 1))
                    {
                        return true;
                    }

                    board[row, col] = 0;
                }
            }
            return false;
        }

        private void RemoveNumbers()
        {
            var rand = new Random();
            int count = emptyCells;

            while (count > 0)
            {
                int row = rand.Next(0, GridSize);
                int col = rand.Next(0, GridSize);

                if (board[row, col] != 0)
                {
                    board[row, col] = 0;
                    count--;
                }
            }
        }
    }
}