using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTracking
{
    class ProblemSet1
    {

        public void printPermutations(String s)
        {
            if(s==null) throw new System.ArgumentNullException();
            if(s.Length<2) Console.WriteLine(s);
            
            char[] c = s.ToCharArray();
            bool[] inArray = new bool[c.Length];
            char[] permut = new char[c.Length];
            printPermutations(c, 0, permut, inArray);

        }

        private void printPermutations(char[] c,int k,char[] permut,bool[] inArray)
        {
            if(k==c.Length)
            {
                Console.WriteLine(new String(permut));
                return;
            }
            for(int i=0;i<inArray.Length;i++)
            {
                if(!inArray[i])
                {
                    permut[k++] = c[i];
                    inArray[i] = true;
                    printPermutations(c, k, permut, inArray);
                    k--;
                    inArray[i] = false;
                }
            }
        }
        
        public void KnightsTour()
        {
            bool[,] visited = new bool[8, 8];
            Stack<string> path = new Stack<string>();
            bool done = false;
            KnightsTour(visited, path, 0, 4,ref done);
            if (!done) Console.WriteLine("No path");
        }

        private void KnightsTour(bool[,] visited,Stack<string> path,int row,int col,ref bool done)
        {
            Console.WriteLine(path.Count);
            if(path.Count == 64)
            {
                while (path.Count > 0)
                {
                    Console.WriteLine(path.Pop());
                }
                done = true;
                return;
            }
            List<int[]> moves = getPossibleMoves(visited, row, col);
            foreach(int[] move in moves)
            {
                path.Push(move[0] + " " + move[1]);
                visited[move[0], move[1]] = true;
                KnightsTour(visited, path, move[0], move[1],ref done);
                if (done) return;
                path.Pop();
                visited[move[0], move[1]] = false;
            }
        }

        private List<int[]> getPossibleMoves(bool[,] visited, int row, int col)
        {
            List<int[]> moves = new List<int[]>();
            int[] rows = new int[] { 2,1,-1,-2,-2,-1,1,2};
            int[] cols = new int[] { 1,2,2,1,-1,-2,-2,-1};

            for (int i = 0; i < 8;i++ )
            {
                int temp = row + rows[i];
                int temp2 = col + cols[i];
                if(temp>=0 && temp < 8 && temp2>=0 & temp2 < 8)
                {
                    if (!visited[temp, temp2]) moves.Add(new int[] { temp, temp2 });
                }
            }
            return moves;
        }

        public void RatInMaze()
        {
            bool[,] maze = new bool[4, 4];
            Queue<string> path = new Queue<string>();
            maze[0, 0] = true;
            maze[1, 0] = true;
            maze[1, 1] = true;
            maze[1, 3] = true;
            maze[2, 1] = true;
            maze[3, 0] = true;
            maze[3, 1] = true;
            maze[3, 2] = true;
            maze[3, 3] = true;
            bool done = false;
            RatInMaze(maze, 0, 0, path, 3, 3, ref done);
            if (!done) Console.WriteLine("No path");
        }

        private void RatInMaze(bool[,] maze,int row, int col, Queue<string> path,int dR,int dC,ref bool done)
        {
            if(row==dR && col==dC)
            {
                done=true;
                while (path.Count > 0)
                    Console.WriteLine(path.Dequeue());
                return;
            }
            List<int[]> moves = getRatMoves(maze, row, col);
            foreach(int[] move in moves)
            {
                path.Enqueue(move[0] + " " + move[1]);
                RatInMaze(maze, move[0], move[1], path, dR, dC, ref done);
                if (done) return;
            }
        }

        private List<int[]> getRatMoves(bool[,] maze,int row, int col)
        {
            int[] forward = new int[] { row, col + 1 };
            int[] down = new int[] { row + 1, col };
            List<int[]> moves = new List<int[]>();
            if (isValidMove(maze, forward)) moves.Add(forward);
            if (isValidMove(maze, down)) moves.Add(down);
            return moves;
        }

        private bool isValidMove(bool[,] maze,int[] move)
        {
            if (move[0] < 0 || move[0] >= maze.GetLength(0) || move[1] < 0 || move[1] >= maze.GetLength(1))
                return false;
            return maze[move[0], move[1]];
        }

        public void Sudoku()
        {
            int[,] grid = new int[,]{{3, 0, 6, 5, 0, 8, 4, 0, 0},
                      {5, 2, 0, 0, 0, 0, 0, 0, 0},
                      {0, 8, 7, 0, 0, 0, 0, 3, 1},
                      {0, 0, 3, 0, 1, 0, 0, 8, 0},
                      {9, 0, 0, 8, 6, 3, 0, 0, 5},
                      {0, 5, 0, 0, 9, 0, 6, 0, 0},
                      {1, 3, 0, 0, 0, 0, 2, 5, 0},
                      {0, 0, 0, 0, 0, 0, 0, 7, 4},
                      {0, 0, 5, 2, 0, 6, 3, 0, 0}};
            Console.WriteLine("Solved Puzzle: \n");
            printSudoku(grid);
            bool done =false;
            Console.WriteLine("Solved Puzzle: \n");
            Sudoku(grid,32,ref done);
        }

        private void Sudoku(int[,] grid,int numFree,ref bool done)
        {
            if(numFree==0)
            {
                printSudoku(grid);
                done = true;
                return;
            }
            List<int[]> moves = getPossibleValues(grid);
            foreach(int[] move in moves)
            {
                for(int i=1;i<10;i++)
                {
                    grid[move[0], move[1]] = i;
                    numFree--;
                    Sudoku(grid, numFree, ref done);
                    if (done) return;
                    numFree++;
                    grid[move[0], move[1]] = 0;
                }
            }
        }

        private void printSudoku(int[,] grid)
        {
            for (int i = 0; i < 9; i++)
            {
                Console.Write("\n");
                for (int j = 0; j < 9; j++)
                    Console.Write(grid[i, j] + " ");
            }
        }
        
        private List<int[]> getPossibleValues(int[,] grid)
        {
            List<int[]> moves = new List<int[]>();
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if (grid[i, j] == 0) moves.Add(new int[] { i, j });
            return moves;
        }
        
    }
}
