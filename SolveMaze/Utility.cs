using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static HenrysDevLib.Extensions.MultiDimArrayExtensions;

namespace SolveMaze
{
    public static class Utility
    {
        public enum Direction
        {
            North = 0,
            East,
            South,
            West
        };
        public struct Node
        {
            public readonly (int X, int Y) Position;
            public int? North { get; set; }
            public int? East { get; set; }
            public int? South { get; set; }
            public int? West { get; set; }

            public Node(int x, int y, int? north = null, int? east = null, int? south = null, int? west = null)
            {
                this.Position = (x, y);
                this.North = north;
                this.East = east;
                this.South = south;
                this.West = west;
            }
        }

        #region Algorithm related

        #region Non-node related
        public static bool Peek(in int[,] maze, (int x, int y) position, in Direction dir)
        {
            TakeStep(ref position, dir);
            if (position.x < 0 || position.x >= maze.GetLength(0) ||
                position.y < 0 || position.y >= maze.GetLength(1))
            {
                return false;
            }

            return maze[position.x, position.y] == 0;
        }

        public static void TakeStep(ref (int x, int y) position, in Direction dir)
        {
            position.x += dir switch
            {
                Direction.East => 1,
                Direction.West => -1,
                _ => 0
            };
            position.y += dir switch
            {
                Direction.North => -1,
                Direction.South => 1,
                _ => 0
            };
        }

        public static (int, int) FindStart(int[,] maze)
        {
            return maze.IndicesOf(0); // IndicesOf() returns first occurence of argument, stepping through x first, then y. 
                                      // For this to work, the entry point of the maze has to be in the top row.
        }

        public static (int, int) FindExit(int[,] maze)
        {
            int y = maze.GetLength(1) - 1;

            for (int x = 0; x < maze.GetLength(0); x++)
            {
                if (maze[x, y] == 0)
                    return (x, y);
            }
            return (-1, -1);
        }
        #endregion

        #region Node-related
        public static Node[] Nodify(int[,] maze)
        {
            List<Node> nodeList = new List<Node>();

            //generate the nodes

            (int sx, _) = FindStart(maze);
            nodeList.Add(new Node(sx, 0, south: -1)); //start node

            for (int y = 0; y < maze.GetLength(1); y++)
            {
                for (int x = 0; x < maze.GetLength(0); x++)
                {
                    if (maze[x, y] == 0)
                    {
                        bool
                            canGoNorth = Peek(in maze, (x, y), Direction.North),
                            canGoEast  = Peek(in maze, (x, y), Direction.East),
                            canGoSouth = Peek(in maze, (x, y), Direction.South),
                            canGoWest  = Peek(in maze, (x, y), Direction.West);

                        int dirCount = 0;
                        dirCount += canGoNorth ? 1 : 0;
                        dirCount += canGoEast  ? 1 : 0;
                        dirCount += canGoSouth ? 1 : 0;
                        dirCount += canGoWest  ? 1 : 0;

                        if (dirCount < 2) continue; //dead ends don't need a node

                        if (dirCount == 2)
                        {
                            if ((canGoNorth && canGoSouth) || (canGoWest && canGoEast)) //is this point part of a straight
                                continue;
                        }

                        Node n = new Node(x, y);
                        if (canGoNorth) n.North = -1;
                        if (canGoEast) n.East = -1;
                        if (canGoSouth) n.South = -1;
                        if (canGoWest) n.West = -1;
                        nodeList.Add(n);
                    }
                }
            }
            (int ex, int ey) = FindExit(maze);
            nodeList.Add(new Node(ex, ey, north: -1)); //exit node

            //connect the nodes
            for (int i = 0; i < nodeList.Count; i++)
            {
                foreach (Direction dir in Enum.GetValues(typeof(Direction)))
                {
                    if (GetNodeNeighbourIdByDir(nodeList[i], dir) == null) continue;

                    (int, int) pos = nodeList[i].Position;

                    while (Peek(in maze, pos, dir))
                    {
                        TakeStep(ref pos, in dir);
                        if (nodeList.Exists(e => e.Position == pos))
                        {
                            int nodeIndex = nodeList.FindIndex(e => e.Position == pos);
                            Node n = nodeList[i]; //nessecairy because https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/cs1612
                            if (dir == Direction.North) n.North = nodeIndex;
                            else if (dir == Direction.East) n.East = nodeIndex;
                            else if (dir == Direction.South) n.South = nodeIndex;
                            else if (dir == Direction.West) n.West = nodeIndex;
                            nodeList[i] = n;
                        }
                    }
                }
            }

            return nodeList.ToArray();
        }

        static int? GetNodeNeighbourIdByDir(Node n, Direction dir) => dir switch
        {
            Direction.North => n.North,
            Direction.East => n.East,
            Direction.South => n.South,
            Direction.West => n.West,
            _ => null
        };
        #endregion

        #endregion
    }
}
