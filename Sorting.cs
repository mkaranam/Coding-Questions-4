using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchingAndSorting
{
    class Sorting
    {
        #region Selection Sort
        public void SelectionSort(int[] input)
        {
            if (input == null || input.Length==0) throw new System.ArgumentNullException();

            for(int i=0;i<input.Length;i++)
            {
                int minIndex = getMin(input, i);
                swap(input, i, minIndex);
            }

        }
        
        private int getMin(int[] input,int start)
        {
            int minIndex = start;
            for(int i=start+1;i<input.Length;i++)
            {
                if (input[i] < input[minIndex]) minIndex = i;
            }
            return minIndex;
        }
        #endregion

        #region Bubble Sort
        public void BubbleSort(int[] input)
        {
            if (input == null || input.Length == 0) throw new System.ArgumentNullException();
            for(int i=0;i<input.Length-1;i++)
            {
                for(int j=i+1;j<input.Length;j++)
                {
                    if(input[j] < input[i])
                    {
                        swap(input, i, j);
                    }
                }
            }
        }
        #endregion

        #region Insertion Sort
        public void InsertionSort(int[] input)
        {
            if (input == null || input.Length == 0) throw new System.ArgumentNullException();
            for(int i=0;i<input.Length;i++)
            {
                int j=i-1;
                while (j >= 0)
                {
                    if (input[j] < input[i]) j--;
                    else
                    {
                        swap(input, i, j);
                        break;
                    }
                }
            }
        }
        #endregion

        #region Merge Sort
        public void MergeSort(int[] input)
        {
            if(input==null || input.Length==0) throw new System.ArgumentNullException();
            input = MergeSortRecur(input);
        }

        private int[] MergeSortRecur(int[] input)
        {
            if(input.Length==0) return null;
            if (input.Length == 1) return input;

            int[] a = new int[input.Length/2];
            int[] b = new int[input.Length - a.Length];
            Array.ConstrainedCopy(input, 0, a, 0, a.Length);
            Array.ConstrainedCopy(input, a.Length - 1, b, 0, b.Length);
            a = MergeSortRecur(a);
            b = MergeSortRecur(b);
            input = Merge(a, b);
            return input;
        }

        private int[] Merge(int[] a, int[] b)
        {
            if (a == null || b == null) return null;
            if (a.Length==0) return b;
            if (b.Length==0) return a;

            int c1, c2, c3;
            c1 = c2 = c3 = 0;
            int[] output = new int[a.Length + b.Length];
            while(c1<a.Length && c2 < b.Length)
            {
                if (a[c1] <= b[c2]) output[c3++] = a[c1++];
                else output[c3++] = b[c2++];
            }
            while (c1 < a.Length) output[c3++] = a[c1++];
            while (c2 < b.Length) output[c3++] = b[c2++];
            return output;
         }
        #endregion

        #region Heap Sort
        public void HeapSort(int[] input)
        {
            if (input == null || input.Length == 0) throw new System.ArgumentNullException();
            //Build Heap
            for (int i = 0; i < input.Length / 2; i++) Heapify(input, i,input.Length);

            //Sort by swapping with the last element
            for(int i=input.Length-1;i>0;i--)
            {
                swap(input, i, 0);
                Heapify(input, 0, input.Length);
            }
        }

        private void Heapify(int[] input, int start, int Length)
        {
            if (start >= Length) return;

            int left = start * 2 + 1;
            int right = start * 2 + 2;
            while (left < Length || right < Length)
            {
                int minIndex = start;
                if (left < Length) minIndex = Math.Max(minIndex, left);
                if (right < Length) minIndex = Math.Max(minIndex, right);
                swap(input, start, minIndex);
                left = minIndex * 2 + 1;
                right = minIndex * 2 + 2;
                start = minIndex;
            }
        }
        #endregion

        #region QuickSort
        public void QuickSort(int[] input)
        {
            if (input == null || input.Length == 0) throw new System.ArgumentNullException();
            QuickSort(input, 0, input.Length);
        }

        private void QuickSort(int[] input, int low, int high)
        {
            int pivot = partition(input, low, high);
            QuickSort(input, 0, pivot - 1);
            QuickSort(input, pivot + 1, high);
        }

        private int partition(int[] input,int low, int high)
        {
            int pivot = high;
            int firstHigh = low;
            for(int i=low;i<high;i++)
            {
                if(input[i] < input[pivot])
                {
                    swap(input, i, firstHigh);
                    firstHigh++;
                }
            }
            swap(input, pivot, firstHigh);
            return pivot;
        }
        
        public void IterativeQuickSort(int[] input)
        {
            Stack<int> s= new Stack<int>();
            s.Push(0);
            s.Push(input.Length-1);
            while(s.Count>0)
            {
                int high = s.Pop();
                int low = s.Pop();
                int pivot = partition(input, low, high);
                if(pivot-1 > low)
                {
                    s.Push(low);
                    s.Push(pivot - 1);
                }

                if(pivot+1<high)
                {
                    s.Push(pivot + 1);
                    s.Push(high);
                }
            }
        }
        #endregion

        #region Other sorting problems
         /*
         * Sorting a nearly sorted array. Given a integer "k" which says that any given element is only within "k" spaces in a sorted array
         */
        public void NearlySorted(int[] input, int k)
        {
            k = Math.Min(input.Length, k);
            int[] heap = new int[k];
            for (int i = 0; i < k; i++) heap[i] = input[i];
            for (int i = 0; i < k / 2; i++) Heapify(heap,i,k);
            int[] result = new int[input.Length];
            int c=0;
            for (int i = k; i < input.Length;i++ )
            {
                result[c++] = heap[0];
                heap[0] = input[i];
                Heapify(heap, 0,k);
            }
            while(c<result.Length)
            {
                result[c++] = heap[0];
                heap[0] = heap[--k];
                Heapify(heap, 0, k);
            }
            input = result;
        }
        
        

        #endregion

        #region Utility functions
        private void swap(int[] input, int i, int j)
        {
            int temp = input[i];
            input[i] = input[j];
            input[j] = temp;
        }
        #endregion
    }
}
