using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    internal class CommunityChestCard : Tile
    {
        // Constructor
        public CommunityChestCard(string name) : base(name)
        {
        }

        // List to store the indices of community chest cards
        List<int> cards = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7 };

        // Override method to handle CommunityChestCard actions
        public override void PerformAction(Player player)
        {
            base.PerformAction(player);

            // Randomly select a community chest card
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

            // If all community chest cards are used, reset the list
            if (cards.Count == 1)
            {
                for (int i = 0; i < 8; i++)
                {
                    cards.Add(i);
                }
            }
            else
            {
                // Remove the selected community chest card from the list
                cards.Remove(number);

            }

            // Handle the action based on the selected community chest card
            if (number == 0) // Collect 200Ꝟ
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Collect 200Ꝟ.");
                player.SetBalance(player.GetBalance() + 200);
                Console.WriteLine(player.Name + "'s new Balance is: " + player.GetBalance() + "Ꝟ");
                Console.ResetColor();
            }
            else if (number == 1) // Collect 100Ꝟ
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Collect 100Ꝟ.");
                player.SetBalance(player.GetBalance() + 100);
                Console.WriteLine(player.Name + "'s new Balance is: " + player.GetBalance() + "Ꝟ");
                Console.ResetColor();
            }
            else if (number == 2) // Place 100Ꝟ on the board
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Place 100Ꝟ on the board.");
                player.SetBalance(player.GetBalance() - 100);
                Board.balance += 100;
                Console.WriteLine(player.Name + "'s new Balance is: " + player.GetBalance() + "Ꝟ");
                Console.ResetColor();
            }
            else if (number == 3) // Place money on the board for each owned house and hotel
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Place on the board 40Ꝟ for each owned house, and 115Ꝟ for each owned hotel.");
                int houses = 0;
                int hotels = 0;
                foreach (Property p in player.OwnedProperties)
                {
                    houses += p.HouseNumber;
                    hotels += p.HotelNumber;
                }
                player.SetBalance(player.GetBalance() - (houses * 40));
                Board.balance += houses * 40;
                player.SetBalance(player.GetBalance() - hotels * 115);
                Board.balance += hotels * 115;
                Console.WriteLine(player.Name + " placed " + ((houses * 40) + (hotels * 115)) + "Ꝟ on the board.");
                Console.WriteLine(player.Name + "'s new Balance is: " + player.GetBalance() + "Ꝟ");
                Console.ResetColor();
            }
            else if (number == 4) // Travel to the nearest utility
            {
                Console.WriteLine("Travel to the nearest utility (electric company or water works). Collect 200Ꝟ if you pass through the beginning tile.");
                List<int> utilityList = new List<int>();

                int utility1 = Math.Abs(player.Position - 12);
                int utility2 = Math.Abs(player.Position - 28);

                utilityList.Add(utility1);
                utilityList.Add(utility2);

                int minimum = utilityList.Min();
                int index = utilityList.IndexOf(minimum);
                string utilityName = $"utility{index + 1}";

                if (utilityName == "utility1")
                {
                    player.SetPosition(12);
                    Tile newTile = Board.tiles[player.Position];
                    newTile.PerformAction(player);
                }
                else if (utilityName == "utility2")
                {
                    player.SetPosition(28);
                    Tile newTile = Board.tiles[player.Position];
                    newTile.PerformAction(player);
                }
            }
            else if (number == 5) // Advance to the beginning tile
            {
                Console.WriteLine("Advance to the beginning tile.");
                player.SetPosition(0);
                player.SetBalance(player.GetBalance() + 200);
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("\n" + player.Name + " now on the beginning tile and will get 200Ꝟ after passing."); //Doğru mu ???
                //Console.WriteLine("Player's new balance is: " + player.GetBalance() + "Ꝟ");
                Console.ResetColor();
            }
            else if (number == 6) // Travel to jail immediately
            {
                Console.WriteLine("Travel to jail immediately. Do not collect 200Ꝟ if you pass through the beginning tile.");
                if (player.JailJoker)
                {
                    Console.WriteLine(player.Name + " is not going to Jail. (had a chance card).");
                    player.SetJailJoker(false);
                }
                else
                {
                    player.SetInJail(true);
                    player.SetPosition(10);
                }
            }
            else if (number == 7) // Collect 100Ꝟ from each player
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Collect 100Ꝟ from each player.");
                player.SetBalance(player.GetBalance() + 100);
                foreach (Player p in Program.players)
                {
                    p.SetBalance(player.GetBalance() - 100);
                    if(p.GetBalance() < 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\t" + p.Name + " Lost!");
                        Console.ResetColor();
                    }
                  
                }
                player.SetBalance(player.GetBalance() + ((Program.players.Count -1) * 100));
                Console.WriteLine(player.Name + "'s new Balance is: " + player.GetBalance() + "Ꝟ");
                Console.ResetColor();
            }
        }
    }
}
