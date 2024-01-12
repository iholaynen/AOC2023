using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC2023.Day3GearRatios
{
    internal class Day3GearRatios : AbstractPuzzle
    {
        private Dictionary<Coord, Neighbors> gears = new Dictionary<Coord, Neighbors>();
        private struct Coord
        {
            public int X { get; set; }
            public int Y { get; set; }

            public Coord(int x, int y)
            {
                X = x;
                Y = y;
            }

        }

        private class Neighbors
        {
            private List<int> nums = new List<int>();
            
            public void AddNeighbor(int neighbor)
            {
                if (nums.Count > 2)
                {
                    return;
                }
                nums.Add(neighbor);
            }

            public int GetGearRatio()
            {
                if (nums.Count == 2)
                {
                    return nums[0] * nums[1];
                }
                return 0;
            }
            
        }
        private AbstractPuzzle.PuzzleType _puzzleType;
        public Day3GearRatios(AbstractPuzzle.PuzzleType puzzleType)
        {
            this._puzzleType = puzzleType;
        }

        public override string Solve()
        {
            int total = 0;
            int curLineIndex = -2;
            string prevLine=string.Empty, curLine = string.Empty, nextLine = string.Empty;
            foreach (string line in Day3GearRatios.ReadLines(Day3GearRatios.GetStandardInput()))
            {
                total += iterateLines(ref curLineIndex, ref prevLine, ref curLine, ref nextLine, line);

            }
            total += iterateLines(ref curLineIndex, ref prevLine, ref curLine, ref nextLine, string.Empty);

            if (this._puzzleType == PuzzleType.Puzzle2)
            {
                var totalPuzzle2 = 0;
                foreach (Neighbors neighb in gears.Values)
                {
                    totalPuzzle2 += neighb.GetGearRatio();
                }
                return totalPuzzle2.ToString();
            }
            return total.ToString();
        }

        private int iterateLines(ref int curLineIndex, ref string prevLine, ref string curLine, ref string nextLine, string line)
        {
            curLineIndex++;
            prevLine = curLine;
            curLine = nextLine;
            nextLine = line;
            int lineTotal = 0;
            if (curLine != string.Empty)
            {
                string pattern = @"\d+";
                MatchCollection matches = Regex.Matches(curLine, pattern);
                foreach (Match match in matches)
                {
                    var gearCoord = new List<Coord>();
                    bool addToTotal = false;
                    //check char to left
                    if (match.Index>0 && curLine[match.Index - 1] != '.')
                    {
                        addToTotal= true;
                        if (curLine[match.Index - 1] == '*')
                        {
                            gearCoord.Add(new Coord(curLineIndex, match.Index - 1));
                        }
                    }
                    //check char to right
                    if (match.Index + match.Value.Length < curLine.Length && curLine[match.Index + match.Value.Length] != '.')
                    {
                        addToTotal = true;
                        if (curLine[match.Index + match.Value.Length] == '*')
                        {
                            gearCoord.Add(new Coord(curLineIndex, match.Index + match.Value.Length));
                        }
                    }
                    int startIndex = Math.Max(0, match.Index - 1);
                    int length = Math.Min(curLine.Length , match.Index + match.Value.Length + 1) - startIndex;
                    //check prev line
                    if ((IsSymbolAtLine(prevLine, startIndex, length, curLineIndex-1, gearCoord) || IsSymbolAtLine(nextLine, startIndex, length, curLineIndex + 1, gearCoord)))
                    {
                            addToTotal = true;
                    }
                    //check next line
                    if (addToTotal)
                    {
                        foreach (Coord gear in  gearCoord)
                        {
                            GetOrCreate(gears, gear, new Neighbors()).AddNeighbor(int.Parse(match.Value));
                            
                        }
                        lineTotal += int.Parse(match.Value);
                    }
                }
            }
            return lineTotal;
        }
        private bool IsSymbolAtLine(string line, int start, int length, int lineIndex, List<Coord> gearCoord)
        {
            if (line == string.Empty)
                return false;
            string pattern = @"\*";
            MatchCollection matches = Regex.Matches(line.Substring(start, length), pattern);
            foreach (Match match in matches)
            {
                gearCoord.Add(new Coord(lineIndex, match.Index + start));
            }
            return line.Substring(start, length) != new string('.', length);
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
