using System;

namespace MaximumBrainfuck
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        static void brainfuck(string arg)
        {

            int[] arr = new int[1000];
            int pointer = 500;
            int a = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = 0;
            }

            while (a < arg.Length)
            {

                switch (arg.ToCharArray()[a])
                {
                    case '>': pointer++; break;
                    case '<': pointer--; break;
                    case '+': arr[pointer]++; break;
                    case '-': arr[pointer]--; break;
                    case '.': Console.Write((char)arr[pointer]); break;
                    case '[':

                        if (arr[pointer] == 0)
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

                        if (arr[pointer] != 0)
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
                }
                a++;
            }

        }
    }
}
