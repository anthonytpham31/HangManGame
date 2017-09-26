using System;
using System.Collections.Generic;
using System.Linq;

namespace HangMan
{
    public class HangManGame
    {
        #region HangMan Fields

        private int lettersRevealed;

        private int guessCounter;

        private bool winCondition;

        private string hiddenWord;

        private List<char> correctGuesses = new List<char>();

        private List<char> incorrectGuesses = new List<char>();

        public HangManGame(string hiddenWord)
        {
            lettersRevealed = 0;
            guessCounter = 6;
            winCondition = false;
            this.hiddenWord = hiddenWord;
        }
        #endregion

        #region HangMan Main Methods

        public void RunHangMan()
        {
            while (!winCondition)
            {
                char[] maskedWord = CreateMaskedWord(hiddenWord);
                DisplayImageAndGuesses(maskedWord);
            }

        }

        public void DisplayImageAndGuesses(char[] maskedWord)
        {
            HangManArt art = new HangManArt();
            string previousGuesses = string.Join(" ", incorrectGuesses.ToArray());
            string[] imageDisplay = new string[7];

            Console.WriteLine("Previous Incorrect Guesses Were: " + previousGuesses);
            Console.WriteLine(String.Join("", art.createHangManImage(guessCounter, imageDisplay)));
            Console.WriteLine(maskedWord);

            UserInput();
        }

        public void UserInput()
        {
            Console.WriteLine("Enter A Letter:");
            string userInput = Console.ReadLine().ToLower();

            Console.Clear();
            CheckUserInputValidity(userInput);
            CheckPreviousGuesses(userInput);
        }
        #endregion

        #region HangMan Check Methods

        private bool CheckUserLoseConditions()
        {
            if (guessCounter == 0) return true;

            return false;
        }

        private bool CheckUserWinConditions()
        {
            if (hiddenWord.Length == lettersRevealed) return true;
            if (winCondition) return true;

            return false;
        }

        private bool CheckUserInputToHiddenWord(string userInput)
        {
            if (hiddenWord.Contains(userInput))
            {
                correctGuesses.Add(userInput[0]);
                return true;
            }
            else
            {
                incorrectGuesses.Add(userInput[0]);
                guessCounter--;
                return false;
            }
        }

        private void CheckPreviousGuesses(string userInput)
        {
            if (correctGuesses.Contains(userInput[0]) || incorrectGuesses.Contains(userInput[0]))
            {
                Console.WriteLine(String.Format($"Already Guessed {userInput[0]}"));
            }
        }

        private void CheckUserInputValidity(string userInput)
        {
            if (userInput.Length != 1 || userInput.Any(x => !char.IsLetter(x)))
            {
                Console.WriteLine("Please Enter Just A Letter");
            }
        }
        #endregion

        #region Hangman Helper Methods

        public char[] CreateMaskedWord(string hiddenWord)
        {
            char[] maskedWord = new char[hiddenWord.Length];

            for (int i = 0; i < maskedWord.Length; i++)
            {
                maskedWord[i] = '*';
            }

            return maskedWord;
        }

        public char[] ReplaceMaskedWord(string hiddenWord, string userInput, char[] replaceMasks)
        {
            for (int i = 0; i < hiddenWord.Length; i++)
            {
                if (hiddenWord[i] == userInput.First())
                {
                    replaceMasks[i] = userInput.First();
                    lettersRevealed++;
                }

            }
            return replaceMasks;
        }
        #endregion

    }
}
