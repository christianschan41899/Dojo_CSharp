using System;
using System.Collections.Generic;

namespace Deck_of_Cards
{
    class Card
    {
        //Card properties
        public string stringVal;
        public int val;
        public string suit;

        //Card constructor
        public Card(string stringVal, int val, string suit)
        {
            this.stringVal = stringVal;
            this.val = val;
            this.suit = suit;
        }
    }

    class Deck
    {
        //Deck properties
        public List<Card> cards;

        //Deck constructor
        public Deck()
        {
            initializeDeck();
        }

        //Deals card and removes it from list
        public Card Deal()
        {
            Console.WriteLine($"Current deck size is {cards.Count}");
            if(cards.Count != 0)
            {
                Card draw = cards[cards.Count - 1];
                cards.RemoveAt(cards.Count - 1);
                Console.WriteLine($"Drew {draw.stringVal}, {draw.suit}");
                return draw;
            }
            return null;
        }

        //Resets card list
        public void Reset()
        {
            initializeDeck();
        }

        //Shuffle at random
        public void Shuffle()
        {
            Random shuffleIdx = new Random();
            int deckSize = cards.Count - 1;
            while(deckSize >= 0)
            {
                int randIdx = shuffleIdx.Next(deckSize+1);
                Card temp = cards[deckSize];
                cards[deckSize] = cards[randIdx];
                cards[randIdx] = temp;
                deckSize--;
            }
        }
        //Will do the creation of the deck for us, since both Reset and constructor will call it.
        private void initializeDeck()
        {
            this.cards = new List<Card>()
            {
                new Card("Ace", 1, "Diamond"),
                new Card("2", 2, "Diamond"),
                new Card("3", 3, "Diamond"),
                new Card("4", 4, "Diamond"),
                new Card("5", 5, "Diamond"),
                new Card("6", 6, "Diamond"),
                new Card("7", 7, "Diamond"),
                new Card("8", 8, "Diamond"),
                new Card("9", 9, "Diamond"),
                new Card("10", 10, "Diamond"),
                new Card("Jack", 11, "Diamond"),
                new Card("Queen", 12, "Diamond"),
                new Card("King", 13, "Diamond"),
                new Card("Ace", 1, "Heart"),
                new Card("2", 2, "Heart"),
                new Card("3", 3, "Heart"),
                new Card("4", 4, "Heart"),
                new Card("5", 5, "Heart"),
                new Card("6", 6, "Heart"),
                new Card("7", 7, "Heart"),
                new Card("8", 8, "Heart"),
                new Card("9", 9, "Heart"),
                new Card("10", 10, "Heart"),
                new Card("Jack", 11, "Heart"),
                new Card("Queen", 12, "Heart"),
                new Card("King", 13, "Heart"),
                new Card("Ace", 1, "Spade"),
                new Card("2", 2, "Spade"),
                new Card("3", 3, "Spade"),
                new Card("4", 4, "Spade"),
                new Card("5", 5, "Spade"),
                new Card("6", 6, "Spade"),
                new Card("7", 7, "Spade"),
                new Card("8", 8, "Spade"),
                new Card("9", 9, "Spade"),
                new Card("10", 10, "Spade"),
                new Card("Jack", 11, "Spade"),
                new Card("Queen", 12, "Spade"),
                new Card("King", 13, "Spade"),
                new Card("Ace", 1, "Club"),
                new Card("2", 2, "Club"),
                new Card("3", 3, "Club"),
                new Card("4", 4, "Club"),
                new Card("5", 5, "Club"),
                new Card("6", 6, "Club"),
                new Card("7", 7, "Club"),
                new Card("8", 8, "Club"),
                new Card("9", 9, "Club"),
                new Card("10", 10, "Club"),
                new Card("Jack", 11, "Club"),
                new Card("Queen", 12, "Club"),
                new Card("King", 13, "Club"),
            };
        }
    }

    class Player
    {
        public string Name;
        public List<Card> hand;

        public Player(string name)
        {
            this.Name = name;
            hand = new List<Card>();
        }

        public void Draw(Deck deck)
        {
            hand.Add(deck.Deal());
        }

        public Card Discard(int handIdx)
        {
            if(handIdx < hand.Count-1)
            {
                Card discard = hand[handIdx];
                Console.WriteLine($"Discarded {discard.stringVal}, {discard.suit}");
                hand.RemoveAt(handIdx);
                return discard;
            }
            else
            {
                Console.WriteLine("No card to discard!");
                return null;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Individual deck testing
            Deck deck1 = new Deck();
            Card card1 = deck1.Deal();
            Card card2 = deck1.Deal();
            deck1.Shuffle();
            Card card3 = deck1.Deal();
            deck1.Reset();
            deck1.Shuffle();
            Console.WriteLine("");
            //Player testing
            Console.WriteLine("Player 1 drawing...");
            Player p1 = new Player("John");
            p1.Draw(deck1);
            p1.Draw(deck1);
            p1.Draw(deck1);
            p1.Draw(deck1);
            p1.Draw(deck1);
            p1.Discard(2);
            p1.Discard(50);

        }
    }
}
