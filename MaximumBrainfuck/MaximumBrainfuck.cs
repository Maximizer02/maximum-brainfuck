using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace MaximumBrainfuck
{
    class MaximumBrainfuck :  Brainfuck, IBrainfuck
    {
        Dictionary<string,string> importedMethods = new Dictionary<string, string>();

        void IBrainfuck.execute(string input)
        {
            char[] code = input.ToCharArray();
            preprocessImports(code);
            while (codePointer < code.Length)
            {
                brainfuck(code);
                maximumBrainfuck(code);
                codePointer++;
            }
        }
        
        
        
        void maximumBrainfuck(char[] code)
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
                    callMethod(tape[tapePointer], "No Method with index '" + tape[tapePointer] + "' defined!");
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
                    callMethod(0, "No default Method defined!");
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
                    callMethodByName(code);
                    break;
                 //case '`':
                 //   importFile(code);
                 //   break;
                    //Idea: Functions that set the current cell to the value calculated on another simulated tape
                    //Would work with stack-based cache
                    //'⍝(+++^+)§:' prints 10
                    //With not yet implemented stack based cache:
                    //  '⍝(!>+++^+?<*)⍝()+?++?++^?§:_++#§:_++#§:_++#' prints 10
            }
        }

        //Extra tidbits

        void preprocessImports(char[] code)
        {
            List<string> importedFilesNames = new List<string>();
            Stack<int> imports = new Stack<int>();
            int importSymbolCounter = 0;
            for(int i = 0; i<code.Length;i++){
                if(code[i]=='`')
                {importSymbolCounter++;
                imports.Push(i);}
            }
            while(imports.Count>0)
            {
                int a = imports.First();
                imports.Pop();
                int b = imports.First()+1;
                imports.Pop();
                importedFilesNames.Add(Utility.substringFromArray(code,b,a));
            }

            foreach (string item in importedFilesNames)
            {
                Console.WriteLine(item);
            }
        }   





        //Outsourced parts of the switch statement


        void startOfMethod(char[] code)
        {
            //check wether method has name given
            int i = 1;
            string methodName = "";
            while (code[codePointer + i] > 96 && code[codePointer + i] < 123)
            {
                methodName += code[codePointer + i];
                i++;
            }


            methodList.Add(codePointer);
            if (!methodName.Equals("")) { methodNames.Add(methodName, methodList.Count - 1); }
            while (codePointer < code.Length && code[codePointer] != 41)
            {
                codePointer++;
            }
        }


        void callMethod(int index, string error)
        {
            if (methodList.Count - 1 < index)
            {
                throw new MethodUndefinedException(error);
            }
            returnStack.Push(codePointer);
            codePointer = methodList[index];
        }


        void stringInput()
        {
            string inputString = Console.ReadLine();
            int? number = Utility.getNumberOfString(inputString);
            if (number != null)
            {
                tape[tapePointer] = (int)number;
                return;
            }
            char[] inputChars = inputString.ToCharArray();
            for (int i = 0; i < inputChars.Length; i++)
            {
                tape[tapePointer + i] = inputChars[i];
            }
        }


        void ifClause(char[] code)
        {
            if (!condition)
            {
                while (codePointer < code.Length && code[codePointer] != 125)
                {
                    codePointer++;
                }
            }
        }

        void stringLiteral(char[] code)
        {
            string text = Utility.getStringUntilChar(code, '"', true, codePointer, ref codePointer);
            //Check string if it is an int literal
            int? number = Utility.getNumberOfString(text);
            if (number != null)
            {
                tape[tapePointer] = (int)number;
                return;
            }
            int j = 0;
            while (tapePointer < tape.Length && j < text.Length)
            {
                tape[tapePointer + j] = text[j];
                j++;
            }
        }

        void callMethodByName(char[] code)
        {
            string name = Utility.getStringUntilChar(code, '´', true, codePointer, ref codePointer);
            if(importedMethods.ContainsKey(name)){
                returnStack.Push(codePointer);
                codePointer=0;
                char[] method = importedMethods[name].ToCharArray();
                while(codePointer < code.Length)
                {
                    maximumBrainfuck(method);
                    codePointer++;
                }
                return;
            }

            if (!name.Equals(""))
            {
                if (!methodNames.ContainsKey(name))
                {
                    throw new MethodUndefinedException("No Method with Name: '" + name + "' defined!");
                }
                callMethod(methodNames[name], "Error lawl");
            }
        }

        string importFile(char[] code)
        {
            string result="";
            try
            {
                Console.WriteLine(Utility.getStringUntilChar(code, '`', true,codePointer,ref codePointer));
                string file = File.ReadAllText(Utility.getStringUntilChar(code, '`', true, codePointer,ref codePointer));
                result = Utility.findAllMethodDeclarations(file.ToCharArray(),ref codePointer);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("No File found");
            }
            return result;
        }

    }
}