using System;
using System.Collections.Generic;
using System.Linq;

namespace ExoRecap_Pendu
{
    class Program
    {
        static int[] scores = new int[3];
        static Random RNG = new Random();
        static string[] words = new string[] {
                "Processeur",
                "Esperluette",
                "Crochet",
                "Guillemet",
                "Parenthèse",
                "Virgule",
                "Structure",
                "Référence",
                "Mémoire",
                "Variable",
                "Algorithmique",
                "Diagramme",
                "Terminal",
                "Opérateur",
                "Ordinateur",
                "Souris",
                "Tapis",
                "Clavier",
                "Touche",
                "Écran",
                "Pointeur",
                "Prise",
                "Cable",
                "Console"
            };
        static void Main(string[] args)
        {
            if(args.Contains("cheat")) words = new string[] { "Hacks","EasterEgg","012345678910" };
            StartGame();
        }
        private static void StartGame()
        {
            do
            {
                int choice = ChooseLevel();
                string word = "";
                switch (choice)
                {
                    case 1:
                        word = ChooseAWord(5, 6);
                        break;
                    case 2:
                        word = ChooseAWord(7, 10);
                        break;
                    case 3:
                        word = ChooseAWord(11, 26);
                        break;
                }
                if (PlayGame(word))
                {
                    DisplayWin(word);
                    if(scores[choice - 1] < 4) scores[choice-1]+=1;
                    if (AllScores(scores)) DisplayGreatScores();
                }
                else
                {
                    DisplayLoose(word);
                }
            } while (PlayAgain());
            DisplayCredits();
        }
        private static void DisplayGreatScores()
        {
            Console.ReadKey(false);
            string line = "*****************************************************";
            Console.Clear();
            Console.WriteLine(line);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n\n\n\nFélicitation vous avez trouvé tout les mots!\n\n\n\n");
            Console.ResetColor();
            Console.WriteLine(line);
        }
        private static void DisplayCredits()
        {
            Console.Clear();
            Console.WriteLine("Merci d'avoir joué!");
        }
        private static bool PlayAgain()
        {
            string Ok;
            string message = "";
            do
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(message);
                Console.ResetColor();
                Console.WriteLine("Voulez-vous rejouer ? true/false");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Ok = Console.ReadLine();
                Console.ResetColor();
                message = "Réponse incorrecte : true ou false uniquement\n";
            } while (Ok != "true" && Ok != "false");
            return Ok == "true";
        }
        private static void DisplayLoose(string word)
        {
            string line = "*****************************************************";
            Console.Clear();
            Console.WriteLine(line);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n\n\n\nDésolé, vous n'avez pas trouvé le mot : {word} ...\n\n\n\n");
            Console.ResetColor();
            Console.WriteLine(line);
        }
        private static void DisplayWin(string word)
        {
            string line = "*****************************************************";
            Console.Clear();
            Console.WriteLine(line);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n\n\n\nBravo vous avez trouvé le mot : {word} !\n\n\n\n");
            Console.ResetColor();
            Console.WriteLine(line);
        }
        static string ChooseAWord(int min, int max)
        {
            string choosen;
            do
            {
                choosen = words[RNG.Next(words.Length - 1)];
            } while (choosen.Length < min || choosen.Length > max);
            return choosen;
        }
        static int ChooseLevel()
        {
            string input;
            string message = "";
            do
            {
                string line = "----------------------------------------------------";
                Console.Clear();
                Console.WriteLine($"|{line}|");
                Console.WriteLine("| Le pendu :");
                Console.WriteLine($"|{line}|");
                Console.WriteLine($"Vous avez réussi {scores[0]+ scores[1]+ scores[2]} niveaux");
                Console.WriteLine($"|{line}|");
                Console.WriteLine("Choisissez un niveau :");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"1 = Niveau facile\t: 5 à 6 lettres\t\t{ShowScore(1)}");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"2 = Niveau moyen\t: 7 à 10 lettres\t{ShowScore(2)}");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"3 = Niveau difficile\t: 11 à 26 lettres\t{ShowScore(3)}");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(message);
                Console.ResetColor();
                Console.WriteLine("Quelle niveau ?");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                input = Console.ReadLine();
                Console.ResetColor();
                message = "\nNiveau inconnu : veuillez saisir 1, 2 ou 3\n";
            } while (input != "1" && input != "2" && input != "3");
            return int.Parse(input);
        }
        static string ShowScore(int level)
        {
            string display = "";
            int score = scores[level - 1];
            int i = 0;
            while ( i < score )
            {
                display += "*";
                i++;
            }
            while (i < 4)
            {
                display += "o";
                i++;
            }
            return display;
        }
        static bool PlayGame(string word)
        {
            int turn = 0;
            char[] tries = new char[10];
            bool[] founds = new bool[word.Length];
            while (turn < 10)
            {
                Console.Clear();
                Console.WriteLine(DisplayWord(word,founds));
                Console.WriteLine(DisplayTries(tries));
                Console.WriteLine($"Votre proposition {turn+1}:");
                DisplayMan(turn, 60, 0);
                Console.SetCursorPosition(0, 8);
                char chara = GetChar();
                while (!CharInTries(chara, tries)) {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Déjà proposé...");
                    Console.ResetColor();
                    chara = GetChar();
                }
                if (!CharInWord(chara, word, founds))
                {
                    turn++;
                    AddChar(chara, tries);
                }
                if (AllFounds(founds)) return true;
            }
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            DisplayMan(turn, 40, 0);
            Console.ReadKey(false);
            Console.ResetColor();
            return false;
        }
        private static bool AllFounds(bool[] founds)
        {
            for (int i = 0; i < founds.Length; i++)
            {
                if (founds[i] == false) return false;
            }
            return true;
        }
        private static bool AllScores(int[] scores)
        {
            for (int i = 0; i < scores.Length; i++)
            {
                if (scores[i] != 4) return false;
            }
            return true;
        }
        private static void AddChar(char chara, char[] tries)
        {
            int i = 0;
            while (i < tries.Length && tries[i] != default(char))
            {
                i++;
            }
            tries[i] = chara;
        }
        private static bool CharInWord(char chara, string word, bool[] founds)
        {
            bool found = false;
            for (int i = 0; i < word.Length; i++)
            {
                char temp = word.ToUpper()[i];
                if (temp == 'É' || temp == 'È' || temp == 'Ë') temp = 'E';
                if (chara == temp)
                {
                    founds[i] = true;
                    found = true;
                }
            }
            return found;
        }
        private static char GetChar()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            string answer = Console.ReadLine().ToUpper();
            Console.ResetColor();
            while (answer.Length != 1)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("1 Caractère à la fois...");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                answer = Console.ReadLine().ToUpper();
                Console.ResetColor();
            }
            return answer[0];
        }
        private static bool CharInTries(char chara, char[] tries)
        {
            for (int i = 0; i < tries.Length; i++)
            {
                if (chara == tries[i]) return false;
            }
            return true;
        }
        private static string DisplayTries(char[] tries)
        {
            string line = "----------------------------------------------------";
            string numbers = "";
            string tries_char = "";
            for (int i = 0; i < tries.Length; i++)
            {
                numbers += $"| {i + 1} ";
                tries_char += $"| {((tries[i]==default(char))?' ':tries[i])} ";
            }
            return $"|{line}|\n|           {numbers}|\n|{line}|\n|Proposition{tries_char}|\n|{line}|";
        }
        private static string DisplayWord(string word, bool[] founds)
        {
            string display = "";
            for (int i = 0; i < word.Length; i++)
            {
                if (founds[i]) display += word[i];
                else display += "_";
                if(i < word.Length - 1) display += " ";
            }
            return display;
        }
        private static void DisplayMan(int turn, int x, int y)
        {
            char[,] man = new char[11, 5];
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    man[i, j] = ' ';
                }
            }
            if(turn >= 1)
            {
                man[0, 4] = ' ';
                man[1, 4] = '_';
                man[2, 4] = '_';
                man[3, 4] = '_';
                man[4, 4] = '_';
                man[5, 4] = '_';
                man[6, 4] = '_';
                man[7, 4] = '_';
                man[8, 4] = '_';
                man[9, 4] = '_';
                man[10, 4] = '_';
            }
            if(turn >= 2)
            {
                man[0, 0] = '|';
                man[0, 1] = '|';
                man[0, 2] = '|';
                man[0, 3] = '|';
                man[0, 4] = '|';
            }
            if (turn >= 3)
            {
                man[1, 0] = '-';
                man[2, 0] = '-';
                man[3, 0] = '-';
                man[4, 0] = '-';
            }
            if (turn >= 4) man[5, 0] = '|';
            if (turn >= 5) man[5, 1] = '☻';
            if (turn >= 6) man[5, 2] = '♦';
            if (turn >= 7) man[4, 2] = '/';
            if (turn >= 8) man[6, 2] = '\\';
            if (turn >= 9) man[4, 3] = '/';
            if (turn >= 10) man[6, 3] = '\\';
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.SetCursorPosition(x+i,y+j);
                    Console.Write(man[i, j]);
                }
            }
        }
    }
}
