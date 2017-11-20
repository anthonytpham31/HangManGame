using System;
using System.Collections.Generic;
using System.Linq;

namespace HangMan.IsolatedConsoleGame
{
    public class HangManConsoleGame
    {
        #region HangMan Fields

        private int _guessCounter;
        private bool _allowUserInput;
        private readonly string _hiddenWord;
        private readonly List<char> _correctGuesses = new List<char>();
        private readonly List<char> _incorrectGuesses = new List<char>();
        private readonly char[] _maskedWord;
        private readonly HangManArt _art;

        public HangManConsoleGame(string hiddenWord)
        {
            _guessCounter = 6;
            _allowUserInput = true;
            _hiddenWord = hiddenWord;
            _maskedWord = CreateMaskedWord();
            _art = new HangManArt();
        }
        #endregion

        #region HangMan Main Methods

        public void RunHangMan()
        {
            while (_allowUserInput)
            {
                DisplayArt();
                UserInput();
                DisplayGuesses();
            }

        }

        private void UserInput()
        {
            Console.WriteLine("Enter A Letter:");
            var userInput = Console.ReadLine()?.ToLower();

            Console.Clear();
            CheckUserInput(userInput);

        }

        #endregion

        #region HangMan Display Methods

        private void DisplayArt()
        {
            var maskedWord = string.Join(" ", _maskedWord.ToArray());
            Console.WriteLine(string.Join("", _art.CreateHangManImage(_guessCounter)));
            Console.WriteLine(maskedWord.ToUpper());
        }

        private void DisplayGuesses()
        {
            // This does not display guess when user Wins the game.
            if (!_allowUserInput) return;

            if (_incorrectGuesses.Count == 0) return;

            var incorrectGuesses = string.Join(" ", _incorrectGuesses.ToArray());

            Console.WriteLine("Previous Incorrect Guesses Were: " + incorrectGuesses.ToUpper());
        }

        private char[] CreateMaskedWord()
        {
            var maskedWord = new char[_hiddenWord.Length];
            for (var i = 0; i < _hiddenWord.Length; i++)
            {
                maskedWord[i] = '*';
            }

            return maskedWord;
        }

        private void ReplaceMaskedWord(string userInput)
        {
            for (var i = 0; i < _hiddenWord.Length; i++)
            {
                ReplaceMaskedLetter(_hiddenWord[i], userInput, i);
            }
        }

        private void ReplaceMaskedLetter(char hiddenLetter, string userInput, int i)
        {
            if (hiddenLetter != userInput.First()) return;
            _maskedWord[i] = userInput.First();
        }
        #endregion

        #region HangMan Check Methods

        private void CheckUserInput(string userInput)
        {
            if (IsUserInputInvalid(userInput)) return;
            if (HasLetterBeenGuessed(userInput)) return;
            CheckUserInputToHiddenWord(userInput);
        }

        private static bool IsUserInputInvalid(string userInput)
        {
            if (userInput.Length == 1 && userInput.All(char.IsLetter)) return false;
            Console.WriteLine("Please Enter Just A Letter.");
            return true;
        }

        private bool HasLetterBeenGuessed(string userInput)
        {
            if (!_correctGuesses.Contains(userInput[0]) && !_incorrectGuesses.Contains(userInput[0])) return false;
            Console.WriteLine(string.Format($"Already Guessed {userInput.ToUpper()}"));

            return true;
        }

        private void CheckUserInputToHiddenWord(string userInput)
        {
            if (_hiddenWord.Contains(userInput))
            {
                _correctGuesses.Add(userInput[0]);
                ReplaceMaskedWord(userInput);
                CheckUserWinConditions();

                return;
            }

            _incorrectGuesses.Add(userInput[0]);
            _guessCounter--;
            ReplaceMaskedWord(userInput);
            CheckUserLoseConditions();

        }

        private void CheckUserLoseConditions()
        {
            if (_guessCounter != 0) return;
            Console.WriteLine(string.Format($"You Lost! The correct answer was {_hiddenWord.ToUpper()}."));
            _allowUserInput = false;
        }

        private void CheckUserWinConditions()
        {
            var maskedWord = string.Join("", _maskedWord);
            if (maskedWord != _hiddenWord) return;
            Console.WriteLine(string.Format($"Congratulations, you won! It was {_hiddenWord.ToUpper()}."));
            _allowUserInput = false;
        }
        #endregion
    }
}