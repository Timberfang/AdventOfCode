using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions
{
    public class Day2
    {
        public static int Puzzle(string[] Input, int MaxRed, int MaxGreen, int MaxBlue)
        {
            if (File.Exists(@".\log.txt")) { File.Delete(@".\log.txt"); }

            int MatchTotal = 0;
            int NeededCubes = 0;
            foreach (string game in Input)
            {
                Regex MatchNumber = new Regex(@"Game \d*");
                Regex BlueGames = new Regex(@"\d* blue");
                Regex RedGames = new Regex(@"\d* red");
                Regex GreenGames = new Regex(@"\d* green");

                int Match = ExtractNumber(game, MatchNumber).First();
                int BlueCubes = ExtractNumber(game, BlueGames).Max();
                int RedCubes = ExtractNumber(game, RedGames).Max();
                int GreenCubes = ExtractNumber(game, GreenGames).Max();
                int MatchCubes = BlueCubes * RedCubes * GreenCubes;

                if (BlueCubes <= MaxBlue && RedCubes <= MaxRed && GreenCubes <= MaxGreen)
                {
                    File.AppendAllText(@".\log.txt", $"Game {Match} -  Blue Cubes: {BlueCubes}, Red Cubes: {RedCubes}, Green Cubes: {GreenCubes}" + Environment.NewLine);
                    File.AppendAllText(@".\log.txt", $"Conditions met, adding to sum..." + Environment.NewLine + Environment.NewLine);
                    MatchTotal += Match;
                    NeededCubes += MatchCubes;
                }
                else { File.AppendAllText(@".\log.txt", $"Conditions NOT met, skipping..." + Environment.NewLine + Environment.NewLine); }
            }

            return MatchTotal;
        }

        private static List<int> ExtractNumber(string Input, Regex Filter)
        {
            List<int> Count = new List<int>();
            Regex Numbers = new Regex("[^0-9]+");

            string[] Cubes = Filter.Matches(Input).OfType<Match>().Select(match => match.Value).ToArray();
            foreach (string Match in Cubes)
            {
                 Count.Add(int.Parse(Numbers.Replace(Match, string.Empty)));
            }

            return Count;
        }
    }
}
