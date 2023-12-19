using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.src.solutions
{
    public class Day7
    {
        // Puzzle entry point
        public static long Puzzle(string[] input, bool part2 = false)
        {
            foreach (string card in input)
            {
                List<CardCount> cardCounts = CountCharacters(card);

                // Display the results using the CardCount class
                foreach (var cardCount in cardCounts)
                {
                    Console.WriteLine($"{cardCount.Card}: {cardCount.Count}");
                }
            }

            return 0;
        }

        static List<CardCount> CountCharacters(string input)
        {
            // Create a list to store CardCount objects
            List<CardCount> cardCounts = new List<CardCount>();

            // Iterate through each character in the string
            foreach (char c in input)
            {
                // Check if the character is already in the list
                CardCount existingCardCount = cardCounts.FirstOrDefault(x => x.Card == c);

                if (existingCardCount != null)
                {
                    // Increment the count if the card is present
                    existingCardCount.Count++;
                }
                else
                {
                    // Add a new CardCount object if the card is not present
                    cardCounts.Add(new CardCount { Card = c, Count = 1 });
                }
            }

            return cardCounts;
        }
    }

    public class CardCount
    {
        public char Card { get; set; }
        public int Count { get; set; }
    }
}
