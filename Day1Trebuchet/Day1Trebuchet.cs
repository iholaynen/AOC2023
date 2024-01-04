using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC2023.Day1Trebuchet
{
    internal class Day1Trebuchet : AbstractPuzzle
    {

        private string m_pattern;
        private static readonly Dictionary<string, int> m_digits = new Dictionary<string, int>
        {
            { "1", 1 },
            { "2", 2 },
            { "3", 3 },
            { "4", 4 },
            { "5", 5 },
            { "6", 6 },
            { "7", 7 },
            { "8", 8 },
            { "9", 9 },
            { "0", 0 },
            { "one", 1 },
            { "two", 2 },
            { "three", 3 },
            { "four", 4 },
            { "five", 5 },
            { "six", 6 },
            { "seven", 7 },
            { "eight", 8 },
            { "nine", 9 },
            { "zero", 0 }
        };

        public Day1Trebuchet(AbstractPuzzle.PuzzleType day1Type) { 
            switch (day1Type)
            {
                case PuzzleType.Puzzle1:
                    m_pattern = "[0-9]";
                    break;
                case PuzzleType.Puzzle2:
                    m_pattern = "([0-9]|one|two|three|four|five|six|seven|eight|nine|zero)";
                    break;

            }
        }
        public override string Solve()
        {
            int total = 0;
            foreach (string line in Day1Trebuchet.ReadLines(Day1Trebuchet.GetStandardInput()))
                total += this.calcValue(line);
            return total.ToString();
        }

        private int calcValue(string line)
        {
            int first = m_digits[Regex.Match(line, m_pattern).Value];
      
            int last = m_digits[Regex.Match(line, m_pattern, RegexOptions.RightToLeft).Value]; 
            return first * 10 + last;
        }

    }
}
