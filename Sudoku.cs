using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTracking
{
    class Sudoku
    {
        private Cell[,] grid;
        private bool solved;
        private int numFree=64;
        private SortedDictionary<int, List<Cell>> queue;

        public void createSudoku()
        {
            grid = new Cell[9, 9];
            queue = new SortedDictionary<int, List<Cell>>();
            List<Cell> init = new List<Cell>();
            for(int i=0;i<9;i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    grid[i, j] = new Cell(i, j);
                    init.Add(grid[i, j]);
                }
            }
            queue.Add(grid[0, 0].possibleValues, init);
            solved = false;
            

            sample();
            Console.WriteLine("Sample sudoku: \n");
            printSudoku();
        }

        public void solveSudoku()
        {
            Console.WriteLine("Solved sudoku: \n");
            solve();   
        }

        private void solve()
        {
            if (solved) return;
            if(queue.Count==0)
            {
                printSudoku();
                solved = true;
                return;
            }

            for (int k = 0; k < queue.Count; k++)
            {
                if (solved) return;
                List<Cell> moves = queue.ElementAt(k).Value ;
                for (int j = 0; j < moves.Count; j++)
                {
                    Cell c = moves[j];
                    for (int i = 0; i < 9; i++)
                    {
                        if (c.Values[i])
                        {
                            makeMove(c.row, c.col, i + 1);
                            solve();
                            undoMove(c.row, c.col);
                        }
                    }
                }
            }


        }

        private void printSudoku()
        {
            for(int i=0;i<9;i++)
            {
                Console.Write("\n");
                for (int j = 0; j < 9; j++)
                    Console.Write(grid[i, j].value + " ");
            }
        }

        private void makeMove(int row, int col,int value)
        {
            Console.WriteLine("Make move: " + row + " " + " " + col + " "+value +" " + numFree );
            grid[row, col].isFilled = true;
            grid[row, col].value = value;
            queue[grid[row, col].possibleValues].Remove(grid[row, col]);
            List<Cell> cells = getAffectedCells(row, col);
            foreach (Cell c in cells)
            {
                if(c.Values[value-1])
                {
                    queue[c.possibleValues].Remove(c);
                    if (queue[c.possibleValues].Count == 0) queue.Remove(c.possibleValues);
                    c.Values[value - 1] = false;
                    c.possibleValues--;
                    if (queue.ContainsKey(c.possibleValues)) queue[c.possibleValues].Add(c);
                    else
                    {
                        List<Cell> l = new List<Cell>();
                        l.Add(c);
                        queue.Add(c.possibleValues, l);
                    }
                }
            }
            numFree--;
        }

        private void undoMove(int row, int col)
        {
            Console.WriteLine("Undo move: " + row + " " + " " + col);
            grid[row, col].isFilled = false;
            int value = grid[row, col].value;
            if (queue.ContainsKey(grid[row, col].possibleValues)) queue[grid[row, col].possibleValues].Add(grid[row, col]);
            else
            {
                List<Cell> l = new List<Cell>();
                l.Add(grid[row, col]);
                queue.Add(grid[row, col].possibleValues, l);
            }
            List<Cell> cells = getAffectedCells(row, col);
            foreach (Cell c in cells)
            {
                if (!c.Values[value - 1])
                {
                    queue[c.possibleValues].Remove(c);
                    if (queue[c.possibleValues].Count == 0) queue.Remove(c.possibleValues);
                    c.Values[value - 1] = true;
                    c.possibleValues++;
                    if (queue.ContainsKey(c.possibleValues)) queue[c.possibleValues].Add(c);
                    else
                    {
                        List<Cell> l = new List<Cell>();
                        l.Add(c);
                        queue.Add(c.possibleValues, l);
                    }
                }
            }
            numFree++;
        }

        private List<Cell> getAffectedCells(int row, int col)
        {
            List<Cell> cells = new List<Cell>();
            for(int i=0;i<grid.GetLength(0);i++)
            {
                if (!grid[row, i].isFilled && !cells.Contains(grid[row, i])) cells.Add(grid[row, i]);
                if (!grid[i, col].isFilled && !cells.Contains(grid[i, col])) cells.Add(grid[i, col]);
            }
            List<Cell> matrix = getSubMatrix(grid[row, col].matrixNo);
            foreach(Cell c in matrix)
                if (!c.isFilled && !cells.Contains(c)) cells.Add(c);
            return cells;
        }

        private List<Cell> getSubMatrix(int Num)
        {
            List<Cell> cells = new List<Cell>();
            for(int i=0;i<9;i++)
            {
                for(int j=0;j<9;j++)
                {
                    if (grid[i, j].matrixNo == Num) cells.Add(grid[i, j]);
                }
            }
            return cells;
        }
    
        private void sample()
        {
            makeMove(0,2,7);
            makeMove(0,6,5);
            makeMove(1,0,4);
            makeMove(1,3,9);
            makeMove(1,5,6);
            makeMove(1,8,1);
            makeMove(2,0,9);
            makeMove(2,3,8);
            makeMove(2,5,1);
            makeMove(2,8,7);
            makeMove(3,2,6);
            makeMove(3,3,2);
            makeMove(3,5,5);
            makeMove(3,6,9);
            makeMove(4,1,9);
            makeMove(4,2,5);
            makeMove(4,6,8);
            makeMove(4,7,7);
            makeMove(5,2,2);
            makeMove(5,3,7);
            makeMove(5,5,8);
            makeMove(5,6,3);
            makeMove(6,0,2);
            makeMove(6,3,1);
            makeMove(6,5,7);
            makeMove(6,8,8);
            makeMove(7,0,8);
            makeMove(7,3,5);
            makeMove(7,5,3);
            makeMove(7,8,6);
            makeMove(8,2,1);
            makeMove(8,6,7);
            


        }
    }

    class Cell
    {
        public int row { get; private set; }
        public int col { get; private set; }
        public bool[] Values;
        public int possibleValues { get; set; }
        public bool isFilled { get;  set; }
        public int value { get;  set; }
        public int matrixNo { get; set; }

        public Cell(int row, int col)
        {
            this.row = row;
            this.col =col;
            Values = new bool[9];
            for (int i = 0; i < 9; i++)
                Values[i] = true;
            possibleValues = 9;
            if(row < 3)
            {
                if (col < 3) matrixNo = 1;
                else if (col < 6) matrixNo = 2;
                else matrixNo = 3;
            }
            else if(row<6)
            {
                if (col < 3) matrixNo = 4;
                else if (col < 6) matrixNo = 5;
                else matrixNo = 6;
            }
            else
            {
                if (col < 3) matrixNo = 7;
                else if (col < 6) matrixNo = 8;
                else matrixNo = 9;
            }
        }
    }
}
