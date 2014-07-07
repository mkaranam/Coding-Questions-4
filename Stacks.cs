using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Stacks
{
    class ProblemSet
    {
        public void infixToPostFix(String input)
        {
            Console.WriteLine("Pre-fix: {0}", input);
            if (input.Length < 2)
                Console.WriteLine("Post-fix: {0}", input);
            char[] exp = input.ToCharArray();
            Stack<char> s = new Stack<char>();
            StringBuilder str = new StringBuilder();
            for(int i=0;i<exp.Length;i++)
            {
                if (exp[i] == '(')
                    s.Push(exp[i]);
                else if (exp[i] == ')')
                {
                    while (s.Peek() != '(')
                        str.Append(s.Pop());
                    str.Append(s.Pop());
                }
                else if (exp[i] == '*' || exp[i] == '/' || exp[i] == '+' || exp[i] == '-')
                {
                    if (s.Count == 0)
                        s.Push(exp[i]);
                    else if (precedence(exp[i], s.Peek()))
                        s.Push(exp[i]);
                    else
                    {
                        while (!precedence(exp[i], s.Peek()))
                            str.Append(s.Pop());
                        s.Push(exp[i]);
                    }
                }
                else str.Append(exp[i]);
            }
            while (s.Count > 0) str.Append(s.Pop());
            Console.WriteLine("Post-Fix: {0}", str.ToString());
        
        }

        private bool precedence(char a, char b)
        {
            int aop = operatorValue(a);
            int bop = operatorValue(b);
            if (aop >= bop) return true;
            else return false;
        }

        private int operatorValue(char a)
        {
            switch(a)
            {
                case '*':
                    return 9;
                case '/':
                    return 9;
                case '%':
                    return 9;
                case '+':
                    return 8;
                case '-':
                    return 8;
                case '&':
                    return 7;
                case '^':
                    return 6;
                case '|':
                    return 5;
                default: 
                    return 0;
            }
        }

        public void ReverseString(String input)
        {
            Console.WriteLine("Input: {0}", input);
            char[] c = input.ToCharArray();
            Stack<char> s = new Stack<char>();
            for (int i = 0; i < c.Length; i++) s.Push(c[i]);
            int index = 0;
            while (s.Count > 0) c[index++] = s.Pop();
            Console.WriteLine("Output: {0}", new String(c));
        }
    
        public bool IsBalancedParanthesis(String input)
        {
            bool isValid = true;
            char[] c = input.ToCharArray();
            Stack<char> s = new Stack<char>();
            for(int i=0;i<c.Length;i++)
            {
                if (c[i] == '(' || c[i]=='{' || c[i]=='[') s.Push(c[i]);
                else if (c[i] == ')' || c[i]=='}' || c[i]==']') 
                {
                    if(s.Count==0)
                    {
                        isValid = false;
                        break;
                    }
                    if (c[i] == ')' && s.Peek()!='(')
                    {
                        isValid = false;
                        break;
                    }
                    if (c[i] == '}' && s.Peek() != '{')
                    {
                        isValid = false;
                        break;
                    }
                    if (c[i] == ']' && s.Peek() != '[')
                    {
                        isValid = false;
                        break;
                    }
                    s.Pop();
                }
            }
            if (s.Count > 0) isValid = false;
            return isValid;
        }
    
        public void NextGreaterNumber(int[] input)
        {
            //NEED TO USE TWO LOOPS...this is wrong
            Stack<int> s = new Stack<int>();
            for(int i=1;i<input.Length;i++)
            {
                if (input[i] >= input[i - 1])
                    s.Push(input[i]);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(" -1");
            for(int i=0;i<input.Length-1;i++)
            {
                if (input[i] == s.Peek()) sb.Append(" " + s.Pop());
                else sb.Append(" " + s.Peek());
            }

            Console.WriteLine(sb.ToString());
        }
 
        public void ReverseStack(int[] input)
        {
            Stack<int> s = new Stack<int>();
            for (int i = 0; i < input.Length; i++) s.Push(input[i]);
            

            while (s.Count > 0)
                Console.Write(s.Pop() + " ");
            
        }

        private void Reverse(Stack<int> s)
        {
            if(s.Count>0)
            {
                int temp = s.Pop();
                Reverse(s);
                InsertAtBottom(s, temp);
            }
        }

        private void InsertAtBottom(Stack<int> s,int item)
        {
            if (s.Count == 0)
                s.Push(item);
            else
            {
                int temp = s.Pop();
                InsertAtBottom(s, item);
                s.Push(temp);
            }
        }
    
        public void StockPlan(int[] input)
        {
            Stack<int> s = new Stack<int>();
            int[] sp = new int[input.Length];
            s.Push(0);
            sp[0] = 1;
            for(int i=1;i<input.Length;i++)
            {
                while (s.Count > 0 && input[i] >= input[s.Peek()])
                    s.Pop();
                if (s.Count == 0) sp[i] = (i + 1);
                else sp[i] = i - s.Peek();
                s.Push(i);
            }
            for (int i = 0; i < input.Length; i++)
                Console.Write(sp[i] + " ");

        }
    
    }


}
