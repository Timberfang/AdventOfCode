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
            CardType Hand = CardType.None;

            foreach (string CardHand in input)
            {
                string[] SplitHand = CardHand.Split(' ');
                string Card = SplitHand[0];
                int Bet = int.Parse(SplitHand[1]);

                List<CardCount> cardCounts = CountCharacters(Card);

                if (cardCounts[0].Count == 5) { Hand = CardType.Five; }
                else if (cardCounts[0].Count == 5 || cardCounts[1].Count == 4) { Hand = CardType.Four; }
                else if (cardCounts[0].Count == 2 && cardCounts[1].Count == 3) { Hand = CardType.Full; }
                else if (cardCounts[0].Count == 3 && cardCounts[1].Count == 2) { Hand = CardType.Full; }
                else if (cardCounts.Any(Card => Card.Count == 3)) { Hand = CardType.Three; }
                else if (cardCounts[1].Count == 2 && cardCounts[1].Count == 2) { Hand = CardType.Two; }
                else if (cardCounts.Any(Card => Card.Count == 2)) { Hand = CardType.One; }
                else { Hand = CardType.High; }
                
                Console.WriteLine($"Card: {Card}. Card Type: {Hand}. Bet: {Bet}");

                cardCounts.Clear(); // Clear list at loop end
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
                    // Increment the count if the Card is present
                    existingCardCount.Count++;
                }
                else
                {
                    // Add a new CardCount object if the Card is not present
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

    public class CardHand
    {
        public string Card { get; set; }
        public CardType CardType { get; set; }
        public int Bet { get; set; }
    }

    public enum CardType { Five, Four, Full, Three, Two, One, High, None }
}
