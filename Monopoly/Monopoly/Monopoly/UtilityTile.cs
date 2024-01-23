using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    // Represents a utility inherits from Tile Class
    internal class UtilityTile : Tile
    {
        // Attributes
        public Player Owner;
        public int Cost;

        // Constructor
        public UtilityTile(string name, int cost) : base(name)
        {
            Owner = null;
            Cost = cost;
        }

        // Override method to handle UtilityTile actions
        public override void PerformAction(Player player)
        {
            base.PerformAction(player);
            if (Owner==null)
            {
                // If the utility is not owned, offer the player the option to buy
                Console.WriteLine("The Cost is: " + Cost + "Ꝟ" + "\nDo you want to buy this " + Name + "?");
                Console.WriteLine("1) Yes \n2) No");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        if (player.GetBalance() >= Cost)
                        {
                            Owner = player;
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            player.SetBalance(player.GetBalance() - Cost);
                            Console.WriteLine("You are now owner of the " + Name);
                            Console.WriteLine(player.Name + "'s new Balance is: " + player.GetBalance() + "Ꝟ");
                            Console.ResetColor();
                            player.AddUtilities(this);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Not Enough Money to buy!");
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
                    // If the utility is owned by the current player, display a welcome message
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Welcome to your " + Name);
                    Console.ResetColor();
                }
                else
                {
                    // If the utility is owned by another player, calculate and collect rent
                    List<Tile> OwnerProps = new List<Tile>();
                    foreach (Tile tile in Owner.GetUtilities())
                    {
                        if (tile is UtilityTile)
                        {
                            OwnerProps.Add(tile);
                        }
                    }
                    int rent = 0;
                    if (OwnerProps.Count == 1)
                    {
                        rent = player.RollDice() * (5 * OwnerProps.Count);

                    }
                    else
                    {
                        rent = player.RollDice() * (10 * OwnerProps.Count);
                    }
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine(Owner.Name + " has " + OwnerProps.Count + " stations your total payment is: " + rent + "Ꝟ");
                    player.SetBalance(player.GetBalance() - rent);
                    Console.WriteLine(player.Name + "'s new Balance is: " + player.GetBalance() + "Ꝟ");
                    Console.ResetColor();
                }
            }
        }
    }
}


