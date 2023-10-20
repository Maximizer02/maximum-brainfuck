using System;

namespace MaximumBrainfuck
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
                brainfuck(args[0]);
        }

        static void brainfuck(string arg)
        {

            int[] tape = new int[1000];
            int pointer = 0;
            int a = 0;
            for (int i = 0; i < tape.Length; i++)
            {
                tape[i] = 0;
            }

            while (a < arg.Length)
            {

                switch (arg.ToCharArray()[a])
                {
                    case '>': pointer++; break;
                    case '<': pointer--; break;
                    case '+': tape[pointer]++; break;
                    case '-': tape[pointer]--; break;
                    case '.': Console.Write((char)tape[pointer]); break;
                    case '[':

                        if (tape[pointer] == 0)
                        {
                            int skip = 0;
                            int ptr = a + 1;
                            while (arg.ToCharArray()[ptr] != 93 || skip > 0)
                            {
                                if (arg.ToCharArray()[ptr] == 91) { skip++; }
                                else if (arg.ToCharArray()[ptr] == 93) { skip--; }
                                ptr++;
                                a = ptr;

                            }
                        }

                        break;
                    case ']':

                        if (tape[pointer] != 0)
                        {
                            int skip = 0;
                            int ptr = a - 1;

                            while (arg.ToCharArray()[ptr] != 91 || skip > 0)
                            {
                                if (arg.ToCharArray()[ptr] == 93) { skip++; }
                                else
                                if (arg.ToCharArray()[ptr] == 91) { skip--; }
                                ptr--;
                                a = ptr;

                            }
                        }
                        break;
                    case '_':
                        tape[pointer] = 0;
                        break;
                    case '@':
                        tape[pointer] = tape[tape[pointer]];
                        break;
                }
                a++;
            }

        }
    }
}
