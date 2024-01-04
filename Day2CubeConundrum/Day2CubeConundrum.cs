using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC2023.Day2CubeConundrum
{
    internal class Day2CubeConundrum : AbstractPuzzle
    {
        //Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
        private static string _patternLine = "Game\\s(?<game>\\d+):\\s(?<tuples>.*)";
        private static string _patternTuple = @"(\d+) (red|blue|green)";
        public override string Solve()
        {
            int total = 0;
            foreach (string line in Day2CubeConundrum.ReadLines(Day2CubeConundrum.GetStandardInput()))
                total += this.evaluateGame(line);
            return total.ToString();
        }

        private int evaluateGame(string line)
        {
            Match gameMatch = Regex.Match(line, _patternLine);
            int gameNum = int.Parse(gameMatch.Groups["game"].Value);

            string[] tuples = gameMatch.Groups["tuples"].Value.Split(';');
            int maxRed = 0;
            int maxGreen = 0;
            int maxBlue = 0;
            foreach (string tuple in tuples)
            {
                MatchCollection matches = Regex.Matches(tuple, _patternTuple);
                foreach (Match match in matches)
                {
                    int count = int.Parse(match.Groups[1].Value);
                    string color = match.Groups[2].Value;
                    switch (color)
                    {
                        case "red":
                            maxRed = Math.Max(maxRed, count);
                            break;
                        case "green":
                            maxGreen = Math.Max(maxGreen, count);
                            break;
                        case "blue":
                            maxBlue = Math.Max(maxBlue, count);
                            break;
                    }
                }
            }

            return (12>=maxRed && 13>=maxGreen && 14>=maxBlue)?gameNum:0;
        }
    }
}
