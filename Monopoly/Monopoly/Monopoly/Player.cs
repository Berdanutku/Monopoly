using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Monopoly
{
    public class Player
    {
        public string Name { get; private set; }
        private int Balance;
        public int Position { get; private set; }
        public List<Property> OwnedProperties { get; private set; }
        public List<Tile> OwnedTrains;
        public List<Tile> OwnedUtilities;

        // Jail-related properties
        public bool InJail { get; private set; }
        public bool JailJoker { get; private set; }
        public int TurnsInJail { get; set; }

        // To determine the order of players
        public int SingleDieScore { get; internal set; }

        // Player's color
        public ConsoleColor Color;

        // Constructor
        public Player(string name, ConsoleColor color)
        {
            Name = name;
            Balance = 200;
            Position = 0;
            OwnedProperties = new List<Property>();
            OwnedTrains = new List<Tile>();
            OwnedUtilities = new List<Tile>();
            InJail = false;
            JailJoker = false;
            TurnsInJail = 0;
            Color = color;
        }

        // Getters and Setters
        public ConsoleColor GetColor() { return Color; }
        public void SetJailJoker( bool jailJoker) { JailJoker = jailJoker; }
        public int GetBalance() { return Balance; }
        public void SetBalance(int balance) { Balance = balance; }
        public void SetInJail(bool inJail) { InJail = inJail; }
        public void SetPosition(int position) { Position = position; }
        public List<Property> GetProperties() { return OwnedProperties; }
        public List<Tile> GetTrains() { return OwnedTrains; }
        public List<Tile> GetUtilities() { return OwnedUtilities; }

        // Method to roll two dice to move
        public int RollDice()
        {
            Random random = new Random();
            int firstDice = random.Next(1, 7);
            int secondDice = random.Next(1, 7);
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\nFirst dice: " + firstDice + "\nSecond dice: " + secondDice);
            Console.ResetColor();
            return firstDice + secondDice;
        }

        // Method to roll a single die to determine players order
        public int RollSingleDie()
        {
            Random random = new Random();
            int dieScore = random.Next(1, 7);
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n" + Name +"'s Single Die Score: " + dieScore);
            Console.ResetColor();
            return dieScore;
        }


        // Move the player based on the dice roll
        public void Move(Player player)
        {
            int steps = player.RollDice();
            if ((Position + steps) >= 40)
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("\n" + Name + " pass through the beginning tile and get 200Ꝟ.");
                Balance += 200;
                Console.WriteLine("Player's new Balance is: " + Balance + "Ꝟ");
                Console.ResetColor();
            }
            Position = (Position + steps) % 40;
        }

        // Pay rent to the landowner
        public void PayRent(int rentAmount, Player landowner)
        {
            Balance -= rentAmount;
            landowner.ReceiveRent(rentAmount);
        }

        // Receive rent from another player
        public void ReceiveRent(int rentAmount)
        {
            Balance += rentAmount;
        }

        // Buy a property if the player has enough balance
        public void BuyProperty(Property property)
        {
            if (Balance >= property.PurchasePrice)
            {
                Balance -= property.PurchasePrice;
                OwnedProperties.Add(property);
                property.Owner = this;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("\n" + Name + " owned " + property.Name + " property.");
                Console.WriteLine(Name + "'s new Balance is: " + Balance + "Ꝟ");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n{Name} does not have enough money to buy {property.Name}.");
                Console.ResetColor();
            }
        }

        // Add a train tile to the player's owned trains
        public void AddTrains(Tile tile)
        {
            OwnedTrains.Add(tile);
        }

        // Add a utility tile to the player's owned utilities
        public void AddUtilities(Tile tile)
        {
            OwnedUtilities.Add(tile);
        }

        // Handle losing conditions
        public void Lose()
        {
            if (Balance < 0)
            {
                foreach (Property property in OwnedProperties)
                {
                    property.Owner = null;
                }
                foreach (TrainTile tile in OwnedTrains)
                {
                    tile.Owner = null;
                }
                foreach (UtilityTile tile in OwnedUtilities)
                {
                    tile.Owner = null;
                }
            }
        }

        // Display player information
        public void Display()
        {
            Console.ForegroundColor = Color;
            Console.WriteLine("Name: " + Name + " Balance: " + Balance + "Ꝟ Color: " + Color);
            Console.WriteLine("All Assets:");
            if (OwnedProperties != null)
            {
                foreach (Tile tile in OwnedProperties)
                {
                    Console.WriteLine(tile.Name);
                }
            }
            if (OwnedTrains != null)
            {
                foreach (Tile tile in OwnedTrains)
                {
                    Console.WriteLine(tile.Name);
                }
            }
            if (OwnedUtilities != null)
            {
                foreach (Tile tile in OwnedUtilities)
                {
                    Console.WriteLine(tile.Name);
                }
            }

            Console.ResetColor();


        }
    }
}
