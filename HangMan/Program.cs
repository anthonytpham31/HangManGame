using System;

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
                var hangMan = new HangManGame.HangManGame(hiddenWord);

                hangMan.RunHangMan();
                repeat = AskToPlayAgain();
            }

        }

        private static bool AskToPlayAgain()
        {
            Console.WriteLine("Press Enter or Y to Play Again.");
            string repeatInput = Console.ReadLine()?.ToLower();
            if (repeatInput == "y" || repeatInput == "") return true;

            return false;
        }
    }

}
