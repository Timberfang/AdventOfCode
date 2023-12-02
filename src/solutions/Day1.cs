using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions
{
    public class Day1
    {
        public static int Puzzle(string[] Input)
        {
            int CalibrationOutput = 0; // Initialize for later use
            foreach (string CalibrationValue in Input)
            {
                int Calibration = WordToNumber(CalibrationValue, true); // Convert "one" to "1", "two" to "2", and so on
                
                // Give output and add to the total
                File.AppendAllText(@".\log.txt", $"Input {CalibrationValue} gives output {Calibration}!" + Environment.NewLine);
                CalibrationOutput += Calibration;
            }

            return CalibrationOutput;
        }


        private static int WordToNumber(string Words, bool ConvertWords = true)
        {
            string Output = null;
            Dictionary<string, string> WordNumbers = new Dictionary<string, string>
            {
                { "zero", "0" },
                { "one", "1" },
                { "two", "2" },
                { "three", "3" },
                { "four", "4" },
                { "five", "5" },
                { "six", "6" },
                { "seven", "7" },
                { "eight", "8" },
                { "nine", "9" },
                { "ten", "10" },
                { "eleven", "11" },
                { "twelve", "12" },
                { "thirteen", "13" },
                { "fourteen", "14" },
                { "fifteen", "15" },
                { "sixteen", "16" },
                { "seventeen", "17" },
                { "eighteen", "18" },
                { "nineteen", "19" },
                { "twenty", "20" },
            };

            // Create a regular expression pattern from the dictionary keys
            string WordSearchPattern;
            if (ConvertWords) { WordSearchPattern = string.Join("|", WordNumbers.Keys.Select(Regex.Escape)) + "|" + string.Join("|", WordNumbers.Values.Select(Regex.Escape)); }
            else { WordSearchPattern = string.Join("|", WordNumbers.Values.Select(Regex.Escape)); } // Don't search for wordforms if WordSearchPattern == false

            Regex FirstNumber = new Regex(WordSearchPattern);
            Regex LastNumber = new Regex(WordSearchPattern, RegexOptions.RightToLeft);

            // Match numbers
            Match FirstNumberMatch = FirstNumber.Match(Words);
            Match LastNumberMatch = LastNumber.Match(Words);

            if (WordNumbers.ContainsKey(FirstNumberMatch.Value) && ConvertWords)
            {
                Output += WordNumbers[FirstNumberMatch.Value];
            }
            else
            {
                Output += FirstNumberMatch.Value;
            }

            if (WordNumbers.ContainsKey(LastNumberMatch.Value) && ConvertWords)
            {
                Output += WordNumbers[LastNumberMatch.Value];
            }
            else
            {
                Output += LastNumberMatch.Value;
            }

            return int.Parse(Output);
        }
    }
}
