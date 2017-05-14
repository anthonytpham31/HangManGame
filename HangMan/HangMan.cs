using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;

namespace HangMan
{
    class HangMan
    {
        SpeechSynthesizer synth = new SpeechSynthesizer();
        private string[] imageDisplay = new string[7];
        private int guessCounter = 6;
        static Random rnd = new Random();
        private string stringDisplay;
        private List<string> wordList;

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
        

        public HangMan()
        {
            this.wordList = getHangManWords();
            this.imageDisplay = createHangManImage();
            this.stringDisplay = string.Join("", this.imageDisplay);
            
        }

        public string[] createHangManImage()
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

        public List<string> getHangManWords()
        {
            List<string> words = new List<string>();
            bool checkForWords = true;
            try
            {
                using (StreamReader sr = new StreamReader("./HangManWords.txt"))
                {
                    while (checkForWords)
                    {
                        string line = sr.ReadLine();
                        if (line == null || !line.Any())
                        {
                            checkForWords = false;
                        }
                        else
                        {
                            words.Add(line);
                            checkForWords = true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return words;
        }

        public string getWord()
        {
            string randomizer = wordList.ElementAt(rnd.Next(wordList.Count()));
            return randomizer;
        }

        public void runHangMan()
        {
            synth.Speak("Hello, Welcome to HangMan");
            bool repeat = true;
            while (repeat)
            {
                Console.Clear();
                string wordToGuess = getWord().ToLower();

                bool won = false;
                int lettersRevealed = 0;
                guessCounter = 6;

                char[] hang = new char[wordToGuess.Length];
                List<char> correctGuesses = new List<char>();
                List<char> incorrectGuesses = new List<char>();

                for (int i = 0; i < hang.Length; i++)
                {
                    hang[i] = '*';
                }

                while (!won && guessCounter > 0)
                {
                    string inc = string.Join(" ", incorrectGuesses.ToArray());
                    Console.WriteLine("Previous Incorrect Guesses Were: " + inc);

                    Console.WriteLine(String.Join("",createHangManImage()));
                    Console.WriteLine(hang);

                    Console.WriteLine("Enter A Letter:");
                    string input = Console.ReadLine().ToLower();
                    
                    Console.Clear();

                    if (input.Length != 1 || input.Any(x => !char.IsLetter(x)))
                    {
                        Console.WriteLine("Please Enter Just A Letter");
                        continue;
                    }
                    else
                    {
                        if (correctGuesses.Contains(input[0]))
                        {
                            Console.WriteLine("Already Guessed {0}", input[0]);
                            continue;
                        }
                        if (incorrectGuesses.Contains(input[0]))
                        {
                            Console.WriteLine("Already Guessed {0}", input[0]);
                            continue;
                        }
                        if (wordToGuess.Contains(input))
                        {
                            correctGuesses.Add(input[0]);

                            for (int i = 0; i < wordToGuess.Length; i++)
                            {
                                if (wordToGuess[i] == input[0])
                                {
                                    hang[i] = input[0];
                                    lettersRevealed++;
                                }

                            }
                            if (lettersRevealed == wordToGuess.Length)
                            {
                                won = true;
                            }
                        }
                        else
                        {
                            incorrectGuesses.Add(input[0]);
                            Console.WriteLine("No {0} in this word!", input);
                            guessCounter--;
                        }

                    }
                    
                }
                if (won)
                {
                    Console.WriteLine("You Win! It was {0}", wordToGuess);
                }
                else
                {
                    Console.WriteLine(String.Join("", createHangManImage()));
                    Console.WriteLine("You Lost! It was {0}", wordToGuess);
                }
                Console.WriteLine("Do You Want to Play Again (Y/N)?");
                char go = Console.ReadLine().ToCharArray()[0];
                if (char.ToLower(go) == 'y')
                {
                    repeat = true;
                }
                else
                {
                    synth.Speak("Good Byeeeeeeeeeeeeeeeeeeeeeeeeeee!");
                    repeat = false;
                }
                
            }
            
        }

        static void Main(string[] args)
        {
            HangMan test = new HangMan();
            test.runHangMan();
        }
    }

}
