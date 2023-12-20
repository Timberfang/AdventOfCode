namespace AdventOfCode.src.solutions
{
    public class Day7
    {
        const string SortOrder = "23456789TJQKA"; // Order of rank for cards

        public static long Puzzle(string[] input, bool part2 = false)
        {
            return 0;
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
        public CardType Type { get; set; }
        public int Bet { get; set; }
        public int Rank { get; set; }

        public CardHand(string Card, CardType Type, int Bet, int Rank = 0)
        {
            this.Card = Card;
            this.Type = Type;
            this.Bet = Bet;
            this.Rank = Rank;
        }
    }

    public enum CardType { Five, Four, Full, Three, Two, One, High, None }
}