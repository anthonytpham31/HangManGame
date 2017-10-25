using System;
using System.Collections.Generic;
using System.Linq;

namespace HangMan
{
    public class HangManConsoleGame
    {
        #region HangMan Fields

        private int _LettersRevealed;
        private int _GuessCounter;
        private bool _AllowUserInput;
        private string _HiddenWord;
        private List<char> _CorrectGuesses = new List<char>();
        private List<char> _IncorrectGuesses = new List<char>();
        private char[] _MaskedWord;
        private HangManArt _Art;

        public HangManConsoleGame(string hiddenWord)
        {
            _LettersRevealed = 0;
            _GuessCounter = 6;
            _AllowUserInput = true;
            _HiddenWord = hiddenWord;
            _MaskedWord = CreateMaskedWord();
            _Art = new HangManArt();
        }
        #endregion

        #region HangMan Main Methods

        public void RunHangMan()
        {
            while (_AllowUserInput)
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
            var maskedWord = string.Join(" ", _MaskedWord.ToArray());
            Console.WriteLine(string.Join("", _Art.CreateHangManImage(_GuessCounter)));
            Console.WriteLine(maskedWord.ToUpper());
        }

        private void DisplayGuesses()
        {
            // This does not display guess when user Wins the game.
            if (!_AllowUserInput) return;

            var incorrectGuesses = string.Join(" ", _IncorrectGuesses.ToArray());

            Console.WriteLine("Previous Incorrect Guesses Were: " + incorrectGuesses.ToUpper());
        }

        private char[] CreateMaskedWord()
        {
            char[] maskedWord = new char[_HiddenWord.Length];
            for (var i = 0; i < _HiddenWord.Length; i++)
            {
                maskedWord[i] = '*';
            }

            return maskedWord;
        }

        private void ReplaceMaskedWord(string userInput)
        {
            for (int i = 0; i < _HiddenWord.Length; i++)
            {
                ReplaceMaskedLetter(_HiddenWord[i], userInput, i);
            }
        }

        private void ReplaceMaskedLetter(char hiddenLetter, string userInput, int i)
        {
            if (hiddenLetter != userInput.First()) return;
            _LettersRevealed++;
            _MaskedWord[i] = userInput.First();
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
            if (userInput.Length != 1 || userInput.Any(x => !char.IsLetter(x)))
            {
                Console.WriteLine("Please Enter Just A Letter.");
                return true;
            }
            return false;
        }

        private bool HasLetterBeenGuessed(string userInput)
        {
            if (_CorrectGuesses.Contains(userInput[0]) || _IncorrectGuesses.Contains(userInput[0]))
            {
                Console.WriteLine(string.Format($"Already Guessed {userInput.ToUpper()}"));

                return true;
            }
            return false;
        }

        private void CheckUserInputToHiddenWord(string userInput)
        {
            if (_HiddenWord.Contains(userInput))
            {
                _CorrectGuesses.Add(userInput[0]);
                ReplaceMaskedWord(userInput);
                CheckUserWinConditions();

                return;
            }

            _IncorrectGuesses.Add(userInput[0]);
            _GuessCounter--;
            ReplaceMaskedWord(userInput);
            CheckUserLoseConditions();

        }

        private void CheckUserLoseConditions()
        {
            if (_GuessCounter != 0) return;
            Console.WriteLine(string.Format($"You Lost! The correct answer was {_HiddenWord.ToUpper()}."));
            _AllowUserInput = false;
        }

        private void CheckUserWinConditions()
        {
            var maskedWord = string.Join("", _MaskedWord);
            if (maskedWord != _HiddenWord) return;
            Console.WriteLine(string.Format($"Congratulations, you won! It was {_HiddenWord.ToUpper()}."));
            _AllowUserInput = false;
        }
        #endregion
    }
}