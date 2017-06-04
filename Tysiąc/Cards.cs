using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;

namespace Thousand
{
    public class Cards
    {
        //tables and lists
        //
        protected static string[] suit = { "9", "Jack", "Queen", "King", "10", "Ace" };
        protected static int[] suitValue = {0, 2, 3, 4, 10, 11};
        protected static string[] flush = { "Heart", "Diamond", "Club", "Spade" };
        protected static string[] deck = new string[DeckQuantity];
        public List<string> ShuffledDeck = new List<string>();
        List<int> ShuffledDeckList = new List<int>();


        //constants
        //
        protected const int DeckQuantity = 24;

        //variable
        //
        protected int actualCardValue = 0;
        Random randomizeCard = new Random();



        //Operating methods
        //
        public void CreateDeck()
        {
            foreach(string actualnFlush in flush)
            {
                foreach (string actualSuit in suit)
                {
                    deck[actualCardValue] = actualSuit + " " + actualnFlush;
                    Console.Write("\t{0}", deck[actualCardValue]);
                    actualCardValue++;
                }
                Console.WriteLine();
            }
        }



        public void ShuffleDeck()
        {
             do
            {
                int randomCardValue = randomizeCard.Next(DeckQuantity);
                for (actualCardValue = 0; actualCardValue < DeckQuantity; actualCardValue++)
                {
                    if (randomCardValue == actualCardValue && !ShuffledDeckList.Contains(randomCardValue))
                    {
                        ShuffledDeck.Add(deck[actualCardValue]);
                        ShuffledDeckList.Add(actualCardValue);
                    }
                }
            } while (ShuffledDeck.Count < DeckQuantity);
            ShuffledDeckList.Clear();
        }


        //Display methods
        //
        public void DisplayShuffledDeck()
        {
            foreach (string card in ShuffledDeck)
            {
                Console.WriteLine("{0}\n", card);
            }
        }
    }
}
