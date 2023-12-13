using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

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

        static Dictionary<string, int> methodNames = new Dictionary<string,int>();

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
            try
            {   
                string input = args[0];
                string[] endings = {".mbf", ".bf"};
                if(File.Exists(input) && endings.Any(i=>input.EndsWith(i))){
                    input=File.ReadAllText(input);
                }
                
                brainfuck(input);
            }
            catch(Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();
            }
                
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
                    startOfMethod(code);
                    break;
                //End off Method
                case ')':
                    codePointer = returnStack.Pop();
                    break;
                //Call Method
                case '#':
                    callMethod(tape[tapePointer],"No Method with index '"+tape[tapePointer]+"' defined!");
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
                    callMethod(0,"No default Method defined!");
                    break;
                //Input entire string
                case ';':
                    stringInput();
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
                    ifClause(code);
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
                    stringLiteral(code);
                    break;
                case '´':
                    string callMethodByName="";
                    codePointer++;
                    while (codePointer < code.Length && code[codePointer] != '´')
                    {
                        callMethodByName+= code[codePointer];
                        codePointer++;
                    }
                    if(!callMethodByName.Equals(""))
                    {
                        if(!methodNames.ContainsKey(callMethodByName))
                        {
                            throw new MethodUndefinedException("No Method with Name: '"+callMethodByName+"' defined!");
                        }
                        callMethod(methodNames[callMethodByName],"I don't even know...");
                    }
                    break;
                //Idea: Functions that set the current cell to the value calculated on another simulated tape
                //Would work with stack-based cache
                //'⍝(+++^+)§:' prints 10
                //With not yet implemented stack based cache:
                //  '⍝(!>+++^+?<*)⍝()+?++?++^?§:_++#§:_++#§:_++#' prints 10
            }
        }

        //Outsourced parts of the switch statement


        static void startOfMethod(char[] code){
            //check wether method has name given
            int i = 1;
            string methodName = "";
            while(code[codePointer+i]>96 &&code[codePointer+i]<123) 
            {
                methodName+=code[codePointer+i];
                i++;
            }
            

            methodList.Add(codePointer);
            if(!methodName.Equals("")){methodNames.Add(methodName,methodList.Count-1);}
            while (codePointer < code.Length && code[codePointer] != 41)
            {
                codePointer++;
            }
        }


        static void callMethod(int index, string error)
        {
            if(methodList.Count-1 < index )
            {
                throw new MethodUndefinedException(error);
            }
            returnStack.Push(codePointer);
            codePointer = methodList[index];
        }


        static void stringInput(){
            string inputString = Console.ReadLine();
            int? number = getNumberOfString(inputString);
            if(number!=null){
                tape[tapePointer]=(int)number;
                return;
            }
            char[] inputChars = inputString.ToCharArray();
            for (int i = 0; i < inputChars.Length; i++)
            {
                tape[tapePointer + i] = inputChars[i];
            }
        }


        static void ifClause(char[] code){
            if (!condition)
                {
                    while (codePointer < code.Length && code[codePointer] != 125)
                    {
                        codePointer++;
                    }
                }
        }

        static void stringLiteral(char[] code){
            codePointer++;
            string text = "";
            while (codePointer < code.Length && code[codePointer] != '"')
            {
                text+= code[codePointer];
                codePointer++;
            }
            //Check string if it is an int literal
            int? number = getNumberOfString(text);
            if(number!=null){
                tape[tapePointer]=(int)number;
                return;
            }
            int j = 0;
            while(tapePointer<tape.Length && j<text.Length){
                tape[tapePointer+j]=text[j];
                j++;
            }
        }

        
        //Helping Methods

        static int? getNumberOfString(string text){
            int identifierSize = 3;
            if(text.Length<identifierSize){return null;}
            string numberIdentifier = text.Substring(0,identifierSize);
            string number = text.Substring(identifierSize,text.Length-identifierSize);
            switch(numberIdentifier){
                case@"0d\":
                return int.Parse(number);
                case @"0b\":
                return Convert.ToInt32(number,2);
                case @"0x\":
                return Convert.ToInt32(number, 16);
            }
            return null;
        }
    }
}
