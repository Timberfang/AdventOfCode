using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.src.solutions
{
    public class Day5
    {
        const int MapCount = 6;

        public static long Puzzle(string[] input, bool part2)
        {
            long Output;

            if (part2) { Output = Part2(input); }
            else { Output = Part1(input); }

            return Output;
        }

        public static long Part1(string[] input)
        {
            var StreamReader = new StreamReader(File.OpenRead("input-day5.txt"));

            // Because seeds are static input, read seeds input to an array
            string CurrentLine = StreamReader.ReadLine().Substring("seeds:".Length);
            long[] Seeds = CurrentLine.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(long.Parse).ToArray();

            StreamReader.ReadLine(); // Skip empty line before maps

            for (int Iteration = 0; Iteration <= MapCount; Iteration++)
            {
                List<ConversionMap> MapList = new List<ConversionMap>();
                StreamReader.ReadLine(); // Skip label for each map
                CurrentLine = StreamReader.ReadLine();

                while (!string.IsNullOrEmpty(CurrentLine))
                {
                    long[] MapComponents = CurrentLine.Split(' ').Select(long.Parse).ToArray(); // Split map into components
                    MapList.Add(new ConversionMap(MapComponents[0], MapComponents[1], MapComponents[2])); // Build components into ConversionMap object
                    CurrentLine = StreamReader.ReadLine(); // Next line
                }

                Seeds = MapSeeds(MapList, Seeds);
            }

            return Seeds.Min();
        }

        public static int Part2(string[] input)
        {
            return 0;
        }

        public static long[] MapSeeds(List<ConversionMap> Maps, long[] input)
        {
            long[] OutputValues = input; // Input array is immutable due to foreach, so output is a separate array

            foreach (long seed in input)
            {
                bool Mapped = false; // Skip mapping if already mapped to prevent double-mapping
                foreach (ConversionMap Map in Maps)
                {
                    long Index = Array.IndexOf(OutputValues, seed);
                    if (Index != -1 && Map.ValidRange(seed) && !Mapped) // Array value should always exist, but this provides a failsafe in case it doesn't
                    {
                        OutputValues[Index] = Map.MapValue(seed);
                    }
                    else if (Index != -1 && !Mapped) { OutputValues[Index] = seed; }
                }
            }

            return OutputValues;
        }

        // Store maps
        public class ConversionMap
        {
            private long SourceStart;
            private long DestStart;
            private long Range;

            public ConversionMap(long DestStart, long SourceStart, long Range)
            {
                this.DestStart = DestStart;
                this.SourceStart = SourceStart;
                this.Range = Range;
            }

            public bool ValidRange(long input) // Check if the input is within the source range start and range length
            {
                if (input >= SourceStart && input < (SourceStart + Range)) { return true; }
                else return false;
            }

            public long MapValue(long input) // Map input value to corresponding map value
            {
                return (this.DestStart + (input - this.SourceStart));
            }
        }
    }
}
