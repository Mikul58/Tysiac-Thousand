using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Thousand
{
    class Program
    {
        static void Main(string[] args)
        {
            Cards cards = new Cards();
            Random whoFirst = new Random();
            cards.CreateDeck();
            bool isPlayersQuantity = true;
            int playersQuantity;
            int firstPlayer = 0;
            do
            {
                playersQuantity = 0;
                Console.WriteLine("How many players? (For now you can pick only 4 players)");
                try
                {
                    playersQuantity = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("It's not a valid number!");
                }
                Player[] players = new Player[playersQuantity];        //Creating table of players

                switch (playersQuantity)
                {
                    case 2:
                        {
                            Console.WriteLine("You choose 2 players (game is closing because of test)");
                            isPlayersQuantity = false;
                            break;
                        }

                    case 3:
                        {
                            Console.WriteLine("You choose 3 players (game is closing because of test)");
                            isPlayersQuantity = false;
                            break;
                        }
                    case 4:
                        {
                            Console.WriteLine("You choose 4 players");
                            CreatePlayers(players);
                            firstPlayer = whoFirst.Next(playersQuantity);
                            players[firstPlayer].isInLine = true;
                            Rules.SetLine(players); //Repeat, until someone will have 1000 points
                            do
                            {
                                cards.ShuffleDeck(); //Preparing for whole game
                                foreach(Player player in players)
                                {
                                    player.GiveCardsToHand(cards.ShuffledDeck);
                                    player.GrabIntendialReport();
                                    player.GrabIntendialReportPoints();
                                    player.GrabAllPoints();
                                    player.DisplayHand();
                                }
                            } while (!CheckAllPoints(players));
                            Rules.MakeBid(players);
                            Rules.SetLine(players);
                            Rules.AddStack(players);
                            Rules.Play(players);


                            isPlayersQuantity = false;
                            break;
                        }
                    default:
                        {
                            isPlayersQuantity = true;
                            break;
                        }
                }
            } while (isPlayersQuantity);
            Console.ReadKey();
        }



        static public void CreatePlayers(Player[] players)
        {
            for (int player = 0; player < players.Length; player++)
            {
                players[player] = new Player();
            }
        }



        static public bool CheckAllPoints(Player[] players)
        {
            bool isPointsValue = true;
            foreach (Player player in players)
            {
                if (player.PointsOnHand + player.ReportPointsOnHand < 21)
                {
                    if (player.CardsInHand.Count < Player.CardLimitInHand)
                    {
                        continue;
                    }
                    else
                    {
                        isPointsValue = false;
                    }
                }
            }

            if (isPointsValue == false)
            {
                foreach (Player player in players)
                {
                    player.PointsOnHand = 0;
                    player.ReportPointsOnHand = 0;
                }
            }
            return isPointsValue;
        }
    }
}
