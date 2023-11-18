using System;
using System.Collections.Generic;

namespace MaximumBrainfuck
{
    class Program
    {
        //Tape where the main values are stored
        static int[] tape = new int[1000];

        //Stack to keep track of were to retun to when a mthod is finished
        static Stack<int> returnStack = new Stack<int>();

        //List with all the positions in code where methodds start
        static List<int> methodList = new List<int>();

        //Variable where all boolean logic is saved to
        static bool condition = true;

        //Singular cahced value that can be used in code, might change this to a stack
        static int cache = 0;

        //Current Position in the tape
        static int tapePointer = 0;

        //Current Position in the code
        static int codePointer = 0;

        //Will add functionality to only use standard Brainfuck in the future
        static bool doMaximum = true;

        static void Main(string[] args)
        {
            if (args.Length > 0)
                brainfuck(args[0]);
        }

        static void brainfuck(string codeString)
        {
            char[] code = codeString.ToCharArray();
            while (codePointer < code.Length)
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
                    default:
                    //If the Interpreter is in maximum brainfuck mode, consider the other characters as well
                    if(doMaximum)
                    {
                        maximumBrainfuck(code);
                    }
                    break;

                }
                codePointer++;
            }

        }

        static void maximumBrainfuck(char[] code)
        {
            switch (code[codePointer])
            {
                //Set current cell value to 0
                case '_':
                    tape[tapePointer] = 0;
                    break;
                //Set current cell to the value of the cell whose index ist the current cells value
                case '=':
                    tape[tapePointer] = tape[tape[tapePointer]];
                    break;
                //Printactual number of current cell value
                case ':':
                    Console.Write(tape[tapePointer]);
                    break;
                //Go To current cells value
                case '@':
                    tapePointer = tape[tapePointer];
                    break;
                //Start Method
                case '(':
                    methodList.Add(codePointer);
                    while (codePointer < code.Length && code[codePointer] != 41)
                    {
                        codePointer++;
                    }
                    break;
                //End off Method
                case ')':
                    codePointer = returnStack.Pop();
                    break;
                //Call Method
                case '#':
                    returnStack.Push(codePointer);
                    codePointer = methodList[tape[tapePointer]];
                    break;
                //Save current cell value to cache
                case '?':
                    cache = tape[tapePointer];
                    break;
                //Write cached value into current cell
                case '!':
                    tape[tapePointer] = cache;
                    break;
                //Square current cell
                case '^':
                    tape[tapePointer] *= tape[tapePointer];
                    break;
                //Multiply current cell with cached value
                case '*':
                    tape[tapePointer] *= cache;
                    break;
                //Divide current cell by cached value
                case '/':
                    tape[tapePointer] /= cache;
                    break;
                //Add cached value to current cell
                case '&':
                    tape[tapePointer] += cache;
                    break;
                //Subtract cached value from current cell
                case '|':
                    tape[tapePointer] -= cache;
                    break;
                //Call first Metod
                case '§':
                    returnStack.Push(codePointer);
                    codePointer = methodList[0];
                    break;
                //Input entire string
                case ';':
                    string inputString = Console.ReadLine();
                    char[] inputChars = inputString.ToCharArray();
                    for (int i = 0; i < inputChars.Length; i++)
                    {
                        tape[tapePointer + i] = inputChars[i];
                    }
                    break;
                //Reset Tape
                case '~':
                    tapePointer = 0;
                    tape = new int[1000];
                    break;
                //Put current index into current cell
                case '°':
                    tape[tapePointer] = tapePointer;
                    break;
                //Start if statement
                case '{':
                    if (!condition)
                    {
                        while (codePointer < code.Length && code[codePointer] != 125)
                        {
                            codePointer++;
                        }
                    }
                    break;
                //Set condition to reslt of cell > cache
                case '€':
                    condition = tape[tapePointer] > cache;
                    break;
                    //Set condition to reslt of cell < cache
                case '$':
                    condition = tape[tapePointer] < cache;
                    break;
                //Set condition to reslt of cell = cache
                case 'µ':
                    condition = tape[tapePointer] == cache;
                    break;
                //Invert condition
                case '\\':
                    condition = !condition;
                    break;
                //Set condition true if chache divides cell value without remainder, else true
                case '%':
                    condition = tape[tapePointer] % cache == 0;
                    break;
                //Marks string to be inserted into tape
                case '"':
                    codePointer++;
                    int j = 0;
                    while (codePointer < code.Length && code[codePointer] != '"')
                    {
                        tape[tapePointer + j] = code[codePointer];
                        j++;
                        codePointer++;
                    }
                    break;
            }
        }
    }
}
