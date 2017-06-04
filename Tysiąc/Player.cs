using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thousand
{
    class Player : Cards
    {
        public List<string> CardsInHand { get; set; }
        public List<string> ReportOnHand = new List<string>();


        public string Name { get; set; }
        public bool isInLine { get; set; }
        public int Points { get; set; }
        public int PointsOnHand { get; set; }
        public int ReportPointsOnHand { get; set; }
        public bool IsInBid { get; set; }
        public bool IsFlushInCard { get; set; }

        public const int CardLimitInHand = 7;


        public Player()
        {
            Console.WriteLine("Your name: ");
            Name = Console.ReadLine();
            isInLine = false;
            IsInBid = true;
            IsFlushInCard = false;
        }


        //Operating methods
        //
        //
        public void GiveCardsToHand(List<string> shuffledDeck)
        {
                if (shuffledDeck.Count < CardLimitInHand)
                {
                    CardsInHand = shuffledDeck.GetRange(0, shuffledDeck.Count);
                    shuffledDeck.Clear();
                    IsInBid = false;
                }
                else
                {
                    CardsInHand = shuffledDeck.GetRange(0, CardLimitInHand);
                    shuffledDeck.RemoveRange(0, CardLimitInHand);
                }
        }



        public void GrabAllPoints()
        {
            PointsOnHand = 0;
            for (int actualCard = 0; actualCard < CardsInHand.Count; actualCard++)
            {
                for (int actualSuit = 0; actualSuit < suit.Length; actualSuit++)
                {
                    if (CardsInHand[actualCard].StartsWith(suit[actualSuit]))
                    {
                        PointsOnHand += suitValue[actualSuit];
                    }
                }
            }
            
        }



        public void GrabIntendialReport() //Okropnie zrobione, ale nie myślałem na ten moment nad innym rozwiązaniem
        {
            ReportOnHand.Clear();
            foreach(string potentialQueen in CardsInHand)
            {
                if (potentialQueen.StartsWith("Queen"))
                {
                    foreach(string potentialKing in CardsInHand)
                    {
                        if(potentialKing.StartsWith("King"))
                        {
                            foreach (string ReportColor in flush)
                            {
                                if (potentialQueen.EndsWith(ReportColor) && potentialKing.EndsWith(ReportColor))
                                {
                                    ReportOnHand.Add(ReportColor);
                                }
                            }
                        }
                    }
                }
            }
        }


        public void GrabIntendialReportPoints()
        {
            ReportPointsOnHand = 0;
            foreach(string flush in ReportOnHand)
            {
                if (flush == "Heart") ReportPointsOnHand += 100;
                else if (flush == "Diamond") ReportPointsOnHand += 80;
                else if (flush == "Club") ReportPointsOnHand += 60;
                else if
                    (flush == "Spade") ReportPointsOnHand += 40;
            }
        }


        //Sorting method
        //
        //
        public void SortCardInHand()
        {
            List<string> tempCardOnHand = new List<string>();
            for (actualCardValue = 0; actualCardValue < deck.Length; actualCardValue++)
            {
                foreach (string card in CardsInHand)
                {
                    if (card == deck[actualCardValue]) tempCardOnHand.Add(card);
                }
            }

            CardsInHand.Clear(); //Clear list for adding temporary cards 
            foreach (string karta in tempCardOnHand)
            {
                CardsInHand.Add(karta);
            }
        }



        //Metody wyświetlające
        //
        //
        public void DisplayHand()
        {
            SortCardInHand();
            int countCard = 1;
            Console.WriteLine("\nPlayer {0}: ", Name);
            foreach (string card in CardsInHand)
            {
                Console.WriteLine("  {0}: {1}", countCard, card);
                countCard++;
            }
            Console.WriteLine();
            DisplayDeckProperties();
            Console.WriteLine();
        }



        public void DisplayDeckProperties()
        {
            foreach (string report in ReportOnHand)
            {
                if (report != null)
                {
                    Console.WriteLine("Report on hand: {0}", report);
                    
                }
            }
            Console.WriteLine("Points on hand: {0}", PointsOnHand + ReportPointsOnHand);
        }
    }
}