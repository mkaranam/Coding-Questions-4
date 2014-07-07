using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitAlgorithms
{
    class ProblemSet1
    {
        public int findNonRepeatedNumber(int[] input)
        {
            if (input == null || input.Length == 0) throw new System.ArgumentNullException();
            if (input.Length < 4) throw new System.ArgumentException();

            int ones = 0;
            int twos = 0;
            int not_thres = 0;
            for (int i = 0; i<input.Length;i++) 
            {
                twos |= ones & input[i];
                ones ^= input[i];
                not_thres = ~(ones & twos);
                ones &= not_thres;
                twos &= not_thres;
            }
            return ones;
        }

        public int findNonRepeatNumber(int[] input,int k)
        {
            int sum = 0;
            int result = 0;
            //Loop over all bits
            for(int i=0;i<32;i++)
            {
                sum=0;
                int mask  = 1 << i;
                for(int j=0;j<input.Length;j++)
                {
                    if ((mask & input[j])!=0) sum++;
                }
                if (sum % k != 0) result |= (mask);
            }
            return result;
        }

        public bool OppositeSigns(int a, int b)
        {
            return ((a ^ b) < 0);
        }

        public int swapBits(int input,int p1,int p2,int n)
        {
            int length = Convert.ToString(input,2).Length;
            
            //Get right bits
            int mask = (~0 << (p2+n)) | ((1 << p2)-1);
            int rNum = input & mask;
            
            //Get left bits
            mask = (~0 << (p1 + n)) | ((1 << p1) - 1);
            int lNum = input & mask;
            //clear right and left bits for swapping
            mask = (~0 << (p2 + n)) & ((1 << p2) - 1);
            input = input & mask;
            //mask = (~0 << (p1 + n)) & ((1 << p1)-1);
            input = input & mask;

            Console.WriteLine("Cleared Input: {0}", Convert.ToString(mask, 2));


            input = input | rNum | lNum;
            return input;
        }
    
        public int Add(int x, int y)
        {
            while(y!=0)
            {
                int carry = x & y;
                x = x ^ y;
                y = carry << 1;
            }
            return x;
        }

        public void ChangeToZero(int[] a)
        {
            a[a[1]] = a[~a[1]];
        }

        public int nextHighestNumber(int a)
        {
            int onepos=0;

            //Get the least significant 1 bit
            while ((a & (1 << onepos)) == 0)
                onepos++;

            //Get the least significant 0 bit after onepos
            int zeropos = onepos+1;
            while ((a & (1 << zeropos)) != 0)
                zeropos++;

            //Set the zeropos to 1
            a = a | (1 << zeropos);
            
            //remove one set bit as we set the zero bit
            onepos--;   
            
            //Set the onepos number of zeroes and then add zeropos-onepos number of 1's
            a = a & (~0 << zeropos);
            a = a | ((1 << onepos) - 1);
            
            return a;
        }
    
        public int AddOne(int n)
        {
            Console.WriteLine(Convert.ToString((n), 2));
            int zeroPos = 0;
            while ((n & (1 << zeroPos)) != 0)
                zeroPos++;
            n = n | (1 << zeroPos);
            n = n & ~((1 << zeroPos)-1);
            Console.WriteLine(Convert.ToString(n, 2));
            return n;
        }
    
        public int Multiplyby3Point5(int n)
        {
            return ((n << 1) + n + (n >> 1));
        }
 
        public void turnOffRightMostBit(int n)
        {
            Console.WriteLine(Convert.ToString(n, 2));
            Console.WriteLine(Convert.ToString(n & (n-1),2));
        }
    
        public void IsaPowerOf4(int n)
        {
            int zeros = 0;
            if((n & n-1)!=0)
            {
                Console.WriteLine("Not a power of 4");
                return;
            }
            while ((n & (1 << zeros)) == 0)
                zeros++;
            if (zeros % 2 == 0)
                Console.WriteLine("Is a power of 4");
            else
                Console.WriteLine("Not a power of 4");
        }
    
        public void Abs(int n)
        {
            int mask = (n >> 32);
            n = (n + mask) ^ mask;
        }

        public int ModulusPower2(int n, int d)
        {
            return (n & (d - 1));
        }
 
        public void CircularShiftRight(int n, int k)
        {
            Console.WriteLine(Convert.ToString(n, 2));

            int rMask = (1 << k) - 1;
            int rNum = n & rMask;   //We now have right bits that need to be added at the beginning
            rNum = rNum << (32-k);
            n = n >> k;
            n = n | rNum;
            Console.WriteLine(Convert.ToString(n,2));

        }

        public void CircularShiftLeft(int n, int k)
        {
            Console.WriteLine(Convert.ToString(n, 2));

            int rMask = (1 << (32-k)) - 1;
            int rNum = n & rMask;   //We now have right bits that need to be added at the beginning
            rNum = rNum << k;
            n = n << k;
            n = n | rNum;
            Console.WriteLine(Convert.ToString(n, 2));

        }
    
        public void TwoNonRepeatedNumbers(int[] input)
        {
            int xor = input[0];
            for (int i = 1; i < input.Length; i++)
                xor = xor ^ input[i];

            int setBit = xor & ~(xor - 1);
            int x, y;
            x = -1;
            y = -1;
            for(int i=0;i<input.Length;i++)
            {
                if ((input[i] & setBit) != 0)
                {
                    if (x == -1) x = input[i];
                    else x = x ^ input[i];
                }
                else
                {
                    if (y == -1) y = input[i];
                    y = y ^ input[i];
                }

            }
        
        }
    
        public int reverseBits(int n)
        {
            int reverse,count;
            count = 32;
            reverse=0;
            while(n!=0)
            {
                reverse = reverse << 1;
                reverse |= n & 1;
                n >>= 1;
                count--;
            }
            reverse <<= count;
            return reverse;
        }
    
        public int CountSetBits(int n)
        {
            int count=0;
            while(n!=0)
            {
                if ((n & 1) != 0) count++;
                n >>= 1;
            }
            return count;
        }
    
        public int NextPowerOf2(int n)
        {
            int onepos = 0;
            while(n!=0)
            {
                n >>= 1;
                onepos++;
            }
            return 1 << (onepos);
        }

        public bool IsaMultipleOf3(int n)
        {
            if (n == 0) return true;
            if (n == 1) return false;
            int odd, even;
            odd = even = 0;
            while(n!=0)
            {
                if ((n & 1) != 0)
                    odd++;
                n >>= 1;

                if ((n & 1) != 0)
                    even++;
                n >>= 1;
            }
            return IsaMultipleOf3(Math.Abs(odd - even));
        }
    
        public void findParity(int n)
        {
            int count = 0;
            while(n!=0)
            {
                count = count + (n & 1);
                n >>= 1;
            }
            if(count%2!=0) Console.WriteLine("Odd Parity");
            else Console.WriteLine("Even Parity");
        }
    
        public bool isAPowerof2(int n)
        {
            if (n == 0) return false;
            else return ((n & (n - 1)) != 0);
        }
    
        public int Multiplywith7(int n)
        {
            return (n + (n << 1) + (n << 2));
        }
    
        public int numberToConvert(int a,int b)
        {
            return CountSetBits(a ^ b);
        }

        //The number 0xAAAAAAAA is a 32 bit number with all even bits set as 1 and all odd bits as 0.
        //The number 0x55555555 is a 32 bit number with all odd bits set as 1 and all even bits as 0.
        public uint swapOddEven(uint n)
        {
            uint even = n & 0xAAAAAAAA;
            uint odd = n & 0x55555555;
            even >>= 1;
            odd <<= 1;
            return even | odd;
        }
    
        public void swap(ref int a,ref int b)
        {
            a = a ^ b;
            b = a ^ b;
            a = a ^ b;
        }
    }
}
