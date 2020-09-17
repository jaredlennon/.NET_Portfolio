using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                PlayTheGame();
            }
            while (PlayAgain());           
        }

        static void PlayTheGame()
        { 
            int rounds;
            int i;
            int userWins = 0;
            int computerWins = 0;
            int ties = 0;

            Console.WriteLine("Welcome to the Rock, Paper, Scissors Challenge!");

            Console.Write("How many rounds would you like to play? (1-10): ");
            rounds = Convert.ToInt32(Console.ReadLine());


            if (rounds > 0 && rounds < 11)
            {
                Console.WriteLine("Okay, great! We will play " + rounds + " rounds...");

                for (i = 1; i <= rounds; i++)
                {

                    Random randomComputerSelection = new Random();
                    int randomCompSelection = randomComputerSelection.Next(1, 4);

                    Console.WriteLine();
                    Console.WriteLine("ROUND " + i);
                    Console.Write("Enter your choice (1 for Rock, 2 for Paper, or 3 for Scissors): ");
                    int userSelection = Convert.ToInt32(Console.ReadLine());
                    
                    // Console.WriteLine("Computer Random choice: " + randomCompSelection); // check random variable

                    // POSSIBLE OUTCOMES PER ROUND
                    if (userSelection == 1 && randomCompSelection == 3)
                    {
                        Console.WriteLine("USER WINS!");
                        userWins++;
                    }
                    else if (userSelection == 1 && randomCompSelection == 2)
                    {
                        Console.WriteLine("COMPUTER WINS!");
                        computerWins++;
                    }
                    else if (userSelection == 1 && randomCompSelection == 1)
                    {
                        Console.WriteLine("IT'S A TIE!");
                        ties++;
                    }
                    else if (userSelection == 2 && randomCompSelection == 1)
                    {
                        Console.WriteLine("USER WINS!");
                        userWins++;
                    }
                    else if (userSelection == 2 && randomCompSelection == 2)
                    {
                        Console.WriteLine("IT'S A TIE!");
                        ties++;
                    }
                    else if (userSelection == 2 && randomCompSelection == 3)
                    {
                        Console.WriteLine("COMPUTER WINS!");
                        computerWins++;
                    }
                    else if (userSelection == 3 && randomCompSelection == 1)
                    {
                        Console.WriteLine("COMPUTER WINS!");
                        computerWins++;
                    }
                    else if (userSelection == 3 && randomCompSelection == 2)
                    {
                        Console.WriteLine("USER WINS!");
                        userWins++;
                    }
                    else if (userSelection == 3 && randomCompSelection == 3)
                    {
                        Console.WriteLine("IT'S A TIE!");
                        ties++;
                    }
                    else
                    {
                        Console.WriteLine("ERROR: " + userSelection + " is an invalid Entry... COMPUTER WINS!");
                        computerWins++;
                    }

                }

                // SCORE TOTALS & FINAL WINNER
                Console.WriteLine();
                Console.WriteLine("Let's tally up the score...");
                Console.WriteLine("User wins: " + userWins);
                Console.WriteLine("Computer wins: " + computerWins);
                Console.WriteLine("Ties: " + ties);
                Console.WriteLine();

                if (userWins > computerWins)
                {
                    Console.WriteLine("FINAL WINNER: USER!");
                }
                else if (userWins < computerWins)
                {
                    Console.WriteLine("FINAL WINNER: COMPUTER!");
                }
                else if (userWins == computerWins)
                {
                    Console.WriteLine("FINAL WINNER: TIE GAME!");
                }

                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("ERROR: you are only allowed to play between 1 and 10 rounds.");
                Console.WriteLine();
            }

        }

        static public bool PlayAgain()
        {
            while(true)
            {
                Console.Write("Do you want to play again (y/n)? ");
                string playAgainAnswer = Console.ReadLine();
                string answerAdjusted = playAgainAnswer.ToLower();

                Console.WriteLine();

                if (answerAdjusted == "y")
                    return true;
                if (answerAdjusted == "n")
                    Console.WriteLine("Thanks for playing!");
                    return false;
            }
        }

    }
}