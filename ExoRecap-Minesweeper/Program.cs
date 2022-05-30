using System;

namespace ExoRecap_Minesweeper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            MinesweeperConsole jeu = new MinesweeperConsole();
            jeu.Start();
        }
    }
}
