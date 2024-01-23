using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Monopoly
{
    internal class Program
    {
        // Game state variables
        public static Board board = new Board();
        public static List<Player> players = new List<Player>();
        public static List<Winner> winners = new List<Winner>();

        static void Main(string[] args)
        {
            // Display previous winners
            DisplayWinners();

            // Set up players
            // Determine order and start the game
            SetPlayers();
            DeterminePlayerOrder();
            Play();

        }

        // Menu for player actions
        public static void Menu(Player player)
        {
            while (true)
            {
                Console.ForegroundColor = player.GetColor();
                Console.WriteLine("\n\n" + player.Name + "'s turn, Balance: " + player.GetBalance() + "Ꝟ");
                Console.ResetColor();
                Console.WriteLine("1) Display assets");
                Console.WriteLine("2) Roll Dice");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        player.Display();
                        break;
                    case "2":
                        board.MovePlayer(player);
                        return;
                    default:
                        Console.WriteLine("Invalid Input!");
                        break;
                }
            }
        }

        // Set up players
        static void SetPlayers()
        {
            while (true)
            {
                Console.WriteLine("Welcome to Monopoly!");
                Console.WriteLine("How many players will play?");
                int playerNumber;
                if (int.TryParse(Console.ReadLine(), out playerNumber) && playerNumber >= 2 && playerNumber <= 4)
                {
                    for (int i = 1; i <= playerNumber; i++)
                    {
                        Console.WriteLine($"Enter name of player {i}:");
                        string playerName = Console.ReadLine();
                        ConsoleColor playerColor = GetPlayerColor(i);

                        Player player = new Player(playerName, playerColor);
                        players.Add(player);
                    }

                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Input! Please enter a number between 2 and 4.\n");
                    Console.ResetColor();
                }

                // Method to get player color based on index
                ConsoleColor GetPlayerColor(int playerIndex)
                {
                    ConsoleColor[] colors = { ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Green, ConsoleColor.Blue };
                    return colors[playerIndex - 1];
                }
            }
        }

        // Determine the order of players based on a single die roll
        static void DeterminePlayerOrder()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\nDetermining Player Order:");
            Console.ResetColor();

            // Roll a single die for each player
            foreach (Player player in players)
            {
               player.SingleDieScore = player.RollSingleDie();
            }

            // Order players based on the single die scores
            players = players.OrderByDescending(player => player.SingleDieScore).ToList();

            // Display the player order
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\nPlayer Order:");
            Console.ResetColor();
            for (int i = 0; i < players.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {players[i].Name} - Single Die Score: {players[i].SingleDieScore}");
            }

            Console.WriteLine("\nPress enter to continue...");
            Console.ReadKey();
            Console.Clear();
        }


        // Main game loop
        static void Play()
        {
            while (true)
            {
                int startingPlayerIndex = 0;

                // Iterate through players in order
                for (int i = startingPlayerIndex; i < players.Count + startingPlayerIndex; i++)
                {
                    int playerIndex = i % players.Count;
                    Player p = players[playerIndex];

                    // Display the game board and player menu
                    board.DisplayBoard();
                    Menu(p);


                    // Check if the player has lost
                    if (p.GetBalance() < 0)
                    {
                        p.Lose();
                        players.RemoveAt(playerIndex);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\t" + p.Name + " Lost!");
                        Console.ResetColor();
                        i--;
                    }

                    Console.WriteLine("\nPress enter to continue...");
                    Console.ReadLine();
                    Console.Clear();

                    // Check for game over
                    if (players.Count <= 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Game Over!");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(players[0].Name + " won the game!");
                        Winner winner = new Winner
                        {
                            Name = players[0].Name,
                            Balance = players[0].GetBalance(),
                            DateTime = DateTime.Now
                        };

                        // Read existing winners
                        List<Winner> winners = new List<Winner>();
                        if (File.Exists("Winners.json"))
                        {
                            string jsonString = File.ReadAllText("Winners.json");

                            if (!string.IsNullOrWhiteSpace(jsonString))
                            {
                                winners = JsonConvert.DeserializeObject<List<Winner>>(jsonString);
                            }
                        }

                        // Add the new winner to the list
                        winners.Add(winner);

                        // Serialize and write the entire list of winners to the file
                        string json = JsonConvert.SerializeObject(winners);
                        File.WriteAllText("Winners.json", json);

                        Console.ResetColor();
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }
            }
        }

        // Winner class to store information of Winners
        public class Winner
        {
            public string Name { get; set; }
            public int Balance { get; set; }
            public DateTime DateTime { get; set; }
        }

        // Display previous winners
        static void DisplayWinners()
        {
            try
            {

                Console.BackgroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("\t\tPrevious Winners:\n");
                // Read the JSON string from a file
                string jsonString = File.ReadAllText("Winners.json");

                if (string.IsNullOrWhiteSpace(jsonString))
                {
                    Console.WriteLine("No winners recorded yet.\n");
                    Console.ResetColor();
                    return;
                }

                // Deserialize the JSON string back into a list of Winner objects
                List<Winner> deserializedWinners = JsonConvert.DeserializeObject<List<Winner>>(jsonString);

                // Access and display information about each winner
                foreach (Winner winner in deserializedWinners)
                {
                    Console.WriteLine($"Name: {winner.Name}, Balance: {winner.Balance}Ꝟ, Date: {winner.DateTime}\n");
                }
                Console.ResetColor();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("No winners recorded yet.\n");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading winners: {ex.Message}");
                Console.ResetColor();
            }
        }

    }

}




