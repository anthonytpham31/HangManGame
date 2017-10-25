namespace HangMan
{
    public class HangManArt
    {
        // Easier to create ASCII Art
        private const string Image1 = "__________    \n";
        private const string Image2 = "|/       |    \n";
        private const string Image3 = "|             \n";
        private const string Image4 = "|             \n";
        private const string Image5 = "|             \n";
        private const string Image6 = "|             \n";
        private const string Image7 = "|             \n";
        private const string WrongGuess1 = "|        O    \n"; // image3
        private const string WrongGuess2 = "|       /     \n"; // image4
        private const string WrongGuess3 = "|       /|    \n"; // image4
        private const string WrongGuess4 = "|       /|\\  \n"; // image4
        private const string WrongGuess5 = "|       /     \n"; // image5
        private const string WrongGuess6 = "|       / \\  \n"; // image5

        public string[] CreateHangManImage(int guessCounter)
        {
            var imageDisplay = new string[7];


            switch (guessCounter)
            {
                case 5:
                    imageDisplay[0] = Image1;
                    imageDisplay[1] = Image2;
                    imageDisplay[2] = Image3;
                    imageDisplay[3] = WrongGuess1;
                    imageDisplay[4] = Image5;
                    imageDisplay[5] = Image6;
                    imageDisplay[6] = Image7;
                    break;
                case 4:
                    imageDisplay[0] = Image1;
                    imageDisplay[1] = Image2;
                    imageDisplay[2] = Image3;
                    imageDisplay[3] = WrongGuess1;
                    imageDisplay[4] = WrongGuess2;
                    imageDisplay[5] = Image6;
                    imageDisplay[6] = Image7;
                    break;
                case 3:
                    imageDisplay[0] = Image1;
                    imageDisplay[1] = Image2;
                    imageDisplay[2] = Image3;
                    imageDisplay[3] = WrongGuess1;
                    imageDisplay[4] = WrongGuess3;
                    imageDisplay[5] = Image6;
                    imageDisplay[6] = Image7;
                    break;
                case 2:
                    imageDisplay[0] = Image1;
                    imageDisplay[1] = Image2;
                    imageDisplay[2] = Image3;
                    imageDisplay[3] = WrongGuess1;
                    imageDisplay[4] = WrongGuess4;
                    imageDisplay[5] = Image6;
                    imageDisplay[6] = Image7;
                    break;
                case 1:
                    imageDisplay[0] = Image1;
                    imageDisplay[1] = Image2;
                    imageDisplay[2] = Image3;
                    imageDisplay[3] = WrongGuess1;
                    imageDisplay[4] = WrongGuess4;
                    imageDisplay[5] = WrongGuess5;
                    imageDisplay[6] = Image7;
                    break;
                case 0:
                    imageDisplay[0] = Image1;
                    imageDisplay[1] = Image2;
                    imageDisplay[2] = Image3;
                    imageDisplay[3] = WrongGuess1;
                    imageDisplay[4] = WrongGuess4;
                    imageDisplay[5] = WrongGuess6;
                    imageDisplay[6] = Image7;
                    break;
                default:
                    imageDisplay[0] = Image1;
                    imageDisplay[1] = Image2;
                    imageDisplay[2] = Image3;
                    imageDisplay[3] = Image4;
                    imageDisplay[4] = Image5;
                    imageDisplay[5] = Image6;
                    imageDisplay[6] = Image7;
                    break;
            }

            return imageDisplay;
        }
    }
}
