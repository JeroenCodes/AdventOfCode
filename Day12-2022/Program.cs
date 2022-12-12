using System;
using System.Collections.Generic;

namespace ElevationSolver
{
    class Program
    {
        static string[] input = File.ReadAllText("/Users/jbijl005/Documents/test.txt").Split("\r\n");
        static char[][] map = new char[input.Length][];



        static void Main(string[] args)
        {
            // Sample input
            var testMap = new char[][]
            {
                new char[] { 'S', 'a', 'b', 'q', 'p', 'o', 'n', 'm' },
                new char[] { 'a', 'b', 'c', 'r', 'y', 'x', 'x', 'l' },
                new char[] { 'a', 'c', 'c', 's', 'z', 'E', 'x', 'k' },
                new char[] { 'a', 'c', 'c', 't', 'u', 'v', 'w', 'j' },
                new char[] { 'a', 'b', 'd', 'e', 'f', 'g', 'h', 'i' }
            };
            for (int i = 0; i < input.Length - 1; i++)
            {
                string currentString = input[i];
                char[] currentCharArray = currentString.ToCharArray();

                map[i] = currentCharArray;
            }

            Part1();
            static void Part1()
            {
                int startRow = 0;
                int startCol = 0;
                for (int row = 0; row < map.Length - 1; row++)
                {
                    for (int col = 0; col < map[row].Length; col++)
                    {
                        if (map[row][col] == 'S')
                        {
                            startRow = row;
                            startCol = col;
                            break;
                        }
                    }
                }

                int endRow = 0;
                int endCol = 0;
                for (int row = 0; row < map.Length - 1; row++)
                {
                    for (int col = 0; col < map[row].Length; col++)
                    {
                        if (map[row][col] == 'E')
                        {
                            endRow = row;
                            endCol = col;
                            break;
                        }
                    }
                }
                var result = BFS(ConvertTo2D(map), new Tuple<int, int>(startRow, startCol), new Tuple<int, int>(endRow, endCol));
                Console.WriteLine(result);

            }
            Part2();
            static void Part2()
            {
                IDictionary<Tuple<int, int>, int> paths = new Dictionary<Tuple<int, int>, int>();
                List<Tuple<int, int>> startPoints = new List<Tuple<int, int>>();
                int endRow = 0;
                int endCol = 0;
                for (int row = 0; row < map.Length - 1; row++)
                {
                    for (int col = 0; col < map[row].Length; col++)
                    {
                        if (map[row][col] == 'E')
                        {
                            endRow = row;
                            endCol = col;
                            break;
                        }
                    }
                }
                for (int row = 0; row < map.Length - 1; row++)
                {
                    for (int col = 0; col < map[row].Length; col++)
                    {
                        if (map[row][col] == 'S' || map[row][col] == 'a')
                        {
                            startPoints.Add(new Tuple<int, int>(row, col));
                        }
                    }
                }
                Console.WriteLine("Found: " + startPoints.Count() + " starting points");
                char[,] newMap = ConvertTo2D(map);

                foreach (Tuple<int, int> startingPoint in startPoints)
                {
                    var result = BFS(newMap, startingPoint, new Tuple<int, int>(endRow, endCol));
                    if(result != 0)
                    {
                        paths.Add(startingPoint, result);
                    }
                    
                }
                Console.WriteLine(paths.MinBy(s => s.Value));

            }

        }
        static int BFS(char[,] map, Tuple<int, int> start, Tuple<int, int> end)
        {
            Queue<Tuple<int, int>> queue = new Queue<Tuple<int, int>>();
            queue.Enqueue(start);

            Dictionary<Tuple<int, int>, bool> visited = new Dictionary<Tuple<int, int>, bool>();
            visited[start] = true;

            Dictionary<Tuple<int, int>, Tuple<int, int>> previous = new Dictionary<Tuple<int, int>, Tuple<int, int>>();

            while (queue.Count > 0)
            {
                Tuple<int, int> current = queue.Dequeue();
                if (current.Equals(end))
                {
                    List<Tuple<int, int>> path = new List<Tuple<int, int>>();
                    while (current != null)
                    {
                        path.Add(current);
                        if (!previous.ContainsKey(previous[current]))
                        {
                            current = null;
                        }
                        else
                        {
                            current = previous[current];
                        }
                        
                    }
                    path.Reverse();
                    return path.Count();
                }

                Tuple<int, int>[] neighbors = GetNeighbors(map, current);
                foreach (Tuple<int, int> neighbor in neighbors)
                {
                    if (!visited.ContainsKey(neighbor))
                    {
                        char currentElevation = map[current.Item1, current.Item2];
                        if (currentElevation == 'S') currentElevation = 'a';
                        char neigbourElevation = map[neighbor.Item1, neighbor.Item2];
                        if (neigbourElevation == 'E') neigbourElevation = 'z';

                        if (neigbourElevation == (char)currentElevation + 1 || neigbourElevation == (char)currentElevation || neigbourElevation < currentElevation)
                        {
                            visited[neighbor] = true;

                            queue.Enqueue(neighbor);

                            previous[neighbor] = current;
                        }
                    }
                }
            }
            return 0;

        }

        static public Tuple<int, int>[] GetNeighbors(char[,] map, Tuple<int, int> current)
        {
            List<Tuple<int, int>> neighbors = new List<Tuple<int, int>>();

            int x = current.Item1;
            int y = current.Item2;
            if (x > 0 && map[x - 1, y] != 0)
            {
                neighbors.Add(new Tuple<int, int>(x - 1, y));
            }
            if (x < map.GetLength(0) - 1 && map[x + 1, y] != 0)
            {
                neighbors.Add(new Tuple<int, int>(x + 1, y));
            }
            if (y > 0 && map[x, y - 1] != 0)
            {
                neighbors.Add(new Tuple<int, int>(x, y - 1));
            }
            if (y < map.GetLength(1) - 1 && map[x, y + 1] != 0)
            {
                neighbors.Add(new Tuple<int, int>(x, y + 1));
            }

            return neighbors.ToArray();
        }

        static public char[,] ConvertTo2D(char[][] map)
        {
            int rows = map.Length-1;

            int columns = map[0].Length;

            char[,] twoDimensionalArray = new char[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    twoDimensionalArray[i, j] = map[i][j];
                }
            }

            return twoDimensionalArray;
        }

    }
}