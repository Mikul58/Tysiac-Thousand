using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thousand
{
    class Rules : Player
    {
        //Properties
        //
        static char BidUp { get; set; }

        //Lists
        //
        static List<string> OneLapCards = new List<string>(); //Cards for only one lap 

        //Variables
        //
        static int joinFirstPlayer = 0;
        static public int Bid = 100;
        static short quantityOfFolds = 0;
        static int pointsInLap = 0;
        static string highestCard = null;
        static int playerPick = 0; //Picks a card 


        //Constants
        //
        const int pointsOverflow = 10;
        const int maxBid = 300;
        const short maxFolds = 2;



        static public void SetLine(Player[] players) //Kolejka konczy się na graczu z 3 kartami
        {
            Player[] playerCopy = new Player[players.Length];
            for (int actualPlayer = 0; actualPlayer < players.Length; actualPlayer++)
            {
                if (players[actualPlayer].isInLine == true)
                {
                    Array.Copy(players, actualPlayer, playerCopy, 0, players.Length - actualPlayer);
                    Array.Copy(players, 0, playerCopy, players.Length - actualPlayer, actualPlayer);
                    players[actualPlayer].isInLine = false;
                    if (actualPlayer + 1 == players.Length) //Sprawdzenie czy ostatni gracz na kolejce
                    {
                        players[0].isInLine = true;
                    }
                    else
                    {
                        players[actualPlayer + 1].isInLine = true;
                    }
                    break;
                }
            }
            Array.Copy(playerCopy, 0, players, 0, playerCopy.Length);
        }



        static public void MakeBid(Player[] players)
        {
            players[0].IsInBid = false;
            string BiddersName = "null";
            do
            {
                foreach (Player player in players)
                {
                    //If 2 players folded, skip this loop
                    //
                    if (quantityOfFolds == 2) continue;

                    //If player have 7 cards and didn't fold
                    //
                    else if (player.CardsInHand.Count == CardLimitInHand && player.IsInBid == true)
                    {
                        PlayersPick(player);
                        if (Char.ToUpper(BidUp) == 'T')
                        {
                            Bid += pointsOverflow;
                            Console.WriteLine("\nYour bid: {0}", Bid);
                            BiddersName = player.Name;
                            player.IsInBid = true;
                            player.isInLine = true;
                        }
                        else if (Char.ToUpper(BidUp) == 'N')
                        {
                            Console.WriteLine("\nYou Fold");
                            player.IsInBid = false;
                            player.isInLine = false;
                            quantityOfFolds++;
                            if (joinFirstPlayer == 0) BiddersName = players[0].Name;
                            if (quantityOfFolds == maxFolds)
                            {
                                Console.WriteLine("\nFirst player: {0}. He Bid {1}", BiddersName, Bid);
                            }
                        }
                        joinFirstPlayer++;
                        if (joinFirstPlayer == 1) players[0].IsInBid = true;
                    }
                }
            } while (quantityOfFolds != maxFolds);
        }



        static void PlayersPick(Player player)
        {
            do
            {
                Console.WriteLine("\n{0}\nDo you want to bid for {1}? \n\tT(Yes)/N(No)", player.Name, Bid + pointsOverflow);
                BidUp = Console.ReadKey(true).KeyChar;
            } while (Char.ToUpper(BidUp) != 'T' && Char.ToUpper(BidUp) != 'N');
        }



        static public void AddStack(Player[] players)
        {
            foreach (Player player in players)
            {
                if (player.CardsInHand.Count < CardLimitInHand)
                {
                    players[0].CardsInHand.AddRange(player.CardsInHand);
                    player.CardsInHand.Clear();
                }
            }
        }



        static void GiveOverflowingCards(Player[] players)
        {
            foreach (Player player in players)
            {
                if (player.CardsInHand.Count < CardLimitInHand || player.CardsInHand.Count > CardLimitInHand) continue;
                players[0].DisplayHand();
                Console.WriteLine("Pick a card for player {0}", player.Name);
                PickCard(players[0].CardsInHand, player.CardsInHand);
                DeleteCard(players[0].CardsInHand);
            }
        }



        static public void Play(Player[] players)
        {
            foreach (Player player in players)
            {
                player.GrabIntendialReport();              //Updates data about reports and points
                player.GrabIntendialReportPoints();        //
                player.GrabAllPoints();                    //  
                player.DisplayHand();                      //   
            }
            GiveOverflowingCards(players);
            GrabCardsFromLap(players);
            GrabPointsFromLap();
            GetHeighestCard();
            Console.WriteLine("Highest card: {0}, points in lap: {1}", highestCard, pointsInLap);
            foreach (string card in OneLapCards)
            {
                Console.WriteLine(card);
            }
        }



        static void GrabCardsFromLap(Player[] players)
        {
            foreach (Player player in players)
            {
                for(int i = 0; i < player.CardsInHand.Count; i++)
                {
                    if (IsCardsInFlush(player.CardsInHand, i))
                    {
                        player.IsFlushInCard = true;
                        break;
                    }
                }
                if (player.CardsInHand.Count > 0)
                {
                    do
                    {
                        player.DisplayHand();
                        PickCard(player.CardsInHand, OneLapCards);
                        if (OneLapCards.Count > 1) //Loop from OneLapCards must have cards for check
                        {
                            if (player.IsFlushInCard == false) break;
                            else if (!IsCardsInFlush(player.CardsInHand, playerPick-1)) // If last card isn't in the same color, card is deleted
                            {
                                Console.WriteLine("Card is from aother color");
                                OneLapCards.RemoveAt(OneLapCards.Count - 1);
                            }
                        }
                    } while (!IsCardsInFlush(player.CardsInHand, playerPick-1) || !player.IsFlushInCard);
                    DeleteCard(player.CardsInHand);
                }
            }
        }



        static void PickCard(List <string> CardsFromHand, List <string> CardsToHand)
        {
                Console.WriteLine("Which card you choose?");
                playerPick = Convert.ToInt32(Console.ReadLine());
                CardsToHand.Add(CardsFromHand[playerPick-1]);
        }



        static void DeleteCard(List<string> CardsFromHand)
        {
            CardsFromHand.Remove(CardsFromHand[playerPick - 1]);
        }



        static void GrabPointsFromLap()
        {
            pointsInLap = 0;                           //Reseting points before counting
            for(int i = 0; i < OneLapCards.Count; i++)
            {
                for(int j = 0; j < suit.Length; j++)
                {
                    if (OneLapCards[i].StartsWith(suit[j]))
                    {
                        pointsInLap += suitValue[j];
                    }
                }
            }
        }



        static void GetHeighestCard()
        {
            highestCard = OneLapCards[0];
            int valueOfHighestCard = 0;
            for(int i = 0; i < OneLapCards.Count; i++)
            {
                for(int j = 0; j < suit.Length;j++)
                {
                    if(OneLapCards[i].StartsWith(suit[j]))
                    {
                        if (valueOfHighestCard < j)
                        {
                            highestCard = OneLapCards[i];
                            valueOfHighestCard = j;
                        }
                    }
                }
            }
        }



        static bool IsCardsInFlush(List <string> cards, int cardValue)
        {
            string[] firstCardFlush = OneLapCards[0].Split(' ');
            string actualCard = cards[cardValue];
            {
                if (actualCard.EndsWith(firstCardFlush[0]))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
