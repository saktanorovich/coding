using System;
using System.Collections.Generic;

    class CompilingDecksWithJokers
    {
        private bool can(long decks, int[] cards, long jokers)
        {
            long need = 0;
            for (int i = 0; i < cards.Length; ++i)
            {
                if (cards[i] < decks)
                {
                    need += decks - cards[i];
                }
            }
            if (need > jokers)
            {
                return false;
            }
            if (need > decks)
            {
                return false;
            }
            return true;
        }
        public int maxCompleteDecks(int[] cards, int jokers)
        {
            Array.Sort(cards);
            long lo = 0, hi = 10000000000;
            while (lo + 1 < hi)
            {
                long decks = (lo + hi) / 2;
                if (can(decks, cards, jokers))
                {
                    lo = decks;
                }
                else
                {
                    hi = decks;
                }
            }
            if (can(hi, cards, jokers))
            {
                return (int)hi;
            }
            return (int)lo;
        }
        static void Main(string[] args)
        {
            CompilingDecksWithJokers cdwj = new CompilingDecksWithJokers();
            Console.WriteLine(cdwj.maxCompleteDecks(new int[]{10, 15}, 3));

            Console.ReadLine();
        }
    }
