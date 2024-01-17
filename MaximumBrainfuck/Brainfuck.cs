using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MaximumBrainfuck
{
    class Brainfuck : IBrainfuck
    {
                //Tape where the main values are stored
        public static int[] tape = new int[1000];

        //Stack to keep track of were to retun to when a mthod is finished
        public static Stack<int> returnStack = new Stack<int>();

        public static Stack<int> loopReturnStack = new Stack<int>();

        //List with all the positions in code where methodds start
        public static List<int> methodList = new List<int>();

        public static Dictionary<string, int> methodNames = new Dictionary<string, int>();

        //Variable where all boolean logic is saved to
        public static bool condition = true;

        //Singular cahced value that can be used in code, might change this to a stack
        public static int cache = 0;

        //Current Position in the tape
        public static int tapePointer = 0;

        //Current Position in the code
        public static int codePointer = 0;

        //Will add functionality to only use standard Brainfuck in the future
        //static ExecutionType executionType ;


       void IBrainfuck.execute(string input)
       {
            char[] code = input.ToCharArray();

            while (codePointer < code.Length)
            {
                brainfuck(code);
                codePointer++;
            }
       }


        
        public void brainfuck(char[] code)
        {
                switch (code[codePointer])
                {
                    //Set pointer to next cell
                    case '>': tapePointer++; break;
                    //Set pointer to previous cell
                    case '<': tapePointer--; break;
                    //Increment current cell
                    case '+': tape[tapePointer]++; break;
                    //Decrement current cell
                    case '-': tape[tapePointer]--; break;
                    //Print current cell value as character
                    case '.': Console.Write((char)tape[tapePointer]); break;
                    //Read single character from console into curret cell
                    case ',': tape[tapePointer] = Console.ReadKey().KeyChar; break;
                    //Start while loop, repeats if current cell is not 0
                    case '[':
                        if (tape[tapePointer] == 0)
                        {
                            int skip = 0;
                            int ptr = codePointer + 1;
                            while (code[ptr] != 93 || skip > 0)
                            {
                                if (code[ptr] == 91) { skip++; }
                                else if (code[ptr] == 93) { skip--; }
                                ptr++;
                                codePointer = ptr;
                            }
                        }
                        break;
                    //End while loop, got back to corresponding [
                    case ']':
                        if (tape[tapePointer] != 0)
                        {
                            int skip = 0;
                            int ptr = codePointer - 1;
                            while (code[ptr] != 91 || skip > 0)
                            {
                                if (code[ptr] == 93) { skip++; }
                                else
                                if (code[ptr] == 91) { skip--; }
                                ptr--;
                                codePointer = ptr;
                            }
                        }
                        break;
                }

            }

        
    }

}
/*
        Better loop, doesnt work yet

                    case '[':
                        if (tape[tapePointer] != 0)
                        {
                            loopReturnStack.Append(codePointer);
                        }
                        else
                        {
                            loopReturnStack.Pop();
                            while (!code[codePointer].Equals(']'))
                            {
                                codePointer++;
                            }
                        }
                        break;
                    //End while loop, got back to corresponding [
                    case ']':
                        codePointer=loopReturnStack.First();
                        break;
*/