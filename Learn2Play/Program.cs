using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn2Play
{
    class Program
    {
        static void Main(string[] args)
        {
            //Caution: violently incomplete
            Console.WriteLine("Welcome to the Learn2Play program. \n");
            Console.WriteLine("Onrush of randomly selected notes is imminent. Press enter...");
            string input = String.Empty;
            while(true)
            {
                input = Console.ReadLine();
                if (input.ToLower() == "exit")
                    break;

                Console.WriteLine(GetRandomNote());
            }
        }

        private static string GetRandomNote()
        {
            string[] notes = new string[] { "a", "h", "c", "d", "e", "f", "g" };
            Random r = new Random();
            string sign = r.NextDouble() > 0.5 ? "#" : "b";
            return notes[r.Next(6)] + (r.NextDouble() > 0.5 ? sign : "");
        }
    }
}
