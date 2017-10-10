using System;
using System.Collections.Generic;
using System.Linq;
using HangMan.ASCII_Art;

namespace HangMan.HangManGame
{
    public class MainGame
    {
        public void RunHangMan()
        {
            var wordBank = new WordBank();
            var createArt = new HangManArt();
            var repeat = true;
            while (repeat)
            {
                Console.Clear();
                string hiddenWord = wordBank.GetWord().ToLower();
                HangMan.HangManGame.HangManGame createGame = new HangMan.HangManGame.HangManGame(hiddenWord);


                bool won = false;
                int lettersRevealed = 0;
                int guessCounter = 6;
                string[] imageDisplay = new string[7];

                char[] hang = new char[hiddenWord.Length];
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

                    Console.WriteLine(String.Join("", createArt.CreateHangManImage(guessCounter)));
                    Console.WriteLine(hang);

                    Console.WriteLine("Enter A Letter:");
                    string userInput = Console.ReadLine().ToLower();

                    Console.Clear();

                    if (userInput.Length != 1 || userInput.Any(x => !char.IsLetter(x)))
                    {
                        Console.WriteLine("Please Enter Just A Letter");
                        continue;
                    }
                    else
                    {
                        if (correctGuesses.Contains(userInput[0]))
                        {
                            Console.WriteLine("Already Guessed {0}", userInput[0]);
                            continue;
                        }
                        if (incorrectGuesses.Contains(userInput[0]))
                        {
                            Console.WriteLine("Already Guessed {0}", userInput[0]);
                            continue;
                        }
                        if (hiddenWord.Contains(userInput))
                        {
                            correctGuesses.Add(userInput[0]);

                            for (int i = 0; i < hiddenWord.Length; i++)
                            {
                                if (hiddenWord[i] == userInput[0])
                                {
                                    hang[i] = userInput[0];
                                    lettersRevealed++;
                                }

                            }
                            if (lettersRevealed == hiddenWord.Length)
                            {
                                won = true;
                            }
                        }
                        else
                        {
                            incorrectGuesses.Add(userInput[0]);
                            Console.WriteLine("No {0} in this word!", userInput);
                            guessCounter--;
                        }

                    }

                }
                if (won)
                {
                    Console.WriteLine("You Win! It was {0}", hiddenWord);
                }
                else
                {
                    Console.WriteLine(String.Join("", createArt.CreateHangManImage(guessCounter)));
                    Console.WriteLine("You Lost! It was {0}", hiddenWord);
                }


            }

        }

        public bool AskToPlayAgain(bool repeat)
        {
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
            return repeat;
        }
    }
}
