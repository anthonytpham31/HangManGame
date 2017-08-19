using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HangMan
{
    public class WordBank
    {
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
            List<string> wordList = getHangManWords();
            Random rnd = new Random();
            string randomizer = wordList.ElementAt(rnd.Next(wordList.Count()));
            return randomizer;
        }
    }
}
