using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DivideAndConquer
{
    class ProblemSet
    {
        public int aPowb(int a, int b)
        {
            if (a < 2 || b == 1) return a;
            if (b == 0) return 1;
            int ret = 1;
            if (b % 2 != 0) ret = a;
            int half = aPowb(a, b / 2);
            return half * half * ret;
        }

        public void Median(int[] a, int[] b)
        {
            Console.WriteLine("Median: {0}", Median(a, 0, a.Length-1, b, 0, b.Length-1));
        }

        private int Median(int[] a, int al, int ah, int[] b, int bl, int bh)
        {
            int length = ah - al+1;
            if (length <= 0) return -1;
            if(length==1)
            {
                return ((a[al] + b[bl]) / 2);
            }
            if (length == 2)
            {
                return (Math.Max(a[al], b[bl]) + Math.Min(a[ah], b[bh])) / 2;
            }
            int m1 = getMedian(a,al,ah);
            int m2 = getMedian(b,bl,bh);
            if (m1 == m2) return m1;
            if (m2 > m1)
            {
                if (length % 2 == 0)
                    return Median(a, (al+ah) / 2, ah, b, bl, (bl+bh) / 2 - 1);
                else
                    return Median(a, (al + ah) / 2, ah, b, bl, (bl + bh) / 2);
            }
            else
            {
                if (length % 2 == 0)
                    return Median(a, al, (al + ah) / 2 - 1, b, (bl + bh) / 2, bh);
                else
                    return Median(a, al, (al + ah) / 2, b, (bl + bh) / 2, bh);
            }
        }

        private int getMedian(int[] input,int low,int high)
        {
            int length = (high-low)+1;
            if (length % 2 == 0)
                return ((input[(low + high) / 2] + input[(low + high) / 2+1])/2);
            else return input[(low + high) / 2];
        }
       
    }
}
