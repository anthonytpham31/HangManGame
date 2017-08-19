using System;
using System.Collections.Generic;
using System.Linq;

namespace HangMan
{
    public class MainGame
    {

        private string stringDisplay;
        private WordBank wordBank = new WordBank();
        private HangManArt createArt = new HangManArt();



        public void runHangMan()
        {
            bool repeat = true;
            while (repeat)
            {
                Console.Clear();
                string wordToGuess = wordBank.getWord().ToLower();

                bool won = false;
                int lettersRevealed = 0;
                int guessCounter = 6;
                string[] imageDisplay = new string[7];

                char[] hang = new char[wordToGuess.Length];
                List<char> correctGuesses = new List<char>();
                List<char> incorrectGuesses = new List<char>();

                for (int i = 0; i < hang.Length; i++)
                {
                    hang[i] = '*';
                }

                while (!won && guessCounter > 0)
                {
                    string inc = string.Join(" ", incorrectGuesses.ToArray());
                    Console.WriteLine("Previous Incorrect Guesses Were: " + inc);

                    Console.WriteLine(String.Join("", createArt.createHangManImage(guessCounter, imageDisplay)));
                    Console.WriteLine(hang);

                    Console.WriteLine("Enter A Letter:");
                    string input = Console.ReadLine().ToLower();

                    Console.Clear();

                    if (input.Length != 1 || input.Any(x => !char.IsLetter(x)))
                    {
                        Console.WriteLine("Please Enter Just A Letter");
                        continue;
                    }
                    else
                    {
                        if (correctGuesses.Contains(input[0]))
                        {
                            Console.WriteLine("Already Guessed {0}", input[0]);
                            continue;
                        }
                        if (incorrectGuesses.Contains(input[0]))
                        {
                            Console.WriteLine("Already Guessed {0}", input[0]);
                            continue;
                        }
                        if (wordToGuess.Contains(input))
                        {
                            correctGuesses.Add(input[0]);

                            for (int i = 0; i < wordToGuess.Length; i++)
                            {
                                if (wordToGuess[i] == input[0])
                                {
                                    hang[i] = input[0];
                                    lettersRevealed++;
                                }

                            }
                            if (lettersRevealed == wordToGuess.Length)
                            {
                                won = true;
                            }
                        }
                        else
                        {
                            incorrectGuesses.Add(input[0]);
                            Console.WriteLine("No {0} in this word!", input);
                            guessCounter--;
                        }

                    }

                }
                if (won)
                {
                    Console.WriteLine("You Win! It was {0}", wordToGuess);
                }
                else
                {
                    Console.WriteLine(String.Join("", createArt.createHangManImage(guessCounter, imageDisplay)));
                    Console.WriteLine("You Lost! It was {0}", wordToGuess);
                }
                Console.WriteLine("Do You Want to Play Again (Y/N)?");
                char go = Console.ReadLine().ToCharArray()[0];
                if (char.ToLower(go) == 'y')
                {
                    repeat = true;
                }
                else
                {
                    repeat = false;
                }

            }

        }
    }
}
