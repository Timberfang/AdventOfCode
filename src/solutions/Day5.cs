using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.src.solutions
{
    public class Day5
    {
        const int MapCount = 6; // Count of input maps - this is always the same in the dataset

        public static long Puzzle(string inputPath, bool part2)
        {
            long Output;

            if (part2) { Output = Part2(inputPath); }
            else { Output = Part1(inputPath); }

            return Output;
        }

        // Part 1 solution
        public static long Part1(string inputPath)
        {
            var StreamReader = new StreamReader(File.OpenRead(inputPath));
            string CurrentLine = StreamReader.ReadLine().Substring("seeds:".Length);

            // Because seeds are static input, read seeds input to an array
            long[] SeedInput = CurrentLine.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(long.Parse).ToArray();

            StreamReader.ReadLine(); // Skip empty line before maps

            List<ConversionMap[]> MapList = new List<ConversionMap[]>(); // Stores arrays of values for each type of map
            long StoredValue = long.MaxValue; // Default to MaxValue so that the first value of output is always lower

            for (int Iteration = 0; Iteration <= MapCount; Iteration++)
            {
                List<ConversionMap> MapType = new List<ConversionMap>();
                StreamReader.ReadLine(); // Skip label for each map
                CurrentLine = StreamReader.ReadLine();

                while (!string.IsNullOrEmpty(CurrentLine))
                {
                    long[] MapComponents = CurrentLine.Split(' ').Select(long.Parse).ToArray(); // Split map into components
                    MapType.Add(new ConversionMap(MapComponents[0], MapComponents[1], MapComponents[2])); // Build components into ConversionMap object
                    CurrentLine = StreamReader.ReadLine(); // Next line
                }
                MapList.Add(MapType.ToArray());
            }

            Parallel.ForEach(SeedInput, SeedValue =>
            {
                foreach (ConversionMap[] mapArray in MapList) // Run each map across the seed value
                {
                    SeedValue = MapSeed(mapArray, SeedValue);
                }
                if (SeedValue < Interlocked.Read(ref StoredValue)) // If SeedValue is less than the stored value, then the SeedValue becomes the new StoredValue - Interlocked should be used for parallel-safe processing, apparently
                {
                    Interlocked.Exchange(ref StoredValue, SeedValue);
                }
            });

            return StoredValue;
        }

        // Part 2 solution
        public static long Part2(string inputPath)
        {
            var StreamReader = new StreamReader(File.OpenRead(inputPath));
            string CurrentLine = StreamReader.ReadLine().Substring("seeds:".Length);

            // Because seeds are static input, read seeds input to an array
            long[] SeedInput = CurrentLine.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(long.Parse).ToArray();

            StreamReader.ReadLine(); // Skip empty line before maps

            SeedRange[] Range = CalculateSeeds(SeedInput); // Convert input seeds into beginning and ending point pairs
            List<ConversionMap[]> MapList = new List<ConversionMap[]>(); // Stores arrays of values for each type of map
            long StoredValue = long.MaxValue;

            for (int Iteration = 0; Iteration <= MapCount; Iteration++)
            {
                List<ConversionMap> MapType = new List<ConversionMap>();
                StreamReader.ReadLine(); // Skip label for each map
                CurrentLine = StreamReader.ReadLine();

                while (!string.IsNullOrEmpty(CurrentLine))
                {
                    long[] MapComponents = CurrentLine.Split(' ').Select(long.Parse).ToArray(); // Split map into components
                    MapType.Add(new ConversionMap(MapComponents[0], MapComponents[1], MapComponents[2])); // Build components into ConversionMap object
                    CurrentLine = StreamReader.ReadLine(); // Next line
                }
                MapList.Add(MapType.ToArray());
            }

            Parallel.ForEach(Range, ValuePair =>
            {
                long startingValue = ValuePair.BeginningPoint; // BeginningPoint is the start of the range
                long endingValue = ValuePair.EndingPoint; // EndingPoint is the range - that is, the values to add to the BeginningPoint

                for (int Iteration = 0; Iteration <= endingValue; Iteration++)
                {
                    long SeedValue = startingValue + Iteration;
                    foreach (ConversionMap[] mapArray in MapList) // Run each map across the seed value
                    {
                        SeedValue = MapSeed(mapArray, SeedValue);
                    }
                    if (SeedValue < Interlocked.Read(ref StoredValue)) // If SeedValue is less than the stored value, then the SeedValue becomes the new StoredValue - Interlocked should be used for parallel-safe processing, apparently
                    {
                        Interlocked.Exchange(ref StoredValue, SeedValue);
                    }
                }
            });

            return StoredValue;
        }

        // Process seeds through the conversion maps
        public static long MapSeed(ConversionMap[] Maps, long input)
        {
            long output = input;
            for (int i = 0; i < Maps.Length; i++)
            {
                if (Maps[i].ValidRange(input))
                {
                    output = Maps[i].MapValue(input);
                    break;  // Break out of the loop once mapping is done
                }
            }
            return output;
        }

        // Calculate beginning and ending ranges for seed values for part 2
        public static SeedRange[] CalculateSeeds(long[] inputArray)
        {
            // Create an array to store SeedRange instances
            SeedRange[] seedRanges = new SeedRange[inputArray.Length / 2]; // Split array into two - even numbered values are the beginning points, odd numbered values are the ranges

            // Populate SeedRange instances
            for (int i = 0; i < inputArray.Length; i += 2)
            {
                seedRanges[i / 2] = new SeedRange(inputArray[i], inputArray[i + 1]);
            }

            // Output SeedRanges
            return seedRanges;
        }

        // Store beginning & ending values of seed ranges for part 2
        public struct SeedRange
        {
            public long BeginningPoint { get; }
            public long EndingPoint { get; }

            public SeedRange (long BeginningPoint, long EndingPoint)
            {
                this.BeginningPoint = BeginningPoint;
                this.EndingPoint = EndingPoint;
            }
        }

        // Store conversion maps
        public struct ConversionMap
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