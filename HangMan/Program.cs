﻿using System;
using HangMan.HangManWords;
using HangMan.HangManGame;

namespace HangMan
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var repeat = true;
            while (repeat)
            {
                var wordBank = new WordBank();
                var hiddenWord = wordBank.GetWord();
                var hangMan = new MainGame(hiddenWord);

                hangMan.RunHangMan();
                repeat = AskToPlayAgain();
            }

        }

        private static bool AskToPlayAgain()
        {
            Console.WriteLine("Press Enter or Y to Play Again.");
            var repeatInput = Console.ReadLine()?.ToLower();
            return repeatInput == "y" || repeatInput == "";
        }
    }

}
