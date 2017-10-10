using System;
using System.Collections.Generic;
using System.Linq;
using HangMan.ASCII_Art;

namespace HangMan.HangManGame
{
    public class MainGame
    {
        #region HangMan Fields

        private int _lettersRevealed;
        private int _guessCounter;
        private bool _allowUserInput;
        private readonly string _hiddenWord;
        private List<char> _correctGuesses = new List<char>();
        private List<char> _incorrectGuesses = new List<char>();

        public MainGame(string hiddenWord)
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
            // TODO: This needs to be split out
            var art = new HangManArt();
            var previousGuesses = string.Join(" ", _incorrectGuesses.ToArray());
            var maskedWord = CreateMaskedWord(_hiddenWord);

            Console.WriteLine("Previous Incorrect Guesses Were: " + previousGuesses);
            Console.WriteLine(string.Join("", art.CreateHangManImage(_guessCounter)));
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
        // TODO: Fix Masked Words Orderings
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

        #region HangMan Check Methods

        private void CheckUserLoseConditions()
        {
            if (_guessCounter != 0) return;
            Console.WriteLine(string.Format($"You Lost! The correct answer was {_hiddenWord}."));
            _allowUserInput = false;
        }

        private void CheckUserWinConditions()
        {
            if (_hiddenWord.Length != _lettersRevealed) return;
            Console.WriteLine(string.Format($"Congratulations, you won! It was {_hiddenWord}."));
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
                Console.WriteLine(string.Format($"Already Guessed {userInput}"));
            }
        }

        private static void CheckUserInputValidity(string userInput)
        {
            if (userInput.Length != 1 || userInput.Any(x => !char.IsLetter(x)))
            {
                Console.WriteLine("Please Enter Just A Letter");
            }
        }

        public void CheckUserInput(string userInput)
        {
            CheckUserInputValidity(userInput);
            CheckPreviousGuesses(userInput);
            CheckUserInputToHiddenWord(userInput);
        }
        #endregion
    }
}
