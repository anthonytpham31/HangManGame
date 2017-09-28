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

        private bool allowUserInput;

        private string hiddenWord;

        private List<char> correctGuesses = new List<char>();

        private List<char> incorrectGuesses = new List<char>();

        public HangManGame(string hiddenWord)
        {
            lettersRevealed = 0;
            guessCounter = 6;
            allowUserInput = true;
            this.hiddenWord = hiddenWord;
        }
        #endregion

        #region HangMan Main Methods

        public void RunHangMan()
        {
            while (allowUserInput)
            {

                DisplayImageAndAllowUserInput();
            }

        }

        public void DisplayImageAndAllowUserInput()
        {
            HangManArt art = new HangManArt();
            string previousGuesses = string.Join(" ", incorrectGuesses.ToArray());
            string[] imageDisplay = new string[7];
            char[] maskedWord = CreateMaskedWord(hiddenWord);

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
            CheckUserInput(userInput);
        }
        #endregion

        #region HangMan Check Methods

        private void CheckUserLoseConditions(string hiddenWord)
        {
            if (guessCounter == 0)
            {
                Console.WriteLine(string.Format($"You Lost! The Correct Answer Was {hiddenWord}"));
                allowUserInput = false;
            }
        }

        private void CheckUserWinConditions()
        {
            if (hiddenWord.Length == lettersRevealed)
            {
                Console.WriteLine("Congratulations, You Won!");
                allowUserInput = false;
            }
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

        public void CheckUserInput(string userInput)
        {
            CheckUserInputValidity(userInput);
            CheckPreviousGuesses(userInput);
            CheckUserInputToHiddenWord(userInput);
        }

        public char[] CreateMaskedWord(string hiddenWord, string userInput = " ")
        {
            char[] maskedWord = new char[hiddenWord.Length];

            for (int i = 0; i < maskedWord.Length; i++)
            {
                ReplaceMaskedLetter(hiddenWord[i], userInput);
            }

            return maskedWord;
        }

        public char ReplaceMaskedLetter(char hiddenLetter, string userInput)
        {
            if (hiddenLetter == userInput.First())
            {
                lettersRevealed++;
                return hiddenLetter;
            }

            return '*';
        }
        #endregion

    }
}
