//using System.Text.RegularExpressions;

//string InputFilePath = "/Users/jbijl005/Documents/test.txt";
//var input = File.ReadAllText(InputFilePath).Split("\r\n");
//IDictionary<string, List<int>> directories = new Dictionary<string, List<int>>();
//IDictionary<string, Dictionary<string, List<string>>> parentDir = new Dictionary<string, Dictionary<string, List<string>>>();
//IDictionary<string, List<string>> directory = new Dictionary<string, List<string>>();
//var currentDir = "/";



//var inputList = input.ToList();


//for(int v = 0; v < input.Length; v++)
//{
//    if (input[v].StartsWith("$ cd"))
//    {
//        var dirName = input[v].Remove(0, 5);
//        if (!input[v].StartsWith("$ cd .."))
//        {

//            if (!parentDir.ContainsKey(currentDir) && currentDir == "/")
//            {
//                parentDir.Add(currentDir, new Dictionary<string, List<string>>());
//            }
//            if (!parentDir.ContainsKey(currentDir))
//            {
//                var newDir = new Dictionary<string, List<string>>() { {dirName, new List<string>()}};
//                parentDir.Add(currentDir, newDir);
//            }
//            else
//            {
//                var parent = parentDir.Where(s => s.Key == currentDir).FirstOrDefault();
//                parent.Value.Add(input[v].Remove(0, 5));
//            }
//        }

//        currentDir = input[v].Remove(0, 5);
//        continue;
//    }
//    //var directoryEntries = inputList.Skip(v).SkipWhile(s => s.StartsWith("$")).TakeWhile(s=>s.);
//    if (input[v].StartsWith("$ ls"))
//    {
//        continue;
//    }
//    if (char.IsDigit((char)input[v].Take(1).FirstOrDefault()))
//    {

//            var size = int.Parse(Regex.Match(input[v], @"^\d+").ToString()); ;

//            if(!directories.ContainsKey(currentDir))
//            {
//                directories.Add(currentDir, new List<int>() { size });

//        }
//        else
//        {
//            var dir = directories.Where(s => s.Key == currentDir).FirstOrDefault();
//            dir.Value.Add(size);

//        }

//     }
//}
//foreach (var pair in directories)
//{
//    if(parentDir.ContainsKey(pair.Key))
//    {
//        var subDirectories = parentDir.Where(s => s.Key == pair.Key).FirstOrDefault().Value;
//        if(subDirectories.Any())
//        {
//            Console.WriteLine(string.Join(", ", subDirectories));
//            foreach(string sub in subDirectories)
//            {
//                if (parentDir.ContainsKey(sub))
//                {
//                    var superSubs = parentDir.Where(s => s.Key == sub).FirstOrDefault().Value;
//                    if(superSubs.Any())
//                    {
//                        foreach(string superSub in superSubs)
//                        {
//                            if(parentDir.ContainsKey(superSub))
//                            {
//                            var ohMy = parentDir.Where(s => s.Key == superSub).FirstOrDefault().Value;
//                            if(ohMy.Any())
//                            {
//                                foreach(string ugh in ohMy)
//                                {
//                                        if (parentDir.ContainsKey(ugh))
//                                        {
//                                    var tooMany = parentDir.Where(s => s.Key == ugh).FirstOrDefault().Value;
//                                    if (tooMany.Any())
//                                    {
//                                        Console.WriteLine("just give up");
//                                    }
//                                        }

//                                }

//                            }
//                            }

//                        }

//                    }
//                }


//            }
//        }
//    }

//}

////var smallDirectories = directories.Where(s => s.Value.Sum() < 100000);



////var sumSmallDirectories = directories.Where(s => s.Value.Sum() < 100000).Sum(s => s.Value.Sum());
////foreach (var pair in smallDirectories)
////{
////    Console.WriteLine(pair.Key + " " + pair.Value.Sum());
////}
////Console.WriteLine(sumSmallDirectories);
///using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class Program
{
    public class Directory
    {
        public string name { get; set; }
        public int size { get; set; }
        public List<Directory> subDir { get; set; }
        public Directory parentDir { get; set; }
        public Dictionary<string, int> files { get; set; }


        public Directory(string dirName, Directory parent)
        {
            name = dirName;
            size = 0;
            subDir = new List<Directory>();
            parentDir = parent;
            files = new Dictionary<string, int>();

        }
    }
    public static class FileSystemCleanup
    {

        static string[] input = File.ReadAllText("/Users/jbijl005/Documents/test.txt").Split("\r\n");

        static Directory TheShabang = new Directory("/", null);
        static List<int> dirSizes = new List<int>();
        static int sumSmallDir = 0;

        private static void Main(string[] args)
        {
            Directory currentDir = null;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i].StartsWith("$ cd"))
                {
                    string dirToChangeTo = input[i].Substring(5);
                    if (dirToChangeTo == "/")
                    {
                        currentDir = TheShabang;
                    }
                    if (dirToChangeTo == "..")
                    {
                        currentDir = currentDir.parentDir;
                    }
                    else
                    {
                        currentDir = currentDir.subDir.Find(x => x.name == dirToChangeTo);
                    }

                }
                else if (input[i] == "$ ls")
                {
                    int inputLine = i + 1;
                    if (inputLine <= input.Length - 1)
                    {
                        while (!input[inputLine].Contains("$"))
                        {
                            if (input[inputLine].Contains("dir "))
                            {
                                string dirName = input[inputLine].Substring(4);
                                if (!currentDir.subDir.Contains(currentDir.subDir.Find(x => x.name == dirName)))
                                {
                                    currentDir.subDir.Add(new Directory(dirName, currentDir));
                                }
                            }
                            else
                            {
                                string[] file = input[inputLine].Split();
                                if (file.Length > 1)
                                { currentDir.files.Add(file[1], int.Parse(file[0])); }

                            }
                            inputLine++;
                            if (inputLine == input.Length)
                            {
                                break;
                            }
                        }
                    }
                    i = inputLine - 1;
                }
            }
            CheckDir(TheShabang);
            Console.WriteLine(sumSmallDir);

            //part two
            int totalSpace = 70000000;
            int update = 30000000;
            int available = totalSpace - TheShabang.size;
            int neededSpace = update - available;
            int toDelete = 70000000;

            foreach (int i in dirSizes)
            {
                if (i >= neededSpace && i <= toDelete)
                {
                    toDelete = i;
                }
            }
            Console.WriteLine(toDelete);
        }

        public static int CheckDir(Directory directory)
        {

            if (directory.files.Count > 0)
            {
                foreach (KeyValuePair<string, int> pair in directory.files)
                {
                    directory.size += pair.Value;
                }
            }

            if (directory.subDir.Count > 0)
            {
                foreach (Directory subD in directory.subDir)
                {
                    directory.size += CheckDir(subD);
                }
            }

            if (directory.size <= 100000)
            {
                sumSmallDir += directory.size;
            }

            dirSizes.Add(directory.size);

            return directory.size;
        }
    }
}
