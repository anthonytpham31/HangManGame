using System;

namespace HangMan
{
    class Program
    {
        static void Main(string[] args)
        {
            WordBank wordBank = new WordBank();

            bool repeat = true;
            while (repeat)
            {
                string hiddenWord = wordBank.getWord();
                HangManGame hangMan = new HangManGame(hiddenWord);
                hangMan.runHangMan();
                repeat = AskToPlayAgain();
            }

        }

        static bool AskToPlayAgain()
        {

            Console.WriteLine("Press Enter or Y to Play Again.");
            string repeatInput = Console.ReadLine().ToLower();
            if (repeatInput == "y" || repeatInput == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
