using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NQueens
{
    class NQueens
    {
        static void Main()
        {
            int n = 8;
            int[] solution = SolveNQueens(n);

            if (solution != null)
            {
                Console.WriteLine("Solution found:");
                PrintBoard(solution);
            }
            else
            {
                Console.WriteLine("No solution found.");
            }
        }

        static int[] SolveNQueens(int n)
        {
            PriorityQueue<Node> priorityQueue = new PriorityQueue<Node>();
            Node initialNode = new Node(new int[n], 0, 0);

            priorityQueue.Enqueue(initialNode, initialNode.Cost);

            while (priorityQueue.Count > 0)
            {
                Node currentNode = priorityQueue.Dequeue();

                if (currentNode.Row == n)
                {
                    return currentNode.Board;
                }

                for (int i = 0; i < n; i++)
                {
                    int[] newBoard = currentNode.Board.ToArray();
                    newBoard[currentNode.Row] = i;

                    if (!HasConflicts(newBoard, currentNode.Row))
                    {
                        Node newNode = new Node(newBoard, currentNode.Row + 1, currentNode.Row + 1 + Heuristic(newBoard));
                        priorityQueue.Enqueue(newNode, newNode.Cost);
                    }
                }
            }

            return null;
        }

        static bool HasConflicts(int[] board, int row)
        {
            for (int i = 0; i < row; i++)
            {
                if (board[i] == board[row] || Math.Abs(board[i] - board[row]) == Math.Abs(i - row))
                {
                    return true; // Conflicts found
                }
            }
            return false; // No conflicts
        }

        static int Heuristic(int[] board)
        {
            int conflicts = 0;
            int n = board.Length;

            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (board[i] == board[j] || Math.Abs(board[i] - board[j]) == Math.Abs(i - j))
                    {
                        conflicts++;
                    }
                }
            }

            return conflicts;
        }

        static void PrintBoard(int[] board)
        {
            int n = board.Length;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (board[i] == j)
                    {
                        Console.Write("Q ");
                    }
                    else
                    {
                        Console.Write(". ");
                    }
                }
                Console.WriteLine();
            }
        }
    }

    class Node
    {
        public int[] Board { get; }
        public int Row { get; }
        public int Cost { get; }

        public Node(int[] board, int row, int gCost)
        {
            Board = board;
            Row = row;
            Cost = gCost; // A* cost function
        }
    }

    class PriorityQueue<T>
    {
        private List<T> elements = new List<T>();
        private List<int> priorities = new List<int>();

        public int Count => elements.Count;

        public void Enqueue(T element, int priority)
        {
            int index = 0;
            while (index < priorities.Count && priority > priorities[index])
            {
                index++;
            }

            elements.Insert(index, element);
            priorities.Insert(index, priority);
        }

        public T Dequeue()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("Priority queue is empty.");
            }

            T element = elements[0];
            elements.RemoveAt(0);
            priorities.RemoveAt(0);

            return element;
        }
    }
}
