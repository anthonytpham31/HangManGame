using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMan
{
    public class HangManArt
    {
        // Easier to create ASCII Art
        const string image1 = "__________    \n";
        const string image2 = "|/       |    \n";
        const string image3 = "|             \n";
        const string image4 = "|             \n";
        const string image5 = "|             \n";
        const string image6 = "|             \n";
        const string image7 = "|             \n";
        const string wrongGuess1 = "|        O    \n"; // image3
        const string wrongGuess2 = "|       /     \n"; // image4
        const string wrongGuess3 = "|       /|    \n"; // image4
        const string wrongGuess4 = "|       /|\\  \n"; // image4
        const string wrongGuess5 = "|       /     \n"; // image5
        const string wrongGuess6 = "|       / \\  \n"; // image5

        public string[] createHangManImage(int guessCounter, string[] imageDisplay)
        {
            imageDisplay[0] = image1;
            imageDisplay[1] = image2;
            imageDisplay[2] = image3;
            imageDisplay[3] = image4;
            imageDisplay[4] = image5;
            imageDisplay[5] = image6;
            imageDisplay[6] = image7;
            if (guessCounter == 5)
            {
                imageDisplay[3] = wrongGuess1;
            }
            if (guessCounter == 4)
            {
                imageDisplay[3] = wrongGuess1;
                imageDisplay[4] = wrongGuess2;
            }
            if (guessCounter == 3)
            {
                imageDisplay[3] = wrongGuess1;
                imageDisplay[4] = wrongGuess3;
            }
            if (guessCounter == 2)
            {
                imageDisplay[3] = wrongGuess1;
                imageDisplay[4] = wrongGuess4;
            }
            if (guessCounter == 1)
            {
                imageDisplay[3] = wrongGuess1;
                imageDisplay[4] = wrongGuess4;
                imageDisplay[5] = wrongGuess5;
            }
            if (guessCounter == 0)
            {
                imageDisplay[3] = wrongGuess1;
                imageDisplay[4] = wrongGuess4;
                imageDisplay[5] = wrongGuess6;
            }

            return imageDisplay;
        }
    }
}
