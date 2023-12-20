using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.src.solutions
{
    public class Day7
    {
        const string SortOrder = "23456789TJQKA"; // Order of rank for cards

        // Puzzle entry point
        public static long Puzzle(string[] input, bool part2 = false)
        {
            CardType Hand = CardType.None;
            List<CardHand> CardHandList = new();

            foreach (string CardHand in input)
            {
                string[] SplitHand = CardHand.Split(' ');
                string Card = SplitHand[0];
                int Bet = int.Parse(SplitHand[1]);

                CardHand cardCounts = CountCharacters(Card);

                cardCounts.Bet = Bet;
                cardCounts.Card = Card;

                if (cardCounts.CardCount[0].Count == 5) { Hand = CardType.Five; }
                else if (cardCounts.CardCount[0].Count == 5 || cardCounts.CardCount[1].Count == 4) { Hand = CardType.Four; }
                else if (cardCounts.CardCount[0].Count == 2 && cardCounts.CardCount[1].Count == 3) { Hand = CardType.Full; }
                else if (cardCounts.CardCount[0].Count == 3 && cardCounts.CardCount[1].Count == 2) { Hand = CardType.Full; }
                else if (cardCounts.CardCount.Any(Card => Card.Count == 3)) { Hand = CardType.Three; }
                else if (cardCounts.CardCount[0].Count == 2 && cardCounts.CardCount[1].Count == 2) { Hand = CardType.Two; }
                else if (cardCounts.CardCount.Any(CardCount => CardCount.Count == 2)) { Hand = CardType.One; }
                else { Hand = CardType.High; }

                cardCounts.CardType = Hand;
                CardHandList.Add(cardCounts);
                Hand = CardType.None;
            }

            CustomSortOrder customComparer = new CustomSortOrder();

            // Order the list in descending order based on CardType
            var orderedList = CardHandList.OrderByDescending(cardHand => cardHand.CardType).ThenBy(cardHand => cardHand.Card, new CustomSortOrder()).ToList();

            int CurrentRank = 1;
            // Iterate through the ordered list in reverse
            foreach (var cardHand in orderedList)
            {
                // Your processing logic here
                cardHand.Rank = CurrentRank;
                Console.WriteLine($"CardType: {cardHand.CardType}, Card: {cardHand.Card}, Rank: {cardHand.Rank}");
                CurrentRank++;
            }

            int Output = 0;

            foreach (var card in CardHandList)
            {
                int AmountWon = card.Rank * card.Bet;
                Output += AmountWon;
            }

            return Output; // 254247632 is too high
        }

        static CardHand CountCharacters(string input)
        {
            // Create a list to store CardCount objects
            CardHand CurrentHand = new CardHand();
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
            CurrentHand.CardCount = cardCounts;

            return CurrentHand;
        }

        class CustomSortOrder : IComparer<string>
        {
            private static readonly string CustomOrder = "23456789TJQKA";

            public int Compare(string x, string y)
            {
                int minLength = Math.Min(x.Length, y.Length);

                for (int i = 0; i < minLength; i++)
                {
                    int xIndex = CustomOrder.IndexOf(x[i]);
                    int yIndex = CustomOrder.IndexOf(y[i]);

                    if (xIndex < yIndex)
                    {
                        return -1;
                    }
                    else if (xIndex > yIndex)
                    {
                        return 1;
                    }
                }

                return x.Length.CompareTo(y.Length);
            }
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
        public List<CardCount> CardCount { get; set; }
        public CardType CardType { get; set; }
        public int Bet { get; set; }
        public int Rank { get; set; }
    }

    public enum CardType { Five, Four, Full, Three, Two, One, High, None }
}
