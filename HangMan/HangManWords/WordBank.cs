using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HangMan.HangManWords
{
    public class WordBank
    {
        private List<string> GetHangManWords()
        {
            var words = new List<string>();
            try
            {

                using (var streamReader = new StreamReader("./HangManWords.txt"))
                {
                    var streamedWords = StreamWords(streamReader);
                    return streamedWords;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable To Read File.");
                Console.WriteLine(e.Message);
            }
            return words;
        }

        private List<string> StreamWords(StreamReader streamReader)
        {
            var checkForWords = true;
            var words = new List<string>();
            while (checkForWords)
            {
                var line = streamReader.ReadLine();
                if (line == null || !line.Any())
                {
                    checkForWords = false;
                }
                else
                {
                    words.Add(line);
                }
            }
            return words;
        }

        public string GetWord()
        {
            var wordList = GetHangManWords();
            var rnd = new Random();
            var randomizer = wordList.ElementAt(rnd.Next(wordList.Count));
            return randomizer;
        }
    }
}
