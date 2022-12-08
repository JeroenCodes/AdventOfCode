using MoreLinq;
internal class Program
{
    public static class TreeGrid
    {


        static string InputFilePath = "/Users/jbijl005/Documents/test.txt";
        static string[] input = File.ReadAllText(InputFilePath).Split("\r\n");

        private static void Main(string[] args)
        {
            var arrayLenght = input.Length;
            List<int>[] grid = new List<int>[1] { GetTrees(input[0])};
            for( int x = 1; x < input.Length; x++)
            {
                var trees = GetTrees(input[x]);
                if(trees.Any())
                {
                    Array.Resize<List<int>>(ref grid, x + 1);

                    grid[x] = trees;
                }             
            }
            int visibleTrees = 0;
            int scenicTop = 0;

            for (int i = 0; i < grid.Length; i++)
            {
                if (i == 0 || i == grid.Length-1)
                {
                    visibleTrees += grid[i].Count;
                    continue;
                }

                for (int t = 0; t < grid[i].Count; t++)
                {
                    if (t == 0 || t == grid[i].Count) {
                        visibleTrees++; }

                    else
                    {
                        var visible = CheckVisible(grid[i][t], AboveTrees(i, t+1), Below(i, t+1, grid.Length), grid[i].Take(t).ToList(), grid[i].Skip(t+1).Take(grid[i].Count - t).ToList());
                        if (visible) visibleTrees++;
                        var scenicScore = ScenicScore(grid[i][t], AboveTrees(i, t + 1), Below(i, t + 1, grid.Length), grid[i].Take(t).ToList(), grid[i].Skip(t + 1).Take(grid[i].Count - t).ToList());
                        if(scenicScore > scenicTop)
                        {
                            scenicTop = scenicScore;
                        }
                        continue;
                    }
                }
            }
            Console.WriteLine(visibleTrees);
            Console.WriteLine(scenicTop);
        }

        public static List<int> AboveTrees(int row, int index)
        {
            var trees = new List<int>();
            for (int i = row-1; i>= 0; i--)
            {
                var selection = GetTrees(input[i]);
                trees.Add(selection[index-1]);
            }
            return trees;
        }
        public static List<int> Below(int row, int index, int gridLength)
        {
            var trees = new List<int>();
            for (int i = row + 1; i <= gridLength-1; i++)
            {
                var selection = GetTrees(input[i]);
                trees.Add(selection[index-1]);
            }
            return trees;
        }

        public static bool CheckVisible(int tree, List<int> above, List<int> below, List<int> before, List<int> after)
        {
            if (!before.Any() || !after.Any() || !below.Any() || !above.Any()) return true;
            if (!above.Any(s => s >= tree) || !below.Any(s => s >= tree) || !before.Any(s => s >= tree) || !after.Any(s => s >= tree))
            {
                return true;
            }
            else return false;
        }

        public static List<int> GetTrees(string row)
        {
            var treesStrings = row.ToCharArray().ToList();
            var trees = new List<int>();
            foreach (char tree in treesStrings)
            {
                trees.Add((int)char.GetNumericValue(tree));
            }
            return trees;
        }

        public static int ScenicScore(int tree, List<int> above, List<int> below, List<int> before, List<int> after)
        {
            var aboveScore = above.TakeUntil(x => x >= tree).Count();
            var belowScore = below.TakeUntil(x => x >= tree).Count();
            before.Reverse();
            var beforeScore = before.TakeUntil(x => x >= tree).Count();
            var afterScore = after.TakeUntil(x => x >= tree).Count();

            var score = aboveScore * belowScore * beforeScore * afterScore;
            return score;
        }
    }
}