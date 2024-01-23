using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    // Represents the game board
    public class Board
    {
        // List of tiles on the board
        public static List<Tile> tiles;
        // Variable for board's balance
        public static int balance;

        // Constructor
        public Board()
        {
            balance = 0;
            tiles = new List<Tile>();
            InitializeTiles();
        }

        // Initialize the tiles on the board
        private void InitializeTiles()
        {
            // Tile instances
            Tile beginningTile = new BasicTile("Beginning Tile");
            Tile brown1 = new Property("Brown 1", "Property", 60, 4, 50);
            Tile community1 = new CommunityChestCard("Community Chest Card 1");
            Tile brown2 = new Property("Brown 2", "Property", 60, 4, 50);
            Tile incomeTax = new TaxTile("Income Tax");
            Tile train1 = new TrainTile("Train 1", 100);
            Tile lightBlue1 = new Property("Light Blue 1", "Property", 120, 8, 50);
            Tile chance1 = new ChanceCard("Chance Card 1");
            Tile lightBlue2 = new Property("Light Blue 2", "Property", 120, 8, 50);
            Tile lightBlue3 = new Property("Light Blue 3", "Property", 120, 8, 50);
            Tile jailTile = new BasicTile("Jail");
            Tile purple1 = new Property("Purple 1", "Property", 120, 8, 50);
            Tile utility1 = new UtilityTile("Utility 1", 150);
            Tile purple2 = new Property("Purple 2", "Property", 160, 12, 100);
            Tile purple3 = new Property("Purple 3", "Property", 160, 12, 100);
            Tile train2 = new TrainTile("Train 2", 100);
            Tile orange1 = new Property("Orange 1", "Property", 200, 16, 100);
            Tile community2 = new CommunityChestCard("Community Chest Card 2");
            Tile orange2 = new Property("Orange 2", "Property", 200, 16, 100);
            Tile orange3 = new Property("Orange 3", "Property", 200, 16, 100);
            Tile freeParking = new BasicTile("Free Parking");
            Tile red1 = new Property("Red 1", "Property", 240, 20, 150);
            Tile chance2 = new ChanceCard("Chance Card 2");
            Tile red2 = new Property("Red 2", "Property", 240, 20, 150);
            Tile red3 = new Property("Red 3", "Property", 240, 20, 150);
            Tile train3 = new TrainTile("Train 3", 100);
            Tile yellow1 = new Property("Yellow 1", "Property", 280, 24, 150);
            Tile yellow2 = new Property("Yellow 2", "Property", 280, 24, 150);
            Tile utility2 = new UtilityTile("Utility 2", 150);
            Tile yellow3 = new Property("Yellow 3", "Property", 280, 24, 150);
            Tile goToJail = new BasicTile("Go To Jail");
            Tile green1 = new Property("Green 1", "Property", 320, 28, 200);
            Tile green2 = new Property("Green 2", "Property", 320, 28, 200);
            Tile community3 = new CommunityChestCard("Community Chest Card 3");
            Tile green3 = new Property("Green 3", "Property", 320, 28, 200);
            Tile train4 = new TrainTile("Train 4", 100);
            Tile chance3 = new ChanceCard("Chance Card 3");
            Tile blue1 = new Property("Blue 1", "Property", 400, 50, 200);
            Tile luxuryTax = new TaxTile("Luxury Tax");
            Tile blue2 = new Property("Blue 2", "Property", 400, 50, 200);


            // Add tiles to the list
            tiles.Add(beginningTile);
            tiles.Add(brown1);
            tiles.Add(community1);
            tiles.Add(brown2);
            tiles.Add(incomeTax);
            tiles.Add(train1);
            tiles.Add(lightBlue1);
            tiles.Add(chance1);
            tiles.Add(lightBlue2);
            tiles.Add(lightBlue3);
            tiles.Add(jailTile);
            tiles.Add(purple1);
            tiles.Add(utility1);
            tiles.Add(purple2);
            tiles.Add(purple3);
            tiles.Add(train2);
            tiles.Add(orange1);
            tiles.Add(community2);
            tiles.Add(orange2);
            tiles.Add(orange3);
            tiles.Add(freeParking);
            tiles.Add(red1);
            tiles.Add(chance2);
            tiles.Add(red2);
            tiles.Add(red3);
            tiles.Add(train3);
            tiles.Add(yellow1);
            tiles.Add(yellow2);
            tiles.Add(utility2);
            tiles.Add(yellow3);
            tiles.Add(goToJail);
            tiles.Add(green1);
            tiles.Add(green2);
            tiles.Add(community3);
            tiles.Add(green3);
            tiles.Add(train4);
            tiles.Add(chance3);
            tiles.Add(blue1);
            tiles.Add(luxuryTax);
            tiles.Add(blue2);
        }

        // Move the player on the board according to jail condition
        public void MovePlayer(Player player)
        {
            // Check if the player is in jail
            if (player.InJail == true)
            {
                // Handle jail conditions
                if (player.TurnsInJail < 3)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.WriteLine(player.Name + " is in jail.");
                    Console.ResetColor();
                    player.TurnsInJail++;
                    return;
                }
                else
                {
                    player.SetInJail(false);
                    player.TurnsInJail = 0;
                }
            }

            // Move the player
            player.Move(player);

            // Get the tile where the player landed
            Tile landedTile = tiles[player.Position];

            // Handle interactions with the landed tile
            HandleTileInteraction(player, landedTile);
        }

        // Handle interactions with the landed tile
        private void HandleTileInteraction(Player player, Tile tile)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n{player.Name} landed on {tile.Name}.");
            Console.ResetColor();

            // Perform actions specific to the landed tile
            tile.PerformAction(player);
        }

        // Display the current state of the board
        public void DisplayBoard()
        {
            foreach (Tile tile in tiles)
            {
                // Highlight the tile with the color
                foreach (Player player in Program.players)
                {
                    int tilePosition = tiles.IndexOf(tile);
                    if (player.Position == tilePosition)
                    {
                        Console.ForegroundColor = player.Color;
                    }
                }

                // Display the tile information
                tile.Display();
                Console.ResetColor();
            }
        }
    }
}

