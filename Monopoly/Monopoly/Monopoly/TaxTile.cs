using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    internal class TaxTile : Tile
    {
        public string Name;

        // Constructor
        public TaxTile(string name) : base(name)
        {
            Name = name;
        }

        // Override method to handle TaxTile actions
        public override void PerformAction(Player player)
        {
            // Display messages in red background
            Console.BackgroundColor = ConsoleColor.Red;
            // Call the base PerformAction method
            base.PerformAction(player);

            // Check the name of tax tile and perform the appropriate action
            if (Name == "Income Tax")
            {
                Console.WriteLine(player.Name + " places 200Ꝟ on the board!");
                player.SetBalance(player.GetBalance() - 200);
                Console.WriteLine(player.Name + "'s new Balance is: " + player.GetBalance() + "Ꝟ");
                // Add the tax amount to the board's balance
                Board.balance += 200;
            }
            else if (Name == "Luxury Tax")
            {
                Console.WriteLine(player.Name + " places 150Ꝟ on the board!");
                player.SetBalance(player.GetBalance() - 150);
                Console.WriteLine(player.Name + "'s new Balance is: " + player.GetBalance() + "Ꝟ");
                // Add the tax amount to the board's balance
                Board.balance += 150;
            }
            Console.ResetColor();
        }
    }
}
