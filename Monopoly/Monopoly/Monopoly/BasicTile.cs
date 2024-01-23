using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    // Represents a basic tiles such as Begginning Tile, Free Parking, Go To Jail, Jail Tiles...
    public class BasicTile : Tile
    {
        // Constructor
        public BasicTile(string name) : base(name)
        {
        }

        // Override method to handle basic tile actions
        public override void PerformAction(Player player)
        {
            base.PerformAction(player);

            // Check the name of basic tile and perform corresponding actions
            if (Name.Equals("Begginning Tile"))
            {
                // Give the player 200Ꝟ for passing the beginning tile
                player.SetBalance(player.GetBalance() + 200);
                Console.WriteLine("Player's new balance is: " + player.GetBalance()+ "Ꝟ");
            }
            else if (Name.Equals("Free Parking"))
            {
                // Collect the money accumulated on the board and give it to the player
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(player.Name + " collects " + Board.balance + "Ꝟ on the board!");
                Console.ResetColor();
                player.SetBalance(player.GetBalance() + Board.balance);
                Board.balance = 0;
            }
            else if (Name.Equals("Go To Jail"))
            {
                // Check if the player has a "Get out of jail" card, otherwise, send the player to jail
                if (player.JailJoker)
                {
                    Console.WriteLine(player.Name + " is not going to Jail. (had a chance card).");
                    player.SetJailJoker(false);
                }
                else
                {
                    Console.WriteLine(player.Name + " will go to jail.");
                    player.SetInJail(true);
                    player.SetPosition(10);
                    player.TurnsInJail++;
                }
            }
            else if (Name.Equals("Jail"))
            {
                // Inform the player that they are just visiting the jail
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(player.Name + " is just visiting the Jail.");
                Console.ResetColor();
            }
        }
    }
}
