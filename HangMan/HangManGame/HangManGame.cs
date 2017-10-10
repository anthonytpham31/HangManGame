using System;
using System.Collections.Generic;
using System.Linq;

namespace HangMan
{
    public class HangManGame
    {
        #region HangMan Fields

        private int _lettersRevealed;
        private int _guessCounter;
        private bool _allowUserInput;
        private readonly string _hiddenWord;
        private List<char> _correctGuesses = new List<char>();
        private List<char> _incorrectGuesses = new List<char>();

        public HangManGame(string hiddenWord)
        {
            _lettersRevealed = 0;
            _guessCounter = 6;
            _allowUserInput = true;
            _hiddenWord = hiddenWord;
        }
        #endregion

        #region HangMan Main Methods

        public void RunHangMan()
        {
            while (_allowUserInput)
            {

                DisplayImageAndAllowUserInput();
            }

        }

        public void DisplayImageAndAllowUserInput()
        {
            var art = new HangManArt();
            var previousGuesses = string.Join(" ", _incorrectGuesses.ToArray());
            var imageDisplay = new string[7];
            var maskedWord = CreateMaskedWord(_hiddenWord);

            Console.WriteLine("Previous Incorrect Guesses Were: " + previousGuesses);
            Console.WriteLine(string.Join("", art.createHangManImage(_guessCounter, imageDisplay)));
            Console.WriteLine(maskedWord);

            UserInput();
        }

        public void UserInput()
        {
            Console.WriteLine("Enter A Letter:");
            var userInput = Console.ReadLine()?.ToLower();

            Console.Clear();
            CheckUserInput(userInput);
        }
        #endregion

        #region HangMan Check Methods

        private void CheckUserLoseConditions()
        {
            if (_guessCounter != 0) return;
            Console.WriteLine(string.Format($"You Lost! The Correct Answer Was {_hiddenWord}"));
            _allowUserInput = false;
        }

        private void CheckUserWinConditions()
        {
            if (_hiddenWord.Length != _lettersRevealed) return;
            Console.WriteLine("Congratulations, You Won!");
            _allowUserInput = false;
        }

        private void CheckUserInputToHiddenWord(string userInput)
        {
            if (_hiddenWord.Contains(userInput))
            {
                _correctGuesses.Add(userInput[0]);
                CheckUserWinConditions();
            }
            _incorrectGuesses.Add(userInput[0]);
            _guessCounter--;
            CheckUserLoseConditions();
        }

        private void CheckPreviousGuesses(string userInput)
        {
            if (_correctGuesses.Contains(userInput[0]) || _incorrectGuesses.Contains(userInput[0]))
            {
                Console.WriteLine(string.Format($"Already Guessed {userInput[0]}"));
            }
        }

        private static void CheckUserInputValidity(string userInput)
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
            var maskedWord = new char[hiddenWord.Length];

            for (var i = 0; i < maskedWord.Length; i++)
            {
                ReplaceMaskedLetter(hiddenWord[i], userInput);
            }

            return maskedWord;
        }

        public char ReplaceMaskedLetter(char hiddenLetter, string userInput)
        {
            if (hiddenLetter != userInput.First()) return '*';
            _lettersRevealed++;
            return hiddenLetter;
        }
        #endregion

    }
}
