using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC2023.Day4Scratchcards
{
    internal class Day4Scratchcards : AbstractPuzzle
    {
        private AbstractPuzzle.PuzzleType _puzzleType;
        public Day4Scratchcards(AbstractPuzzle.PuzzleType puzzleType)
        {
            this._puzzleType = puzzleType;
        }

        public override string Solve()
        {
            int totalPuzzle1 = 0;
            int totalPuzzle2 = 0;
            Dictionary<int, int> copies = new Dictionary<int, int>();
            var currentCard = 1;
            
            foreach (string line in Day4Scratchcards.ReadLines(Day4Scratchcards.GetStandardInput()))
            {
                copies[currentCard] = GetOrCreate(copies, currentCard, 0) + 1;
                var matchingNumbers = calculateCard(line);
                totalPuzzle1 += matchingNumbers > 0 ? (int)Math.Pow(2, matchingNumbers - 1) : 0;
                for (int i = currentCard + 1; i< currentCard + matchingNumbers + 1; i++){
                    copies[i] = GetOrCreate(copies,i, 0) + copies[currentCard];
                }
                totalPuzzle2 += copies[currentCard];
                currentCard++;
            }
            
            return this._puzzleType==PuzzleType.Puzzle1? totalPuzzle1.ToString():totalPuzzle2.ToString();
        }

        private int calculateCard(string line)
        {
            var total = 0;
            var pattern = @"(\d[\d ]*)\s*\|\s*(\d[\d ]*)";
            var match = Regex.Match(line, pattern);
            if (match.Success)
            {
                var winNums = new HashSet<string>();
                foreach (var win in match.Groups[1].Value.Split(' ') )
                {
                    if (win.Length>0)
                    {
                        winNums.Add(win);
                    }                    
                }
                foreach (var num in match.Groups[2].Value.Split(' '))
                {
                    if (winNums.Contains(num))
                    {
                        total++;
                    }
                }
            }
            return total;
        }
    }
}
