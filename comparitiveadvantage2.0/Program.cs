using System;
using System.Collections.Generic;
using System.Linq;

namespace ComparitiveAdvantage
{
    class Program
    {
        static void Main(string[] args)
        {
        Begin:
            Console.WriteLine("Enter the Number of Players");
            int numberOfPlayers = int.Parse(Console.ReadLine()); // Takes the number of players participating from user

            Console.WriteLine("Enter Power of Your Team Players (space seperated)");
            string teamPowerRaw = Console.ReadLine(); // Takes the power of team players participating from user
            string[] teamPowerStringArray = teamPowerRaw.Split(' '); // Converts the powers into an array
            int[] teamPowerIntArray = Array.ConvertAll(teamPowerStringArray, int.Parse); // Converts the string array into an int array

            if (teamPowerIntArray.Count() != numberOfPlayers)
            {
                int playerDifference = numberOfPlayers - teamPowerIntArray.Count();
                Console.WriteLine("There are " + playerDifference + " team members missing Would you like to try again? (Y/N)");
                string answer1 = Console.ReadLine();
                if (answer1 == "Y")
                {
                    goto Begin;
                }
                else
                {
                    goto End;
                }
            }

            Console.WriteLine("Enter Power of Your Opponent Players (space seperated)");
            string opponentPowerRaw = Console.ReadLine(); // Takes the power of opponent players participating from user
            string[] opponentPowerStringArray = opponentPowerRaw.Split(' '); // Converts the powers into an array
            int[] opponentPowerIntArray = Array.ConvertAll(opponentPowerStringArray, int.Parse); // Converts the string array into an int array

            if (opponentPowerIntArray.Count() != numberOfPlayers)
            {
                int opponentDifference = numberOfPlayers - opponentPowerIntArray.Count();
                Console.WriteLine("There are " + opponentDifference + " team members missing, Would you like to try again? (Y/N)");
                string answer2 = Console.ReadLine();
                if (answer2 == "Y")
                {
                    goto Begin;
                }
                else
                {
                    goto End;
                }
            }

            int[] finalSolution = new int[numberOfPlayers];

            int[] teamPowerIntSorted = new int[numberOfPlayers];
            Array.Copy(teamPowerIntArray, teamPowerIntSorted, numberOfPlayers);
            Array.Sort(teamPowerIntSorted);

            int[] opponentPowerIntSorted = new int[numberOfPlayers];
            Array.Copy(opponentPowerIntArray, opponentPowerIntSorted, numberOfPlayers);
            Array.Sort(opponentPowerIntSorted);

            List<int> teamExtras = new List<int>();

            while (teamPowerIntSorted.Count() != 0)
            {
                int j = 0;

                while (teamPowerIntSorted[0] > opponentPowerIntSorted[j] && j < (opponentPowerIntSorted.Count() - 1))
                {
                    j++;
                }

                if (j == 0)
                {
                    teamExtras.Add(teamPowerIntSorted[0]);
                    teamPowerIntSorted = teamPowerIntSorted.Where((val, idx) => idx != 0).ToArray();
                }
                else
                {
                    int pos = Array.IndexOf(opponentPowerIntArray, opponentPowerIntSorted[(j - 1)]);
                    finalSolution[pos] = teamPowerIntSorted[0];

                    teamPowerIntSorted = teamPowerIntSorted.Where((val, idx) => idx != 0).ToArray();
                    opponentPowerIntSorted = opponentPowerIntSorted.Where((val, idx) => idx != (j - 1)).ToArray();
                }
            }

            int k = 0;
            while (finalSolution.Any(x => x == 0))
            {
                int extraIndex = Array.IndexOf(opponentPowerIntArray, opponentPowerIntSorted[0]);
                finalSolution[extraIndex] = teamExtras[k];

                opponentPowerIntSorted = opponentPowerIntSorted.Where((val, idx) => idx != 0).ToArray();
                k++;
            }

            int l = 1;
            foreach (int element in finalSolution)
            {
                Console.WriteLine("Match " + l + ": " + element + "\t vs \t" + opponentPowerIntArray[(l - 1)]);
                l++;
            }

            int win = 0;
            int loss = 0;
            int i = 0;

            while (win + loss != numberOfPlayers)
            {
                if (finalSolution[i] > opponentPowerIntArray[i])
                {
                    win++;
                }
                else if (finalSolution[i] <= opponentPowerIntArray[i])
                {
                    loss++;
                }
                i++;
            }

            Console.WriteLine("With the recommended configuration, your team will win " + win + " times and lose " + loss + " times.");
        End:
            Console.WriteLine("Do you want to quit? (Y/N)");
            string quitDecision = Console.ReadLine();

            if (quitDecision == "Y")
            {
                Environment.Exit(69);
            }
            else
            {
                goto Begin;
            }
        }
    }
}
