
using System.Drawing;
using System.Reflection;

internal class Program
{
    public static class RopeCalculations
    {


        static string InputFilePath = "/Users/jbijl005/Documents/test.txt";
        static string[] input = File.ReadAllText(InputFilePath).Split("\r\n");

        private static void Main(string[] args)
        {
            Part1(new Point(0, 0), new Point(0, 0));
            static void Part1(Point startPointH, Point startPointT)
            {

                //Point startPoint = new Point(0, 0);
                List<Point> visited = new List<Point>() { startPointH };

                Point TPosition = startPointT;
                Point HPosition = startPointH;

                //move around
                foreach (string command in input)
                {
                    //var command = input[x];
                    if (command == "") break;
                    var direction = command.Substring(0, 1).FirstOrDefault();
                    var stepsString = command.Substring(2, command.Length - 2);
                    var steps = int.Parse(stepsString);
                    //Move

                    for (int s = 0; s < steps; s++)
                    {
                        //move T
                        switch (direction)
                        {
                            case 'R': TPosition.X += 1; break;
                            case 'U': TPosition.Y += 1; break;
                            case 'L': TPosition.X -= 1; break;
                            case 'D': TPosition.Y -= 1; break;
                            default: break;
                        }
                        Point relativeProximity = RelativeProximity(TPosition, HPosition, direction);

                        //Check if H needs moving
                        if (NeedMove(relativeProximity))
                        {
                            //move H
                            HPosition = Move(HPosition, relativeProximity);
                            if (!visited.Any(s => s == HPosition))
                            {
                                visited.Add(HPosition);
                            }
                        }

                    }
                }
                Console.WriteLine(visited.Count());
            }

            Part2();
            static void Part2()
            {
                List<Point> snake = new List<Point>() { new Point(0,0), new Point(0, 0) , new Point(0, 0) , new Point(0, 0) , new Point(0, 0) , new Point(0, 0) , new Point(0, 0) , new Point(0, 0) , new Point(0, 0), new Point(0, 0) };
                List<Point> visited = new List<Point>() { new Point(0, 0) };

                foreach (string command in input)
                {
                    if (command == "") break;
                    var direction = command.Substring(0, 1).FirstOrDefault();
                    var stepsString = command.Substring(2, command.Length - 2);
                    var steps = int.Parse(stepsString);

                    for (int s = 0; s < steps; s++)
                    {
                        for(int d = 0; d< snake.Count-1; d++)
                        {
                            var point1 = snake[d];
                            var point2 = snake[d + 1];

                            if(d == 0)
                            {
                                switch (direction)
                                {
                                    case 'R': point1.X += 1; break;
                                    case 'U': point1.Y += 1; break;
                                    case 'L': point1.X -= 1; break;
                                    case 'D': point1.Y -= 1; break;
                                    default: break;
                                }
                            }

                            Point relativeProximity = RelativeProximity(point1, point2, direction);

                            if (NeedMove(relativeProximity))
                            {
                                //move H
                                point2 = Move(point2, relativeProximity);
                                if (d == 8 && !visited.Any(s => s == point2))
                                {
                                    visited.Add(point2);
                                }
                            }
                            snake[d] = point1;
                            snake[d + 1] = point2;
                        }
                    }
                }
                Console.WriteLine(visited.Count());
            }

        }
     

        public static Point Move(Point startingPoint, Point relative)
        {
            Point newPosition = startingPoint;
            if(relative.X >= 2)
            {
                switch (relative.Y)
                    {
                    case -1: newPosition.X += 1; newPosition.Y -= 1; break;
                    case 1: newPosition.X += 1; newPosition.Y += 1; break;
                    case -2: newPosition.X += 1; newPosition.Y -= 1; break;
                    case 2: newPosition.X += 1; newPosition.Y += 1; break;
                    default: newPosition.X += 1; break;
                }
            }else
            if (relative.Y >= 2)
            {
                switch (relative.X)
                {
                    case -1: newPosition.X -= 1; newPosition.Y += 1; break;
                    case 1: newPosition.X += 1; newPosition.Y += 1; break;
                    case -2: newPosition.X -= 1; newPosition.Y += 1; break;
                    case 2: newPosition.X += 1; newPosition.Y += 1; break;
                    default: newPosition.Y += 1; break;
                }
            }else
            if (relative.X <= -2)
            {
                switch (relative.Y)
                {
                    case -1: newPosition.X -= 1; newPosition.Y -= 1; break;
                    case 1: newPosition.X -= 1; newPosition.Y += 1; break;
                    case -2: newPosition.X -= 1; newPosition.Y -= 1; break;
                    case 2: newPosition.X -= 1; newPosition.Y += 1; break;
                    default: newPosition.X -= 1; break;
                }
            }else
            if (relative.Y <= -2)
            {
                switch (relative.X)
                {
                    case -1: newPosition.X -= 1; newPosition.Y -= 1; break;
                    case 1: newPosition.X += 1; newPosition.Y -= 1; break;
                    case -2: newPosition.X -= 1; newPosition.Y -= 1; break;
                    case 2: newPosition.X += 1; newPosition.Y -= 1; break;
                    default: newPosition.Y -= 1; break;
                }
            }
            return newPosition;
        }


        public static bool NeedMove(Point relative)
        {
            if((-1 <= relative.X && relative.X <= 1) && (-1 <= relative.Y && relative.Y <= 1))
            {
                return false;
            }
            return true;
        }

        public static Point RelativeProximity(Point tPosition, Point hPoistion, char direction)
        {
            int x = tPosition.X - hPoistion.X;
            int y = tPosition.Y - hPoistion.Y;
            return new Point(x, y);
        }

    }
}
