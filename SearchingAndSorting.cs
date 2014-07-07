using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeeksForGeeks;

namespace SearchingAndSorting
{
    class Problems
    {
         /*
         * Find the minimum length unsorted array, sorting which will make the whole array sorted
         */
        public void MinUnsortedArray(int[] input)
        {
            if (input == null || input.Length == 0) throw new System.ArgumentNullException();

            int start = 0;
            int end = input.Length-1;

            //Find start point
            for (start = 0; start < input.Length-1; start++)
            {
                if (input[start] < input[start+1])break;
            }
            if(start == input.Length-1) {
                Console.WriteLine("Whole array already sorted...");
                return;
            }
            //find end point
            for (end = input.Length-1; end >0; end--)
            {
                if (input[end] < input[end-1]) break;
            }

            //Find subarray max
            int min, max;
            min = input[start];
            max = input[start];
            for (int i = start + 1; i <= end; i++)
            {
                min = Math.Min(input[i], min);
                max = Math.Max(input[i], max);
            }

            for (int i = 0; i < start; i++)
            {
                if (input[i] > min)
                {
                    start = i;
                    break;
                }
            }

            for (int i = input.Length - 1; i > end; i--)
            {
                if(input[i] < max)
                {
                    end = i;
                    break;
                }
            }

            Console.WriteLine("Minimum length unsorted subarray: {0} to {1}", start, end);
        }
    
        public void BinarySearch(int[] input, int key)
        {
            int temp = BS(input,0,input.Length-1,key);
            if (temp == -1) Console.WriteLine("Number not found...");
            else Console.WriteLine("Number found at: {0}", temp);
        }

        private int BS(int[] input, int low, int high, int key)
        {
            if (low > high) return -1;
            if (low == high)
            {
                if(key == input[low]) return low;
                else return -1;
            }
            int mid = (low + high) / 2;
            if (key == input[mid]) return mid;
            int ret = BS(input, low, mid - 1, key);
            if(ret==-1) ret = BS(input, mid + 1, high, key);
            return ret;
        }
     
        
    }
}
