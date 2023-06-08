using System;

namespace Hangman
{
    //simple Hangman game. you lose a point for every character in the user input that is incorrect.
    // you lose when you reach 6 mistakes
    class Program
    {
        static void SkipLines(int lines) // a function to jump however many lines i need.
        {
            for (int i = 0; i < lines; i++)
            {
                Console.WriteLine();
            }
        }

        static void Game()
        {
            Console.Clear();
            int tries = 0; // 6 tries is the limit
            string[] samples = { "archeologist", "mathematics", "science", "video games", "computer", "central processing unit" }; // pre-made words
            string word = ""; // the word that has to be guessed
            string guessedLetters = ""; // keep guessed letters in a string so we can separate them later

            Random random = new Random(); // instantiate random

            string ReturnCommonCharacters(string target, string guess) // returns how many characters in string "guess" are in common with characters in string "target"
            {
                string lettersFound = ""; // string of correct characters
                foreach (char x in guess)
                {
                    if (target.Contains(x))
                    {
                        lettersFound += x; // if the character is correct then add it to "lettersFound"
                    }
                    else
                    {
                        tries++; // if the character is not found add to "tries"
                        if (tries >= 6)
                        {
                            break; // break the loop once 6 mistakes are reached. Restarts the game
                        }
                    }
                }
                return lettersFound;
            }

            Console.WriteLine("HANGMAN.");
            SkipLines(5);
            Console.WriteLine("Please input a word you would like to guess.");
            Console.WriteLine("(enter 'S' to pick a random sample word)");

            word = Console.ReadLine().ToLower(); // makes the input lowercase

            if (word == "s")
            {
                int rand = random.Next(0, samples.Length);
                word = samples[rand]; // get random word from the pre-made words list
            }
            string foundLetters = ""; // string of correctly guessed characters
            bool completed = true; // ugly hack (check line 94)
            while (tries < 6) // game loop
            {
                char[] wordTable = word.ToCharArray(); // where we keep the underscores(_) and actual characters together.

                Console.WriteLine("Guess the word!");
                guessedLetters = Console.ReadLine();
                
                foundLetters += ReturnCommonCharacters(word, guessedLetters); // add correctly guessed characters to the 'foundLetters' string
                completed = true; // ugly hack part 2
                for (int i = 0; i < word.Length; i++)
                {
                    if (foundLetters.Contains(word[i]))
                    {
                        wordTable[i] = word[i];
                    }
                    if (!foundLetters.Contains(word[i]))
                    {
                        if (word[i] == ' ') // if the word has a space, then use a space instead of an underscore
                        {
                            wordTable[i] = ' ';
                        }
                        else
                        {
                            completed = false; //ugly hack part 3
                            wordTable[i] = '_';
                        }
                    }
                }
                Console.WriteLine("Fails: " + tries.ToString());
                SkipLines(2);
                Console.WriteLine(wordTable);

                if (completed == true) // wordTable.ToString()==word didn't work so that's why i did this.
                {
                    Console.WriteLine("You guessed the word!");
                    break;
                }
                
            }
            Console.ReadLine();
        }

        static void Main(string[] args) 
        {
            while (true) // game loop
            {
                Game();
            }
        }
    }
}
