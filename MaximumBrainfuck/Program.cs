using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MaximumBrainfuck
{
    class Program
    {

        static void Main(string[] args)
        {
            IBrainfuck interpreter = new MaximumBrainfuck();

            if (args.Length == 0) return;

            if (args[0].Equals("--test"))
            { 
                //MaximumBrainfuck.preprocessImports(args[1].ToCharArray());
                return; 
            }


            try
            {
                //Read code from file, if specified
                string input = args[0];
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
