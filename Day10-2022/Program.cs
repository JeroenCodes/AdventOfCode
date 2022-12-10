

internal class Program
{
    static string[] input = File.ReadAllText("/Users/jbijl005/Documents/test.txt").Split("\r\n");
    public class RopeCalculations
    {
        static List<string> instructionsList = input.ToList();
        private static void Main(string[] args)
        {

            Part1(new CPU());
            static void Part1(CPU cpu)
            {
                int cycle = 0;
                List<int> signals = new List<int>();
                List<int> cyclesToCheck = new List<int>() { 20, 60, 100, 140, 180, 220 };
                foreach (string instruction in instructionsList)
                {
                    if (instruction == "noop")
                    {
                        cpu.Noop();
                        cycle++;
                        if (cyclesToCheck.Any(s => s == cycle))
                        {
                            int signal = cycle * cpu.X;
                            signals.Add(signal);
                        }
                    }
                    else if (instruction.StartsWith("addx"))
                    {

                        int v = int.Parse(instruction.Split(' ')[1]);
                        cycle++;
                        if (cyclesToCheck.Any(s => s == cycle))
                        {
                            int signal = cycle * cpu.X;
                            signals.Add(signal);
                        }
                        cycle++;
                        if (cyclesToCheck.Any(s => s == cycle))
                        {
                            int signal = cycle * cpu.X;
                            signals.Add(signal);
                        }

                        cpu.AddX(v);
                    }
                }
                Console.WriteLine("Part 1: " + signals.Sum());
            }

            Part2(new CPU(), new CRT(), new Sprite());
            static void Part2(CPU cpu, CRT cRT, Sprite sprite)
            {
                int cycle = 0;
                int crtRow = 0;

                foreach (string instruction in instructionsList)
                {
                    if (instruction == "noop")
                    {
                        cpu.Noop();
                        if (sprite.IsVisibleAt(cycle))
                        {
                            cRT.DrawPixel(cycle, crtRow);
                        }
                        cycle++;
                        if (cycle == 40)
                        {
                            crtRow++;
                            cycle = 0;
                            sprite.MoveTo(cpu.X);
                        }
                    }
                    else if (instruction.StartsWith("addx"))
                    {

                        int v = int.Parse(instruction.Split(' ')[1]);
                        if (sprite.IsVisibleAt(cycle))
                        {
                            cRT.DrawPixel(cycle, crtRow);
                        }
                        cycle++;
                        if (cycle == 40)
                        {
                            crtRow++;
                            cycle = 0;
                            sprite.MoveTo(cpu.X);
                        }

                        if (sprite.IsVisibleAt(cycle))
                        {
                            cRT.DrawPixel(cycle, crtRow);
                        }
                        cycle++;
                        if (cycle == 40)
                        {
                            crtRow++;
                            cycle = 0;
                            sprite.MoveTo(cpu.X);
                        }

                        cpu.AddX(v);
                        sprite.MoveTo(cpu.X);
                    }
                }
                if(crtRow > 5)
                {
                    Console.Write("Part 2: ");
                    cRT.OutputScreen();
                }
            }

        }

    }

    public class CPU
    {
        public int X { get; set; }

        public CPU()
        {
            X = 1;
        }

        public void AddX(int v)
        {
            X += v;
        }

        public void Noop()
        {

        }
    }

    public class CRT
    {
        public string[] Screen { get; set; }

        public CRT()
        {
            Screen = new string[6];
            for (int i = 0; i < 6; i++)
            {
                Screen[i] = new string('.', 40);
            }
        }
        public void DrawPixel(int x, int y)
        {
            if (x >= 0 && x < 40 && y >= 0 && y < 6)
            {
                string row = Screen[y];
                string newRow = row.Substring(0, x) + "#" + row.Substring(x + 1);
                Screen[y] = newRow;
            }
        }
        public void OutputScreen()
        {
            Console.WriteLine("");
            for (int y = 0; y < 6; y++)
            {
                Console.WriteLine(Screen[y]);
            }
        }
    }

    public class Sprite
    {
        public int X { get; set; }

        public Sprite()
        {
            X = 0;
        }

        public void MoveTo(int x)
        {
            X = x;
        }

        public bool IsVisibleAt(int x)
        {
            return x >= X - 1 && x <= X + 1;
        }
    }
}

