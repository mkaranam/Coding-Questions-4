using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynammicProgramming
{
    class ProblemSet2
    {
        public void LongestIncreasingSubsequence(int[] input)
        {
            int[] LIS = new int[input.Length];
            int[] parent = new int[input.Length];
            int lisLength = 0;
            for(int i=0;i<input.Length;i++) parent[i] =-1;

            LIS[0] = 0;
            for(int i=1;i<input.Length;i++)
            {
                //if input[i] > last element of LIS, just add it
                if(input[LIS[lisLength]] < input[i])
                {
                    lisLength++;        //Update LIS length
                    LIS[lisLength] = i; //Add to LIS
                    parent[i] = LIS[lisLength - 1]; //Set the last previous element as parent
                }
                else
                {
                    //We need to find the next element >= input[i] and replace it with input[i]
                    //Truly a binary search, but here lets do a linear search
                    int index=0;
                    for(index=0;index<=lisLength;index++)
                    {
                        if (input[LIS[index]] >= input[i])
                            break;
                    }
                    LIS[index] = i; //Replace LIS with input[i]
                    if(index > 0) parent[i] = LIS[index - 1]; //Update parent
                }
            }
            Console.WriteLine("LIS is of length {0}: ", lisLength + 1);
            int temp = LIS[lisLength];
            Console.Write(input[temp]);
            while(parent[temp] !=-1)
            {
                Console.Write(input[parent[temp]]);
                temp = parent[temp];
            }
        }

        public void LongestCommonSubsequence(String s1,String s2)
        {
            if(s1 ==null || s2==null || s1.Length==0 || s2.Length==0)
            {
                Console.WriteLine("No subsequence");
                return;
            }
            char[] a = s1.ToCharArray();
            char[] b = s2.ToCharArray();
            int[,] table = new int[a.Length + 1, b.Length + 1];

            for(int i=0;i<table.GetLength(0);i++)
            {
                for(int j=0;j<table.GetLength(1);j++)
                {
                    if (i == 0 || j == 0)
                        table[i, j] = 0;
                    else if(a[i-1]==b[j-1])
                    {
                        table[i, j] = 1 + table[i - 1, j - 1];
                    }
                    else
                    {
                        table[i, j] = Math.Max(table[i - 1, j], table[i, j - 1]);
                    }
                }
            }

            if (table[a.Length, b.Length] == 0)
            {
                Console.WriteLine("No subsequence");
                return;
            }
            Console.WriteLine("LCS length: {0}", table[a.Length, b.Length]);
            Stack<char> lcs = new Stack<char>();
            int row = a.Length;
            int col = b.Length;
            while(row >0 && col >0)
            {
                if(a[row-1]==b[col-1])
                {
                    lcs.Push(a[row - 1]);
                    row--;
                    col--;
                }
                else
                {
                    if (table[row - 1, col] > table[row, col - 1])
                        row--;
                    else
                        col--;
                }
            }
            Console.WriteLine("LCS: ");
            while (lcs.Count > 0)
                Console.Write(lcs.Pop() + " ");
            
        }
    
        public void EditDistance(String s1,String s2)
        {
            if (s1 == null || s2 == null || s1.Length == 0 || s2.Length == 0)
            {
                Console.WriteLine("Cannot convert");
                return;
            }
            char[] a = s1.ToCharArray();
            char[] b = s2.ToCharArray();
            int[,] table = new int[a.Length + 1, b.Length + 1];

            for(int i=0;i<table.GetLength(0);i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    if (i == 0 || j == 0)
                    {
                        table[i, j] = i + j;
                    }
                    else
                    {
                        if (a[i - 1] == b[j - 1])
                        {
                            table[i, j] = table[i - 1, j - 1];  //MATCH
                        }
                        else
                        {
                            int insert = 1 + table[i - 1, j];   //INSERT
                            int delete = 1 + table[i, j - 1];   //DELETE
                            int replace = 1 + table[i - 1, j - 1];  //SUBSTITUTION
                            table[i, j] = Math.Min(replace, Math.Min(insert, delete));
                        }
                    }
                }
            }

            Console.WriteLine("Edit Distance Min Cost: {0}", table[a.Length, b.Length]);
            Stack<char> lcs = new Stack<char>();
            int row = a.Length;
            int col = b.Length;
            while (row > 0 && col > 0)
            {
                if (a[row - 1] == b[col - 1])
                {
                    lcs.Push('M');
                    row--;
                    col--;
                }
                else
                {
                    int insert = table[row - 1, col];   //INSERT
                    int delete = table[row, col - 1];   //DELETE
                    int replace = table[row - 1, col - 1];  //SUBSTITUTION
                    if(insert <= delete && insert <= replace )
                    {
                        lcs.Push('I');
                        row--;
                    }
                    else if(delete <= replace)
                    {
                        lcs.Push('D');
                        col--;
                    }
                    else
                    {
                        lcs.Push('S');
                        row--;
                        col--;
                    }

                }
            }
            Console.WriteLine("Edit Distance: ");
            while (lcs.Count > 0)
                Console.Write(lcs.Pop() + " ");

        }
    
        public void LongestPallindromicSubsequence(int[] input)
        {
            int[,] table = new int[input.Length,input.Length];

            for (int i = 0; i < input.Length; i++)
                table[i, i] = 1;

            for (int cl = 2; cl <= input.Length; cl++)
            {
                for (int i = 0; i < input.Length - cl + 1; i++)
                {
                    int j = i + cl - 1;
                    if (input[i] == input[j])
                    {
                        if (Math.Abs(j - i) == 1)
                            table[i, j] = 2;
                        else table[i, j] = 2 + table[i + 1, j - 1];
                    }
                    else table[i, j] = Math.Max(table[i + 1, j], table[i, j - 1]);
                }
            }

            Console.WriteLine("Longest pallindrome: {0}", table[0,input.Length - 1]);
        }
    
        public void MaxSumSubArray(int[] input)
        {
            int[] sumArray = new int[input.Length];
            int currentMax = 0;

            if(input[0]>0)
            {
                currentMax = input[0];
                sumArray[0] = input[0];
            }

            for(int i=1;i<input.Length;i++)
            {
                int sum = input[i] + sumArray[i - 1];
                if (sum > 0) sumArray[i] = sum;
                if (sum > currentMax) currentMax += sum;
            }
            int endindex=0;
            for(endindex=0;endindex<sumArray.Length;endindex++)
            {
                if (sumArray[endindex] == currentMax)
                    break;
            }
            int startIndex = endindex-1;
            while(startIndex>=0)
            {
                if (sumArray[startIndex] == 0)
                    break;
                startIndex--;
            }
            if (startIndex < 0) startIndex = 0;
            Console.WriteLine("Maximum Sum SubArray with sum {0}", currentMax);
            for (int i = startIndex; i <= endindex; i++)
                Console.Write(input[i] +" ");
            
        }

        public void MaxSumIncreasingSubsequence(int[] input)
        {
            int[] maxSumArray = new int[input.Length];
            int currentMax = Int32.MinValue;
            int[] children = new int[input.Length];
            int globalMax = Int32.MinValue;
            for (int i = 0; i < input.Length; i++) children[i] = -1;


            for (int i = 0; i < input.Length; i++)
            {
                currentMax = Int32.MinValue;
                for (int j = 0; j < i; j++)
                {
                    if (input[i] > input[j])
                    {
                        int sum = maxSumArray[j] + input[i];
                        if (sum > currentMax)
                        {
                            children[i] = j;
                            currentMax = sum;
                        }
                    }
                }
                currentMax = Math.Max(currentMax, input[i]);
                maxSumArray[i] = currentMax;
                globalMax = Math.Max(currentMax, globalMax);
            }
            
            int start = 0;
            for(start=0;start<input.Length;start++)
            {
                if (maxSumArray[start] == globalMax)
                    break;
            }

            Console.WriteLine("Maximum Sum: {0}", globalMax);
            while(start!=-1)
            {
                Console.Write(input[start] + " ");
                start = children[start];
            }
        }

        public void LongestBiotonicSubsequence(int[] input)
        {
            int[] maxSeq = new int[input.Length];
            int currentMax = 0;
            int[] children = new int[input.Length];
            int globalMax = 0;
            for (int i = 0; i < input.Length; i++) children[i] = -1;


            for (int i = 0; i < input.Length; i++)
            {
                currentMax = 0;
                for (int j = 0; j < i; j++)
                {
                    if (input[i] > input[j])
                    {
                        //It can only support a inscreasing sequence at this point
                        int prev = children[j];
                        if(prev==-1 || input[prev] < input[j])
                        {
                            currentMax = Math.Max(currentMax, maxSeq[j] + 1);
                            children[i] = j;
                        }
                    }
                    else
                    {
                        if(input[i] != input[j])
                        {
                            currentMax = Math.Max(currentMax, maxSeq[j] + 1);
                            children[i] = j;
                        }
                    }
                }
                currentMax = Math.Max(currentMax, 1);
                maxSeq[i] = currentMax;
                globalMax = Math.Max(currentMax, globalMax);
            }

            int start = 0;
            for (start = 0; start < input.Length; start++)
            {
                if (maxSeq[start] == globalMax)
                    break;
            }

            Console.WriteLine("Longest Biotonic Sequence: {0}", globalMax);
            while (start != -1)
            {
                Console.Write(input[start] + " ");
                start = children[start];
            }
        }
    
        public void CuttingRod(int[] input)
        {
            int[] maxPrice = new int[input.Length+1];
            int currentMax = 0;
            int[] Pieces = new int[input.Length];
            int globalMax = 0;
            for (int i = 0; i < input.Length; i++) Pieces[i] = -1;


            for (int i = 0; i < input.Length; i++)
            {
                currentMax = 0;
                for (int j = 0; j < i; j++)
                {
                    int sum = maxPrice[j+1] + maxPrice[i - j];
                    if (sum > currentMax)
                    {
                        currentMax = sum;
                        Pieces[i] = j;
                    }
                }
                if(currentMax<=input[i])
                {
                    Pieces[i] = -1; //Reset links if this good enough
                    currentMax = input[i];
                }
                maxPrice[i+1] = currentMax;
                globalMax = Math.Max(currentMax, globalMax);
            }

            
            Console.WriteLine("Maximum Price: {0}", globalMax);
            
            if(Pieces[7]==-1)
            {
                Console.WriteLine("Made of Piece: {0}", 8);
            }
            else
            {
                
                Console.Write("{0} length rod made of Pieces: ", 8);
                int total = 8;
                int cur = 7;
                while (total > 0 && cur!=-1)
                {
                    int next =  Pieces[cur];
                    if (next == -1) next = cur;
                    next += 1;
                    Console.Write((next) + " ");
                    total = total - next;
                    cur = total-1;
                }
            }
            
        }
    
        public void FindTuples(int[] input, int k)
        {
            int[] counts = new int[input.Length];
            int totalCount = 0;
            counts[0] = 0;
            counts[1] = 0;
            
            for(int i=2;i<input.Length;i++)
            {
                if (input[i] > k) break;
                int count = 0;
                if(input[i] + input[i-1] <= k)
                {
                    count = counts[i - 1] + (i + 1);
                }
                else
                {
                    for(int j=i-1;j>=2;j++)
                    {
                        if (input[i] + input[j] <= k)
                        {
                            count = counts[j] + (i + 1);
                        }
                    }
                }

            }
        }

    }
}
