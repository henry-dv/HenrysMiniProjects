using System;
using System.IO;

namespace SelfDescription
{
    class Program
    {
        static void Main(string[] args)
        {
            // Hello :) it's me again

            //I'm currently writing something into a console by putting a comment into a C# script...

            Console.Write(File.ReadAllText(Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName, "Program.cs")));
            Console.ReadLine();
        }
    }
}
