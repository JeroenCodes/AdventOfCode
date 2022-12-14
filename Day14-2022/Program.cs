using System;
using System.Collections.Generic;

namespace CaveScan;

class Program
{
    static List<string> input = File.ReadAllText("/Users/jbijl005/Documents/test.txt").Split("\r\n").ToList();
    static void Main(string[] args)
    {
        // List to store the coordinates of the rock structures
        IDictionary<int, List<(int x, int y)>> rockCoordinates = new Dictionary<int, List<(int x, int y)>>();
        List<(int x, int y)> inputRocks = new List<(int x, int y)>();

        // Read the input and track the coordinates of the rock structures
        //string input = "498,4 -> 498,6 -> 496,6";
        //string[] lines = input.Split('\n');
        for (int z = 0; z < input.Count() - 1; z++)
        {
            string[] coordinates = input[z].Split(new[] { "->" }, StringSplitOptions.None);
            rockCoordinates.Add(z, new List<(int x, int y)> { });

            // Loop through the coordinates and add them to the list
            for (int i = 0; i < coordinates.Length; i++)
            {
                string[] xy = coordinates[i].Trim().Split(',');
                int x = int.Parse(xy[0]);
                int y = int.Parse(xy[1]);
                rockCoordinates[z].Add((x, y));
            }
        }

        //Now all the "lines" are in the input, how to draw the straight lines/register where all the rocks are?
        foreach (var entry in rockCoordinates)
        {
            //get the difference between current entry and the next, register all those coordinates in an all rocks list
            for (int i = 0; i < entry.Value.Count() - 1; i++)
            {
                //compare i and i+1
                if (entry.Value[i].x != entry.Value[i + 1].x)
                {
                    int diff = entry.Value[i].x - entry.Value[i + 1].x;
                    if (diff < 0)
                    {
                        //moving right
                        for (int z = 0; z <= Math.Abs(diff); z++)
                        {
                            int newX = entry.Value[i].x + z;
                            if (inputRocks.Any(x => x.x == newX && x.y == entry.Value[i].y)) continue;
                            inputRocks.Add((newX, entry.Value[i].y));
                        }
                    }
                    else
                    {
                        //moving left
                        for (int z = 0; z <= Math.Abs(diff); z++)
                        {
                            int newX = entry.Value[i].x - z;
                            if (inputRocks.Any(x => x.x == newX && x.y == entry.Value[i].y)) continue;
                            inputRocks.Add((newX, entry.Value[i].y));
                        }
                    }
                }
                else if (entry.Value[i].y != entry.Value[i + 1].y)
                {
                    int diff = entry.Value[i].y - entry.Value[i + 1].y;
                    if (diff < 0)
                    {
                        //moving up
                        for (int z = 0; z <= Math.Abs(diff); z++)
                        {
                            int newY = entry.Value[i].y + z;
                            if (inputRocks.Any(x => x.x == entry.Value[i].x && x.y == newY)) continue;
                            inputRocks.Add((entry.Value[i].x, newY));
                        }
                    }
                    else
                    {
                        //moving left
                        for (int z = 0; z <= Math.Abs(diff); z++)
                        {
                            int newY = entry.Value[i].y - z;
                            if (inputRocks.Any(x => x.x == entry.Value[i].x && x.y == newY)) continue;
                            inputRocks.Add((entry.Value[i].x, newY));
                        }
                    }
                }

            }
        }

        Console.WriteLine("part 1: ");
        Console.WriteLine(DropSand(inputRocks));

        Console.WriteLine("part 2: ");
        Console.WriteLine(DropSand2(inputRocks));
    }
    static int DropSand(List<(int x, int y)> inputRocks)
    {
        bool falling = true;
        int sandCounter = 0;
        SandUnit lastDrop = new SandUnit();
        List<(int x, int y)> allRocks = new List<(int x, int y)>();
        allRocks.AddRange(inputRocks);
        //sandCoordinates.Add(counter, (500, 0));
        while (falling)
        {
            SandUnit current = new SandUnit();
            //if can move down, move down
            var firstObstacle = allRocks.Where(x => x.x == 500).OrderBy(z => z.y).FirstOrDefault();
            bool atRest = false;
            while(!atRest)
            {
                while(CanMoveDown(current.cords.x, current.cords.y, allRocks))
                {
                    //one above the first obstacle. Check, left, then right
                    current.cords = Move(current.cords.x, current.cords.y, 0);
                    if (current.cords.y >= allRocks.OrderByDescending(z => z.y).Select(z => z.y).FirstOrDefault())
                    {
                        //freefall
                        falling = false;
                        atRest = true;
                        break;
                    }
                }
                if(CanMoveLeft(current.cords.x, current.cords.y, allRocks))
                {
                    current.cords = Move(current.cords.x, current.cords.y, -1);
                    continue;
                }
                if(CanMoveRight(current.cords.x, current.cords.y, allRocks))
                {
                    current.cords = Move(current.cords.x, current.cords.y, 1);
                    continue;
                }
                if(!CanMoveDown(current.cords.x, current.cords.y, allRocks) && !CanMoveLeft(current.cords.x, current.cords.y, allRocks) && !CanMoveRight(current.cords.x, current.cords.y, allRocks))
                {
                    allRocks.Add(current.cords);
                    sandCounter++;
                    lastDrop = current;
                    atRest = true;
                }

            }


            continue;
        }
        return sandCounter;

    }
    static int DropSand2(List<(int x, int y)> inputRocks)
    {
        bool falling = true;
        int sandCounter = 0;
        SandUnit lastDrop = new SandUnit();
        List<(int x, int y)> allRocks = inputRocks;
        int bottom = allRocks.OrderByDescending(z => z.y).Select(z => z.y).FirstOrDefault();
        for(int x=300; x <=850; x++)
        {
            (int x, int y) tuple = (x, bottom + 2);
            allRocks.Add(tuple);
        }

        while (falling)
        {
            SandUnit current = new SandUnit();
            //if can move down, move down
            var firstObstacle = allRocks.Where(x => x.x == 500).OrderBy(z => z.y).FirstOrDefault();
            bool atRest = false;
            while (!atRest)
            {
                while (CanMoveDown(current.cords.x, current.cords.y, allRocks))
                {
                    //one above the first obstacle. Check, left, then right
                    current.cords = Move(current.cords.x, current.cords.y, 0);
                    //if (current.cords.y )
                    //{
                    //    //freefall
                    //    falling = false;
                    //    atRest = true;
                    //    break;
                    //}
                }
                if (CanMoveLeft(current.cords.x, current.cords.y, allRocks))
                {
                    current.cords = Move(current.cords.x, current.cords.y, -1);
                    continue;
                }
                if (CanMoveRight(current.cords.x, current.cords.y, allRocks))
                {
                    current.cords = Move(current.cords.x, current.cords.y, 1);
                    continue;
                }
                if (!CanMoveDown(current.cords.x, current.cords.y, allRocks) && !CanMoveLeft(current.cords.x, current.cords.y, allRocks) && !CanMoveRight(current.cords.x, current.cords.y, allRocks))
                {
                    if(current.cords.x == 500 && current.cords.y ==0)
                    {
                        falling = false;
                        atRest = true;
                        break;
                    }
                    allRocks.Add(current.cords);
                    sandCounter++;
                    if (sandCounter % 1000 == 0) Console.WriteLine(sandCounter);
                    lastDrop = current;
                    atRest = true;
                }

            }


            continue;
        }
        return sandCounter;

    }

    static bool CanMoveDown(int x, int y, List<(int x, int y)> allRocks)
    {
        if (!isOccupied(x, y+1, allRocks)) return true;
        return false;
    }
    static bool CanMoveLeft(int x, int y, List<(int x, int y)> allRocks)
    {
        if (!isOccupied(x - 1, y+1, allRocks)) return true;
        return false;
    }
    static bool CanMoveRight(int x, int y, List<(int x, int y)> allRocks)
    {
        if (!isOccupied(x + 1, y+1, allRocks)) return true;
        return false;
    }
    static (int x, int y) Move(int x, int y, int direction)
    {
        var newCords = (0, 0);
        if (direction == 0)
        {
            newCords = (x, y + 1);
        }
        if (direction > 0)
        {
            newCords = (x + 1, y + 1);

        }
        else if (direction < 0)
        {
            newCords = (x - 1, y + 1);
        }
        return newCords;
    }

    static bool isOccupied(int x, int y,List<(int x, int y)> allRocks)
    {
        if ( allRocks.Any(z => z.x == x && z.y == y)) return true;
        return false;
    }
}

public class SandUnit
{
    public int number { get; set; }
    public (int x, int y) cords { get; set; }

    public SandUnit()
    {
        cords = (500, 0);
    }
}
