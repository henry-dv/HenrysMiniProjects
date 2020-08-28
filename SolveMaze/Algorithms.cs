using System;
using System.Collections.Generic;
using System.Text;
using static SolveMaze.Utility;

namespace SolveMaze
{
    public static class Algorithms
    {
        public static int[,] RightHandRule(int[,] maze, out bool solved) //dumb
        {
            solved = false;
            (int, int) start = FindStart(maze);
            (int, int) position = start;
            (int, int) exit = FindExit(maze);
            Direction dir = Direction.South;
            Stack<Direction> path = new Stack<Direction>();

            while (position != exit)
            {
                dir = (Direction)(((int)dir + 1) % 4); //turn right

                for (int i = 0; i < 4; i++)
                {
                    if (Peek(in maze, position, in dir))
                    {
                        TakeStep(ref position, in dir);

                        if (position == start)
                            return maze;

                        if (path.Count > 0 && Math.Abs((int)dir - (int)path.Peek()) == 2)
                            path.Pop();
                        else
                            path.Push(dir);

                        break;
                    }
                    dir = (Direction)(((int)dir + 3) % 4);
                }
            }

            solved = true;
            Stack<Direction> reversed = new Stack<Direction>();
            while (path.Count > 0)
                reversed.Push(path.Pop());

            (int x, int y) = position = start;
            maze[x, y] = 2;
            while (reversed.Count > 0)
            {
                TakeStep(ref position, reversed.Pop());
                (x, y) = position;
                maze[x, y] = 2;
            }

            return maze;
        }
    }
}
