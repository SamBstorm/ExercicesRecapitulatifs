using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoRecap_Minesweeper
{
    public struct MinesweeperConsole
    {
        public Minesweeper jeu;
        public uint currentX;
        public uint currentY;

        public void ShowGrid() {
            Console.SetCursorPosition(0,0);
            for (int y = 0; y < Minesweeper.HEIGHT; y++)
            {
                for (int x = 0; x < Minesweeper.WIDTH; x++)
                {
                    string cellText = " ";
                    if (currentX == x && currentY == y) Console.BackgroundColor = ConsoleColor.Red;
                    switch (jeu.grid[x,y].state)
                    {
                        case CellState.None:
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            break;
                        case CellState.Activated:
                            Console.ForegroundColor = ConsoleColor.White;
                            int minesCount = jeu.grid[x, y].minesCount;
                            cellText = (minesCount>0)?minesCount.ToString():cellText;
                            break;
                        case CellState.Flagged:
                            Console.ForegroundColor = ConsoleColor.Blue;
                            break;
                    };
                    if (jeu.isFinished && jeu.grid[x, y].isMined)
                    {
                        cellText = "x";
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    Console.Write($"[{cellText}]");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }

        public void Start() {
            Console.SetWindowSize(Minesweeper.WIDTH * 3, Minesweeper.HEIGHT + 5);
            jeu = new Minesweeper();
            jeu.Start();
            MinesweeperAction action;
            do { 
                ShowGrid();
                action = Command();
                switch (action)
                {
                    case MinesweeperAction.GoUp:
                        if(currentY > 0) currentY--;
                        else Console.Beep(120, 300);
                        break;
                    case MinesweeperAction.GoDown:
                        if (currentY < Minesweeper.HEIGHT-1) currentY++;
                        else Console.Beep(120, 300);
                        break;
                    case MinesweeperAction.GoLeft:
                        if (currentX > 0) currentX--;
                        else Console.Beep(120, 300);
                        break;
                    case MinesweeperAction.GoRight:
                        if (currentX < Minesweeper.WIDTH - 1) currentX++;
                        else Console.Beep(120, 300);
                        break;
                    case MinesweeperAction.Active:
                        Console.Beep(200, 500);
                        jeu.ActiveCell(currentX, currentY);
                        break;
                    case MinesweeperAction.Flag:
                        Console.Beep(250, 500);
                        jeu.FlagCell(currentX, currentY);
                        break;
                }
            } while (action != MinesweeperAction.Quit && !jeu.isFinished);
            if (action == MinesweeperAction.Quit) return;
            ShowGrid();
            if (jeu.minesFound == Minesweeper.MINES) Console.WriteLine("Gagné!");
            else Console.WriteLine("Perdu...");
        }

        public MinesweeperAction Command()
        {
            ConsoleKey key;
            do
            {
                key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        return MinesweeperAction.GoUp;
                    case ConsoleKey.LeftArrow:
                        return MinesweeperAction.GoLeft;
                    case ConsoleKey.RightArrow:
                        return MinesweeperAction.GoRight;
                    case ConsoleKey.DownArrow:
                        return MinesweeperAction.GoDown;
                    case ConsoleKey.E:
                        return MinesweeperAction.Active;
                    case ConsoleKey.F:
                        return MinesweeperAction.Flag;
                    case ConsoleKey.Q:
                    case ConsoleKey.Escape:
                        return MinesweeperAction.Quit;
                    default:
                        Console.Beep(150, 500);
                        break;
                }
            } while (true);
        }
    }
}
