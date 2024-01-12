using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace AOC2023
{
    internal abstract class AbstractPuzzle
    {
        public enum PuzzleType { Puzzle1, Puzzle2 }
        public abstract string Solve();
        internal static IEnumerable<string> ReadLines(string path)
        {
            using (StreamReader file = new StreamReader(path))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }
        internal static string GetStandardInput()
        {
            string directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return  Path.Combine(directory, "data.txt");
        }
        public static TValue GetOrCreate<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
        {
            if (!dictionary.TryGetValue(key, out TValue value))
            {
                value = defaultValue;
                dictionary[key] = value;
            }

            return value;
        }
    }
}
