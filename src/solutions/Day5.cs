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
        
            Output = Part2(input);
        
            return Output;
        }

        public static long Part2(string[] input)
        {
            var StreamReader = new StreamReader(File.OpenRead("input-day5.txt"));
        
            // Because seeds are static input, read seeds input to an array
            string CurrentLine = StreamReader.ReadLine().Substring("seeds:".Length);
        
            long[] SeedInput = CurrentLine.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(long.Parse).ToArray();

            Console.WriteLine("Building list...");
            SeedRange[] Range = CalculateSeeds(SeedInput);
            List<long> SeedList = new List<long>();
            foreach (SeedRange ValuePair in Range)
            {
                long startingValue = ValuePair.BeginningRange;
                long endingRange = ValuePair.EndingRange;

                SeedList.AddRange(Enumerable.Range(0, (int)endingRange).Select(i => startingValue + i));
            }

            Console.WriteLine("List-building done");

            long[] Seeds = SeedList.ToArray();
            Console.WriteLine("Converted to array");
        
            StreamReader.ReadLine(); // Skip empty line before maps
        
            Console.WriteLine("Processing maps");
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

                Console.WriteLine("Processing seeds");
                Seeds = MapSeeds(MapList, Seeds);
            }
        
            return Seeds.Min();
        }

        public static long[] MapSeeds(List<ConversionMap> Maps, long[] input)
        {
            // long[] OutputValues = new long[input.Length];
            // Array.Copy(input, OutputValues, input.Length);

            HashSet<long> seedSet = new HashSet<long>(input);

            foreach (long seed in input)
            {
                bool mapped = false;
                foreach (ConversionMap map in Maps)
                {
                    // Console.WriteLine($"Mapping seed {seed}...");
                    if (seedSet.Contains(seed) && map.ValidRange(seed))
                    {
                        int index = Array.IndexOf(input, seed);
                        input[index] = map.MapValue(seed);
                        mapped = true;
                        break;  // Break out of the inner loop once mapping is done
                    }
                }
                if (!mapped)
                {
                    int index = Array.IndexOf(input, seed);
                    input[index] = seed;
                }
            }

            return input;
        }

        public static SeedRange[] CalculateSeeds(long[] inputArray)
        {
            // Create an array to store SeedRange instances
            SeedRange[] seedRanges = new SeedRange[inputArray.Length / 2];

            // Populate SeedRange instances
            for (int i = 0; i < inputArray.Length; i += 2)
            {
                seedRanges[i / 2] = new SeedRange(inputArray[i], inputArray[i + 1]);
            }

            // Output SeedRanges
            return seedRanges;
        }

        public class SeedRange
        {
            public long BeginningRange { get; }
            public long EndingRange { get; }

            public SeedRange (long BeginningRange, long EndingRange)
            {
                this.BeginningRange = BeginningRange;
                this.EndingRange = EndingRange;
            }
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

            public bool ValidRange(long input) => input >= SourceStart && input < (SourceStart + Range);

            public long MapValue(long input) => this.DestStart + (input - this.SourceStart); // Map input value to corresponding map value
        }
    }
}
