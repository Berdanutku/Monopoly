using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    // Represents a train station inherits from Tile Class
    internal class TrainTile : Tile
    {
        // Attr
        public Player Owner;
        public int Cost;

        // Constructor
        public TrainTile(string name, int cost) : base(name)
        {
            Owner = null;
            Cost = cost;
        }

        // Override method to handle TrainTile actions
        public override void PerformAction(Player player)
        {
            base.PerformAction(player);
            if (Owner==null)
            {
                // If the train station is not owned, offer the player the option to buy
                Console.WriteLine("The Cost is: " + Cost + "Ꝟ" + "\nDo you want to buy this " + Name + "?");
                Console.WriteLine("1) Yes \n2) No");
                String input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        if (player.GetBalance() >= Cost)
                        {
                            Owner = player;
                            player.SetBalance(player.GetBalance() - Cost);
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.WriteLine("\nYou are now owner of the " + Name);
                            Console.WriteLine(player.Name + "'s new Balance is: " + player.GetBalance() + "Ꝟ");
                            Console.ResetColor();
                            player.AddTrains(this);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nNot Enough Money to buy!");
                            Console.ResetColor();
                        }
                        break;
                    case "2":
                        break;
                    default:
                        Console.WriteLine("Invalid Input!");
                        break;
                }
            }
            else if (Owner != null)
            {
                if (Owner == player)
                {
                    // If the train station is owned by the current player, display a welcome message
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Welcome to your " + Name);
                    Console.ResetColor();
                }
                else
                {
                    // If the train station is owned by another player, calculate and collect rent
                    List<Tile> OwnerProps = new List<Tile>();
                    foreach (Tile tile in Owner.GetTrains())
                    {
                        if (tile is TrainTile)
                        {
                            OwnerProps.Add(tile);
                        }
                    }
                    int rent = 50 * OwnerProps.Count;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine(Owner.Name + " has " + OwnerProps.Count + " stations your total payment is: " + rent + "Ꝟ");
                    player.SetBalance(player.GetBalance() - rent);
                    Owner.SetBalance(Owner.GetBalance() + rent);
                    Console.WriteLine(player.Name + "'s new balance is: " + player.GetBalance() + "Ꝟ");
                    Console.ResetColor();
                }
            }

        }
    }
}



