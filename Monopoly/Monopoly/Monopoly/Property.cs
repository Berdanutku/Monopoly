using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    // Represents a property tile inherits from Tile Class
    public class Property : Tile
    {
        // Property attributes
        public string PropName { get; private set; }
        public int PurchasePrice { get; private set; }
        public Player Owner { get; set; }
        public int Rent { get; private set; }
        public int HouseNumber;
        public int HotelNumber;
        public int HouseCost;
        public int HotelCost;

        // Constructor
        public Property(string name, string propName, int purchasePrice, int rent, int houseCost) : base(name)
        {
            PropName = propName;
            PurchasePrice = purchasePrice;
            Owner = null;
            Rent = rent;
            HouseNumber = 0;
            HotelNumber = 0;
            HouseCost = houseCost;
            HotelCost = HouseCost * 5;

        }

        // Empty constructor
        public Property(string name) : base(name)
        {
        }

        // Override method to handle Property actions
        public override void PerformAction(Player player)
        {
            base.PerformAction(player);
            Console.WriteLine("\nWelcome to " + Name + " property");
            if (Owner == null)
            {
                // If the property is not owned, offer the player the option to buy
                Console.WriteLine("The cost is: " + PurchasePrice + "Ꝟ" + "\nDo you want to buy this " + Name + " property");
                Console.WriteLine("1) Yes\n2) No");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        player.BuyProperty(this);
                        break;
                    case "2":
                        break;
                    default:
                        Console.WriteLine("Invalid Input");
                        break;
                }
            }
            else if (Owner != null)
            {
                if (Owner == player)
                {
                    // If the property is owned by the current player, offer building options
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("\nWelcome to your " + Name + " property");
                    Console.WriteLine("Do you want to build on this property?");
                    Console.WriteLine("1) Yes \n2) No");

                    String buildInput = Console.ReadLine();

                    switch (buildInput)
                    {
                        case "1":
                            Console.WriteLine("Do you want to build a house or a hotel?");
                            Console.WriteLine("1) House Cost:" + HouseCost + "Ꝟ" + "\n2) Hotel Cost:" + HotelCost + "Ꝟ" + "\n3) Cancel");

                            string buildTypeInput = Console.ReadLine();

                            switch (buildTypeInput)
                            {
                                case "1":
                                    BuildHouse(player);

                                    break;

                                case "2":
                                    BuildHotel(player);
                                    break;

                                case "3":
                                    break;

                                default:
                                    Console.WriteLine("Invalid Input!");
                                    break;
                            }
                            break;

                        case "2":
                            break;

                        default:
                            Console.WriteLine("Invalid Input!");
                            break;
                    } Console.ResetColor();
                }
                else
                {
                    // If the property is owned by another player, calculate and collect rent
                    Console.BackgroundColor = ConsoleColor.Red;
                    int rentAmount = CalculateRent();
                    Console.WriteLine("You landed on " + Name + " property owned by " + Owner.Name + ". Rent: " + rentAmount + "Ꝟ");
                    player.PayRent(rentAmount, Owner);
                    Console.WriteLine(player.Name + "'s new Balance is: " + player.GetBalance() + "Ꝟ");
                    Console.ResetColor();
                }
            }
        }

        // Method to build a house on the property
        private void BuildHouse(Player player)
        {
            if (player.GetBalance() >= HouseCost)
            {
                HouseNumber++;
                player.SetBalance(player.GetBalance() - HouseCost);
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(player.Name + " payed " + HouseCost + " Ꝟ for " + " build a house to " + Name + " property.");
                Console.WriteLine(player.Name + "'s new Balance is: " + player.GetBalance() + "Ꝟ");
                Console.ResetColor();
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(player.Name + " does not have enough money to build a house!");
                Console.ResetColor();
            }

        }

        // Method to build a hotel on the property
        private void BuildHotel(Player player)
        {
            if (player.GetBalance() >= HotelCost)
            {
                HotelNumber++;
                player.SetBalance(player.GetBalance() - HotelCost);
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(player.Name + " payed " + HotelCost + " Ꝟ for " + " build a hotel to " + Name + " property.");
                Console.WriteLine(player.Name + "'s new Balance is: " + player.GetBalance() + "Ꝟ");
                Console.ResetColor();
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(player.Name + " does not have enough money to build a hotel!");
                Console.ResetColor();
            }

        }

        // Rent calculation logic for different property groups
        private int CalculateRent()
        {
            if (Name == "Brown 1" || Name == "Brown 2")
            {
                switch (HouseNumber)
                {
                    case 0:
                        Rent = Rent;
                        break;
                    case 1:
                        Rent = 20;
                        break;
                    case 2:
                        Rent = 60;
                        break;
                    case 3:
                        Rent = 180;
                        break;
                    case 4:
                        Rent = 320;
                        break;
                }
                if (HotelNumber > 0) { Rent = 450; }
            }
            else if (Name == "Light Blue 1" || Name == "Light Blue 2" || Name == "Light Blue 3")
            {
                switch (HouseNumber)
                {
                    case 0:
                        Rent = Rent;
                        break;
                    case 1:
                        Rent = 40;
                        break;
                    case 2:
                        Rent = 100;
                        break;
                    case 3:
                        Rent = 300;
                        break;
                    case 4:
                        Rent = 450;
                        break;
                }
                if (HotelNumber > 0) { Rent = 600; }
            }
            else if (Name == "Purple 1" || Name == "Purple 2" || Name == "Purple 3")
            {
                switch (HouseNumber)
                {
                    case 0:
                        Rent = Rent;
                        break;
                    case 1:
                        Rent = 60;
                        break;
                    case 2:
                        Rent = 180;
                        break;
                    case 3:
                        Rent = 500;
                        break;
                    case 4:
                        Rent = 700;
                        break;
                }
                if (HotelNumber > 0) { Rent = 900; }
            }
            else if (Name == "Orange 1" || Name == "Orange 2" || Name == "Orange 3")
            {
                switch (HouseNumber)
                {
                    case 0:
                        Rent = Rent;
                        break;
                    case 1:
                        Rent = 80;
                        break;
                    case 2:
                        Rent = 220;
                        break;
                    case 3:
                        Rent = 600;
                        break;
                    case 4:
                        Rent = 800;
                        break;
                }
                if (HotelNumber > 0) { Rent = 1000; }
            }
            else if (Name == "Red 1" || Name == "Red 2" || Name == "Red 3")
            {
                switch (HouseNumber)
                {
                    case 0:
                        Rent = Rent;
                        break;
                    case 1:
                        Rent = 100;
                        break;
                    case 2:
                        Rent = 300;
                        break;
                    case 3:
                        Rent = 750;
                        break;
                    case 4:
                        Rent = 925;
                        break;
                }
                if (HotelNumber > 0) { Rent = 1100; }
            }
            else if (Name == "Yellow 1" || Name == "Yellow 2" || Name == "Yellow 3")
            {
                switch (HouseNumber)
                {
                    case 0:
                        Rent = Rent;
                        break;
                    case 1:
                        Rent = 120;
                        break;
                    case 2:
                        Rent = 360;
                        break;
                    case 3:
                        Rent = 850;
                        break;
                    case 4:
                        Rent = 1025;
                        break;
                }
                if (HotelNumber > 0) { Rent = 1200; }
            }
            else if (Name == "Green 1" || Name == "Green 2" || Name == "Green 3")
            {
                switch (HouseNumber)
                {
                    case 0:
                        Rent = Rent;
                        break;
                    case 1:
                        Rent = 150;
                        break;
                    case 2:
                        Rent = 450;
                        break;
                    case 3:
                        Rent = 1000;
                        break;
                    case 4:
                        Rent = 1200;
                        break;
                }
                if (HotelNumber > 0) { Rent = 1400; }
            }
            else if (Name == "Blue 1" || Name == "Blue 2" || Name == "Blue 3")
            {
                switch (HouseNumber)
                {
                    case 0:
                        Rent = Rent;
                        break;
                    case 1:
                        Rent = 200;
                        break;
                    case 2:
                        Rent = 600;
                        break;
                    case 3:
                        Rent = 1400;
                        break;
                    case 4:
                        Rent = 1700;
                        break;
                }
                if (HotelNumber > 0) { Rent = 2000; }
            }
            return Rent;
        }

    }
}


