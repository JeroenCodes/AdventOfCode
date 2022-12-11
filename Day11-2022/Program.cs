

using System.Numerics;
using static Program.MonkeyBusiness;

internal class Program
{
    public class MonkeyBusiness
    {
        private static void Main(string[] args)
        {  
            Part1(CreateMonkeys(false));
            static void Part1(Monkey[] monkeys)
            {
                for (int round = 0; round < 20; round++)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Monkey monkey = monkeys[i];
                        for (int item = 0; item < monkey.StartingItems.Count; item++)
                        {
                            long result = ItemSwitch((int)monkey.StartingItems[item], i, false);
                            monkey.Inspections++;
                            var postInspection = (int)result / 3;

                            int target = TargetSwitch(postInspection, i, monkey, false);
                            monkeys[target].ReceiveItem(postInspection);
                        }
                        monkey.ClearList();
                    }
                }
                var topList = monkeys.Select(s => s.Inspections).OrderDescending().Take(2);
                Console.WriteLine(topList.First() * topList.Last());
            }

            Part2(CreateMonkeys(false));
            static void Part2(Monkey[] monkeys)
            {

                var globalDivider = monkeys.Select(s => s.TestValue).Aggregate((m, i) => m * i);
                int lastRound = 0;
                for (int round = 1; round <= 10000; round++)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Monkey monkey = monkeys[i];
                        for (int item = 0; item < monkey.StartingItems.Count; item++)
                        {
                            long result = ItemSwitch(monkey.StartingItems[item], i, false);
                            monkey.Inspections++;
                            var postInspection = result % globalDivider;
                            int target = TargetSwitch((int)postInspection, i, monkey, false);

                            monkeys[target].ReceiveItem(postInspection);
                        }
                        monkey.ClearList();
                    }
                    lastRound = round;
                }
                var topList = monkeys.Select(s => s.Inspections).OrderDescending().Take(2);
                var sum = topList.First() * topList.Last();
                Console.WriteLine(sum);

            }
            Console.WriteLine("Test cases");
            Part1Test(CreateMonkeys(true));
            static void Part1Test(Monkey[] monkeys)
            {
                for (int round = 0; round < 20; round++)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Monkey monkey = monkeys[i];
                        for (int item = 0; item < monkey.StartingItems.Count; item++)
                        {
                            long result = ItemSwitch((int)monkey.StartingItems[item], i, true);
                            monkey.Inspections++;
                            var postInspection = (int)result / 3;
                            int target = TargetSwitch(postInspection, i, monkey, true);

                            monkeys[target].ReceiveItem(postInspection);
                        }
                        monkey.ClearList();
                    }
                }
                var topList = monkeys.Select(s => s.Inspections).OrderDescending().Take(2);
                Console.WriteLine(topList.First() * topList.Last());
            }

            Part2Test(CreateMonkeys(true));
            static void Part2Test(Monkey[] monkeys)
            {
                var globalDivider = monkeys.Select(s => s.TestValue).Aggregate((m, i) => m * i);
                for (int round = 1; round <= 10000; round++)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Monkey monkey = monkeys[i];
                        for (int item = 0; item < monkey.StartingItems.Count; item++)
                        {
                            long result = ItemSwitch(monkey.StartingItems[item], i, true);
                            monkey.Inspections++;
                            var postInspection = result % globalDivider;
                            int target = TargetSwitch((int)postInspection, i, monkey, true);

                            monkeys[target].ReceiveItem(postInspection);
                        }
                        monkey.ClearList();
                    }
                }
                var topList = monkeys.Select(s => s.Inspections).OrderDescending().Take(2);
                Console.WriteLine(topList.First() * topList.Last());

            }

            static Monkey[] CreateMonkeys(bool test)
            {
                if (test)
                {
                    Monkey[] testMonkeys = new Monkey[4];

                    testMonkeys[0] = new Monkey(new List<long>() { 79, 98 }, 2, 3, 23);
                    testMonkeys[1] = new Monkey(new List<long>() { 54, 65, 75, 74 }, 2, 0, 19);
                    testMonkeys[2] = new Monkey(new List<long>() { 79, 60, 97 }, 1, 3, 13);
                    testMonkeys[3] = new Monkey(new List<long>() { 74 }, 0, 1, 17);
                    return testMonkeys;
                }
                Monkey[] monkeys = new Monkey[8];
                monkeys[0] = new Monkey(new List<long>() { 53, 89, 62, 57, 74, 51, 83, 97 }, 1, 5, 13);
                monkeys[1] = new Monkey(new List<long>() { 85, 94, 97, 92, 56 }, 5, 2, 19);
                monkeys[2] = new Monkey(new List<long>() { 86, 82, 82 }, 3, 4, 11);
                monkeys[3] = new Monkey(new List<long>() { 94, 68 }, 7, 6, 17);
                monkeys[4] = new Monkey(new List<long>() { 83, 62, 74, 58, 96, 68, 85 }, 3, 6, 3);
                monkeys[5] = new Monkey(new List<long>() { 50, 68, 95, 82 }, 2, 4, 7);
                monkeys[6] = new Monkey(new List<long>() { 75 }, 7, 0, 5);
                monkeys[7] = new Monkey(new List<long>() { 92, 52, 85, 89, 68, 82 }, 0, 1, 2);
                return monkeys;
            }
        }
        private static long ItemSwitch(long item, int i, bool test)
        {
            long result = 0;
            if(test)
            {
                switch (i)
                {
                    case 0: result = (item * 19); break;
                    case 1: result = (item + 6); break;
                    case 2: result = (item * item); break;
                    case 3: result = (item + 3); break;
                    default: break;
                }return result;
            }
            switch (i)
            {
                case 0: result = (item * 3); break;
                case 1: result = (item + 2); break;
                case 2: result = (item + 1); break;
                case 3: result = (item + 5); break;
                case 4: result = (item + 4); break;
                case 5: result = (item + 8); break;
                case 6: result = (item * 7); break;
                case 7: result = (item * item); break;
                default: break;
            }
            return result;
        }

        private static int TargetSwitch(int postInspection, int i, Monkey monkey, bool test)
        {
            int target = 0;
            if(test)
            {
                switch (i)
                {
                    case 0: if (postInspection % monkey.TestValue == 0) { target = monkey.ThrowIfTrue; } else { target = monkey.ThrowIfFalse; } break;
                    case 1: if (postInspection % monkey.TestValue == 0) { target = monkey.ThrowIfTrue; } else { target = monkey.ThrowIfFalse; } break;
                    case 2: if (postInspection % monkey.TestValue == 0) { target = monkey.ThrowIfTrue; } else { target = monkey.ThrowIfFalse; } break;
                    case 3: if (postInspection % monkey.TestValue == 0) { target = monkey.ThrowIfTrue; } else { target = monkey.ThrowIfFalse; } break;
                    default: break;
                }return target;
            }
            switch (i)
            {
                case 0: if (postInspection % monkey.TestValue == 0) { target = monkey.ThrowIfTrue; } else { target = monkey.ThrowIfFalse; } break;
                case 1: if (postInspection % monkey.TestValue == 0) { target = monkey.ThrowIfTrue; } else { target = monkey.ThrowIfFalse; } break;
                case 2: if (postInspection % monkey.TestValue == 0) { target = monkey.ThrowIfTrue; } else { target = monkey.ThrowIfFalse; } break;
                case 3: if (postInspection % monkey.TestValue == 0) { target = monkey.ThrowIfTrue; } else { target = monkey.ThrowIfFalse; } break;
                case 4: if (postInspection % monkey.TestValue == 0) { target = monkey.ThrowIfTrue; } else { target = monkey.ThrowIfFalse; } break;
                case 5: if (postInspection % monkey.TestValue == 0) { target = monkey.ThrowIfTrue; } else { target = monkey.ThrowIfFalse; } break;
                case 6: if (postInspection % monkey.TestValue == 0) { target = monkey.ThrowIfTrue; } else { target = monkey.ThrowIfFalse; } break;
                case 7: if (postInspection % monkey.TestValue == 0) { target = monkey.ThrowIfTrue; } else { target = monkey.ThrowIfFalse; } break;
                default: break;
            }
            return target;
        }

        public class Monkey
        {
            public List<long> StartingItems { get; set; }
            public int ThrowIfTrue { get; set; }
            public int ThrowIfFalse { get; set; }
            public long Inspections { get; set; }
            public int TestValue { get; set; }

            public Monkey(List<long> startingItems, int throwIfTrue, int throwIfFalse, int testValue)
            {
                StartingItems = startingItems;
                ThrowIfTrue = throwIfTrue;
                ThrowIfFalse = throwIfFalse;
                TestValue = testValue;
            }

            public void ClearList()
            {
                StartingItems.RemoveAll(s => s.ToString() != null);
            }

            public void ReceiveItem(long item)
            {
                StartingItems.Add(item);
            }

        }
    }
}

