namespace AdventOfCode.src.solutions
{
    public class Day7
    {
        const string SortOrder = "23456789TJQKA"; // Order of rank for cards

        public static long Puzzle(string[] input, bool part2 = false)
        {
            List<CardHand> HandList = new();

            foreach (string CardHand in input)
            {
                string[] SplitHand = CardHand.Split(' ');
                HandList.Add(new CardHand(SplitHand[0], int.Parse(SplitHand[1])));
            }

            // Order the list in descending order based on CardType
            var orderedList = HandList.OrderByDescending(cardHand => cardHand.Type).ThenBy(cardHand => cardHand.Hand, new CustomSortOrder(SortOrder)).ToList();

            int CurrentRank = 1;
            // Iterate through the ordered list in reverse
            foreach (var cardHand in orderedList)
            {
                // Your processing logic here
                cardHand.Rank = CurrentRank;
                Console.WriteLine($"CardType: {cardHand.Type}, Card: {cardHand.Hand}, Rank: {cardHand.Rank}, Bid: {cardHand.Bet}");
                CurrentRank++;
            }

            int Output = 0;

            foreach (var card in HandList)
            {
                int AmountWon = card.Rank * card.Bet;
                Output += AmountWon;
            }

            return Output; // 254247632 is too high

        }
    }

    class CustomSortOrder : IComparer<string>
    {
        private string CustomOrder;

        public CustomSortOrder(string CustomOrder)
        {
            this.CustomOrder = CustomOrder;
        }

        public int Compare(string? x, string? y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            int minLength = Math.Min(x.Length, y.Length);

            for (int i = 0; i < minLength; i++)
            {
                int xIndex = CustomOrder.IndexOf(x[i]);
                int yIndex = CustomOrder.IndexOf(y[i]);

                if (xIndex < yIndex) { return -1; }
                else if (xIndex > yIndex) { return 1; }
            }

            return x.Length.CompareTo(y.Length);
        }
    }

    public class CardCount
    {
        public char Card { get; set; }
        public int Count { get; set; }

    }

    public class CardHand
    {
        public string Hand { get; init; }
        public List<CardCount> Cards { get { return CountCharacters(this); } }
        public CardType Type { get { return GetType(this); } }
        public int Bet { get; init; }
        public int Rank { get; set; }

        public CardHand(string Hand, int Bet, int Rank = 0)
        {
            this.Hand = Hand;
            this.Bet = Bet;
            this.Rank = Rank;
        }

        private static List<CardCount> CountCharacters(CardHand input)
        {
            // Create a list to store CardCount objects
            List<CardCount> CardCount = new();

            // Iterate through each character in the string
            foreach (char InputCard in input.Hand)
            {
                // Check if the character is already in the list
                CardCount? ExistingCardCount = CardCount.FirstOrDefault(x => x.Card == InputCard);

                if (ExistingCardCount != null) { ExistingCardCount.Count++; }
                else { CardCount.Add(new CardCount { Card = InputCard, Count = 1 }); }
            }

            return CardCount;
        }

        private static CardType GetType(CardHand input)
        {
            CardType OutputType = input.Cards.Select(c => c.Count).OrderByDescending(count => count).First() switch
            {
                5 => CardType.Five,
                4 => CardType.Four,
                3 => input.Cards.Any(c => c.Count == 2) ? CardType.Full : CardType.Three,
                2 => input.Cards.Where(c => c.Count == 2).Count() == 2 ? CardType.Two : CardType.One,
                _ => CardType.High,
            };
            return OutputType;
        }
    }

    public enum CardType { Five, Four, Full, Three, Two, One, High, None }
}