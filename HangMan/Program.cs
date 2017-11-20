using System;
using HangMan.IsolatedConsoleGame;

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
                var hangMan = new HangManConsoleGame(hiddenWord);

                hangMan.RunHangMan();
                repeat = AskToPlayAgain();
            }

        }

        private static bool AskToPlayAgain()
        {
            Console.WriteLine("Press Enter or Y to Play Again.");
            var repeatInput = Console.ReadLine()?.ToLower();
            Console.Clear();
            return repeatInput == "y" || repeatInput == "";
        }
    }

}
