﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace MaximumBrainfuck
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
                brainfuck(args[0]);
        }

        static void brainfuck(string code)
        {
            int[] tape = new int[1000];
            Stack<int> returnStack = new Stack<int>();
            List<int> methodList = new List<int>();
            int cache = 0;
            int tapePointer = 0;
            int codePointer = 0;
            for (int i = 0; i < tape.Length; i++)
            {
                tape[i] = 0;
            }

            while (codePointer < code.Length)
            {

                switch (code.ToCharArray()[codePointer])
                {
                    case '>': tapePointer++; break;
                    case '<': tapePointer--; break;
                    case '+': tape[tapePointer]++; break;
                    case '-': tape[tapePointer]--; break;
                    case '.': Console.Write((char)tape[tapePointer]); break;
                    case ',': tape[tapePointer]=Console.ReadKey().KeyChar; break;
                    case '[':

                        if (tape[tapePointer] == 0)
                        {
                            int skip = 0;
                            int ptr = codePointer + 1;
                            while (code.ToCharArray()[ptr] != 93 || skip > 0)
                            {
                                if (code.ToCharArray()[ptr] == 91) { skip++; }
                                else if (code.ToCharArray()[ptr] == 93) { skip--; }
                                ptr++;
                                codePointer = ptr;

                            }
                        }

                        break;
                    case ']':

                        if (tape[tapePointer] != 0)
                        {
                            int skip = 0;
                            int ptr = codePointer - 1;

                            while (code.ToCharArray()[ptr] != 91 || skip > 0)
                            {
                                if (code.ToCharArray()[ptr] == 93) { skip++; }
                                else
                                if (code.ToCharArray()[ptr] == 91) { skip--; }
                                ptr--;
                                codePointer = ptr;

                            }
                        }
                        break;
                    case '_':
                        tape[tapePointer] = 0;
                        break;
                    case '=':
                        tape[tapePointer] = tape[tape[tapePointer]];
                        break;
                    case ':':
                        Console.Write(tape[tapePointer]);
                        break;
                     case '@':
                        tapePointer = tape[tapePointer];
                        break;
                    case'(':
                        methodList.Add(codePointer);
                        while(code[codePointer]!=41)
                        {
                            codePointer++;
                        }
                    break;
                    case')':
                        codePointer=returnStack.Pop();
                    break;
                    case'#':

                        returnStack.Push(codePointer);
                        codePointer=methodList[tape[tapePointer]];
                    break;
                    case '?':
                        cache = tape[tapePointer];
                        break;
                    case '!':
                        tape[tapePointer] = cache;
                        break;
                    case '^':
                        tape[tapePointer] *= tape[tapePointer];
                        break;
                    case '*':
                        tape[tapePointer] *= cache;
                        break;
                    case '/':
                        tape[tapePointer] /= cache;
                        break;
                    case '&':
                        tape[tapePointer] += cache;
                        break;    

                }
                //Console.WriteLine(tape[0]+";"+tape[1]);
                codePointer++;
            }
            
        }
    }
}
