using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintBinaryStrings
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintBinaryString("Wer das liest ist ein Nerd :)");
            Console.ReadLine();
        }

        static void PrintBinaryString(string input)
        {
            foreach (string word in input.Split(' '))
            {
                foreach (char c in word)
                {
                    Console.Write($"{ToBinaryString(c)} ");
                }
                Console.WriteLine();
            }
        }

        static string ToBinaryString(char c, int length = 7)
        {
            int letter = (int)c;
            int max = (int)Math.Pow(2, length - 1);
            string output = String.Empty;

            for(int i = 0; i < length; i++)
            {
                output += (letter & max) == max ? "1" : "0";
                letter <<= 1;
            }

            return output;
        }
    }
}
