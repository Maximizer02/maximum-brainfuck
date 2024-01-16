using System;
using System.Security.Cryptography;
namespace MaximumBrainfuck
{
    class Utility
    {
        //Helping Methods

        public static int? getNumberOfString(string text)
        {
            int identifierSize = 3;
            if (text.Length < identifierSize) { return null; }
            string numberIdentifier = text.Substring(0, identifierSize);
            string number = text.Substring(identifierSize, text.Length - identifierSize);
            switch (numberIdentifier)
            {
                case @"0d\":
                    return int.Parse(number);
                case @"0b\":
                    return Convert.ToInt32(number, 2);
                case @"0x\":
                    return Convert.ToInt32(number, 16);
            }
            return null;
        }
    

        public static string getStringUntilChar(char[] code, char character, bool syncPointer, int startingPoint, ref int codePointer)
        {
            string result = "";
            int ptr = startingPoint;
            ptr++;
            while (ptr < code.Length && code[ptr] != character)
            {
                result += code[ptr];
                ptr++;
            }
            if (syncPointer) { codePointer = ptr; }
            return result;
        }

        public static string findAllMethodDeclarations(char[] code, ref int codePointer)
        {
            string result = "";
            for (int i = 0; i < code.Length; i++)
            {
                if (code[i].Equals('('))
                {
                    result += "(" + getStringUntilChar(code, ')', false, 0, ref codePointer) + ")";
                }
            }
            return result;
        }

        public static string substringFromArray(char[] code, int first, int second)
        {
            int length = second-first;
            string result = "";
            for (int i = 0; i < length; i++)
            {
                result+=code[first+i];
            }
            return result;
        }
    }
}