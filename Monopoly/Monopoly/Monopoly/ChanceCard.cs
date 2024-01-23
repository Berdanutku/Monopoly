using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    // ChanceCard class which is inherited from Tile class
    internal class ChanceCard : Tile
    {
        // Constructor
        public ChanceCard(string name) : base(name)
        {
        }

        // List to store the indices of chance cards
        List<int> cards = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7 };

        // Override method to handle ChanceCard actions
        public override void PerformAction(Player player)
        {
            base.PerformAction(player);

            // Randomly select a chance card
            Random random = new Random();
            int number = -1;
            while (cards.Count > 0)
            {
                number = random.Next(0, 8);
                if (!cards.Contains(number))
                {

                }
                else
                {
                    // If card contains exits while condition
                    break;
                }
            }

            // If all chance cards are used, reset the list
            if (cards.Count == 1)
            {
                for (int i = 0; i < 8; i++)
                {
                    cards.Add(i);
                }
            }
            else
            {
                // Remove the selected chance card from the list
                cards.Remove(number);

            }

            // Handle the action based on the selected chance card
            if (number == 0) // Collect 150Ꝟ
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Collect 150Ꝟ.");
                player.SetBalance(player.GetBalance() + 150);
                Console.WriteLine(player.Name + "'s new Balance is: " + player.GetBalance() + "Ꝟ");
                Console.ResetColor();
            }
            else if (number == 1) // Collect 50Ꝟ
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Collect 50Ꝟ.");
                player.SetBalance(player.GetBalance() + 50);
                Console.WriteLine(player.Name + "'s new Balance is: " + player.GetBalance() + "Ꝟ");
                Console.ResetColor();
            }
            else if (number == 2) // Place 150Ꝟ on the board
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Place 150Ꝟ on the board.");
                player.SetBalance(player.GetBalance() - 150);
                Board.balance += 150;
                Console.WriteLine(player.Name + "'s new Balance is: " + player.GetBalance() + "Ꝟ");
                Console.ResetColor();
            }
            else if (number == 3) // Place money on the board for each owned house and hotel
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Place on the board 25Ꝟ for each owned house, and 100Ꝟ for each owned hotel.");
                int houses = 0;
                int hotels = 0;
                foreach (Property p in player.OwnedProperties)
                {
                    houses += p.HouseNumber;
                    hotels += p.HotelNumber;
                }
                player.SetBalance(player.GetBalance() - (houses * 25));
                Board.balance += houses * 25;
                player.SetBalance(player.GetBalance() - hotels * 100);
                Board.balance += hotels * 100;
                Console.WriteLine(player.Name + " placed " + ((houses * 25) + (hotels * 100)) + "Ꝟ on the board.");
                Console.WriteLine(player.Name + "'s new Balance is: " + player.GetBalance() + "Ꝟ");
                Console.ResetColor();
            }
            else if (number == 4) // Travel to the nearest train station
            {
                Console.WriteLine("Travel to the nearest train station. Collect 200Ꝟ if you pass through the beginning tile.");
                List<int> trainList = new List<int>();

                int train1 = Math.Abs(player.Position - 5);
                int train2 = Math.Abs(player.Position - 15);
                int train3 = Math.Abs(player.Position - 25);
                int train4 = Math.Abs(player.Position - 35);

                trainList.Add(train1);
                trainList.Add(train2);
                trainList.Add(train3);
                trainList.Add(train4);

                int minimum = trainList.Min();
                int index = trainList.IndexOf(minimum);
                string trainName = $"train{index + 1}";

                if (trainName == "train1")
                {
                    player.SetPosition(5);
                    Tile newTile = Board.tiles[player.Position];
                    newTile.PerformAction(player);
                }
                else if (trainName == "train2")
                {
                    player.SetPosition(15);
                    Tile newTile = Board.tiles[player.Position];
                    newTile.PerformAction(player);
                }
                else if (trainName == "train3")
                {
                    player.SetPosition(25);
                    Tile newTile = Board.tiles[player.Position];
                    newTile.PerformAction(player);
                }
                else if (trainName == "train4")
                {
                    player.SetPosition(35);
                    Tile newTile = Board.tiles[player.Position];
                    newTile.PerformAction(player);
                }
            }
            else if (number == 5) // Go back 3 tiles
            {
                Console.WriteLine("Go back 3 tiles.");
                if (player.Position < 3)
                {
                    if (player.Position == 2) { 
                        player.SetPosition(39);
                        Tile newTile = Board.tiles[player.Position];
                        newTile.PerformAction(player);
                    }
                    else if (player.Position == 1) { 
                        player.SetPosition(38);
                        Tile newTile = Board.tiles[player.Position];
                        newTile.PerformAction(player);
                    }
                    else if (player.Position == 0) { 
                        player.SetPosition(37);
                        Tile newTile = Board.tiles[player.Position];
                        newTile.PerformAction(player);
                    }
                }
                else
                {
                    player.SetPosition(player.Position - 3);
                    Tile newTile = Board.tiles[player.Position];
                    newTile.PerformAction(player);
                }
            }
            else if (number == 6) // Get out of jail immediately and get a Joker Card
            {
                Console.WriteLine("Get out of jail immediately, if in jail. (From now on player has a Joker Card)");
                player.SetJailJoker(true);
            }
            else if (number == 7) // Pay each player 50 Ꝟ
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Pay each player 50 Ꝟ");
                foreach (Player p in Program.players)
                {
                    p.SetBalance(player.GetBalance() + 50);
                }
                player.SetBalance(player.GetBalance() - (Program.players.Count * 50));
                Console.WriteLine(player.Name + "'s new Balance is: " + player.GetBalance() + "Ꝟ");
                Console.ResetColor();
            }
        }
    }
}
