using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Globalization;

namespace MaximumBrainfuck
{
    class Program
    {
        static double version = 1.1;

        static void Main(string[] args)
        {
            IBrainfuck interpreter;

            if (args.Length == 0) return;

            try
            {
                //Read code from file, if specified
                string input;
                if (args[0].StartsWith("-"))
                {
                    input = args[1];

                    switch (args[0])
                    {
                        case "-s":
                            interpreter = new Brainfuck();
                            break;

                        case "-v":
                            Console.WriteLine("Current version of Maximum Brainfuck Interpreter: " + version.ToString(new CultureInfo("en-Us")));
                            return;

                        default:
                            interpreter = new MaximumBrainfuck();
                            break;
                    }
                }
                else 
                { 
                    input = args[0];
                     interpreter = new Brainfuck(); 
                }

                string[] endings = { ".mbf", ".bf" };

                if (File.Exists(input) && endings.Any(i => input.EndsWith(i)))
                {
                    input = File.ReadAllText(input);
                }

                interpreter.execute(input);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                Console.ResetColor();
            }

        }



    }
}
