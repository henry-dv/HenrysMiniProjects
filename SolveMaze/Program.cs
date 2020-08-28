using System;
using System.Collections.Generic;
using static SolveMaze.Algorithms;
using static HenrysDevLib.Extensions.MultiDimArrayExtensions;
using System.Diagnostics;
using static SolveMaze.Utility;

namespace SolveMaze
{
    class Program
    {
        delegate int[,] MazeSolver(int[,] maze, out bool solved);

        static int[,] veryTinyMaze = new int[,]
        {
            { 1, 1, 1, 0, 1, 1 },
            { 1, 0, 0, 0, 0, 1 },
            { 1, 0, 1, 1, 0, 1 },
            { 1, 0, 0, 1, 0, 1 },
            { 1, 1, 1, 1, 0, 1 },
            { 1, 0, 0, 0, 0, 1 },
            { 1, 0, 1, 1, 1, 1 }
        }.FlipXY();

        static int[,] tinyMaze = new int[,]
        {
            {1,1,1,0,1,1,1,1,1,1},
            {1,0,0,0,0,0,0,0,0,1},
            {1,0,1,1,1,1,0,1,0,1},
            {1,0,0,0,1,0,0,1,0,1},
            {1,0,1,0,1,0,1,1,0,1},
            {1,0,0,0,0,0,1,0,0,1},
            {1,1,1,1,1,0,1,1,1,1},
            {1,0,1,0,1,0,1,1,0,1},
            {1,0,0,0,0,0,0,0,0,1},
            {1,1,1,1,1,1,1,0,1,1}
        }.FlipXY();

        static void Main(string[] args)
        {
            //var m = tinyMaze;
            //Node[] nodes = Nodify(m);
            //foreach (Node n in nodes)
            //{
            //    (int x, int y) = n.Position;
            //    m[x, y] = 3;
            //}
            //PrintMaze(m);
            //return;


            int[,] maze = tinyMaze;
            MazeSolver solveMaze = RightHandRule;

            Stopwatch sw = new Stopwatch();

            Console.WriteLine("Maze:");
            PrintMaze(maze);
            Console.WriteLine("Crunching numbers...");
            sw.Start();
            int[,] solvedMaze = solveMaze(maze, out bool solved);
            sw.Stop();
            if (solved)
            {
                Console.WriteLine("Solution found!");
                PrintMaze(solvedMaze);
            }
            else
            {
                Console.WriteLine("No solution found!");
            }
            Console.WriteLine($"\nElapsed time: {sw.ElapsedMilliseconds} ms");

            Console.ReadLine();
        }

        static void PrintMaze(int[,] maze)
        {
            string line;
            for (int y = 0; y < maze.GetLength(1); y++)
            {
                line = string.Empty;
                for (int x = 0; x < maze.GetLength(0); x++)
                {
                    line += maze[x, y] switch
                    {
                        0 => "░",
                        1 => "█",
                        2 => "▓",
                        _ => "?"
                    };
                }
                Console.WriteLine(line);
            }
        }
    }
}
