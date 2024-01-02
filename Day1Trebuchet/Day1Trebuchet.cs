using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Day1Trebuchet
{
    internal class Day1Trebuchet : AbstractPuzzle
    {
        public override string Solve()
        {
            int total = 0;
            foreach (string line in Day1Trebuchet.ReadLines(Day1Trebuchet.GetStandardInput()))
                total += this.calcValue(line);
            return total.ToString();
        }

        private int calcValue(string line)
        {
            int first = line.FirstOrDefault(Char.IsDigit) - '0';
            int last = line.LastOrDefault(Char.IsDigit) - '0';
            return first * 10 + last;
        }

    }
}
