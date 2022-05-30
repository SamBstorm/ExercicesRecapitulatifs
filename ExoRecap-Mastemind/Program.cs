using System;

namespace ExoRecap_Mastemind
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            MastermindConsole jeu = new MastermindConsole();
            jeu.Start(args);
        }
    }
}
