using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoRecap_Puissance4
{
    public struct Puissance4Console
    {
        public Puissance4 jeu;
        public const int ZONE_GRID_X = 4;
        public const int ZONE_GRID_Y = 3;
        public int token_position;

        public void Start()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.SetWindowSize((ZONE_GRID_X * 2) + (Puissance4.COLUMNS*3), (ZONE_GRID_Y * 2) + Puissance4.HEIGHT_LIMIT);
            do
            {
                Console.ResetColor();
                Console.Clear();
                token_position = 0;
                jeu = new Puissance4();
                jeu.Start();
                do
                {
                    jeu.TogglePlayer();
                    ShowGrid();
                    SelectColumn();
                    int landingHeight = jeu.AddToken(token_position);
                    jeu.CheckNewGrid(token_position, landingHeight);
                } while (!jeu.gameFinished);
                Console.BackgroundColor = GetPlayerColor(jeu.current_Token);
                Console.Clear();
                ShowGrid();
                Console.BackgroundColor = GetPlayerColor(jeu.current_Token);
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine($"Le {jeu.current_Token} a gagné!");
                Console.ReadKey(); 
            } while (Confirm("Voulez-vous rejouer?"));
        }


        public void ShowGrid()
        {
            int posX = ZONE_GRID_X;
            int posY = ZONE_GRID_Y;
            Console.SetCursorPosition(posX, posY);
            Console.BackgroundColor = ConsoleColor.Blue;
            for (int i = 1; i <= Puissance4.HEIGHT_LIMIT; i++)
            {
                for (int j = 0; j < Puissance4.COLUMNS; j++)
                {
                    Console.ForegroundColor = GetPlayerColor(jeu.grid[j, Puissance4.HEIGHT_LIMIT - i]);
                    Console.Write($" {'\x25CF'} ");
                }
                posY++;
                Console.SetCursorPosition(posX, posY);
            }
            Console.ResetColor();
        }

        public void SelectColumn()
        {
            ConsoleKey key;
            do
            {
                ShowSelection();
                key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.RightArrow:
                        Console.Beep(350, 100);
                        token_position = (token_position == Puissance4.COLUMNS - 1) ? 0 : token_position + 1;
                        break;
                    case ConsoleKey.LeftArrow:
                        Console.Beep(350, 100);
                        token_position = (token_position == 0) ? Puissance4.COLUMNS - 1 : token_position - 1;
                        break;
                    default:
                        Console.Beep(250, 300);
                        break;
                }
            } while (key != ConsoleKey.Enter);
        }

        public void ShowSelection() {
            Console.ForegroundColor = GetPlayerColor(jeu.current_Token);
            Console.SetCursorPosition(ZONE_GRID_X, ZONE_GRID_Y-2);
            for (int i = 0; i < Puissance4.COLUMNS; i++)
            {
                if (token_position == i) Console.Write($"<{'\x25CF'}>");
                else { Console.Write("   "); }
            }
        }

        public ConsoleColor GetPlayerColor(P4_token token)
        {
            ConsoleColor color = ConsoleColor.Black;
            switch (token)
            {
                case P4_token.Player1:
                    color =  ConsoleColor.Red;
                    break;
                case P4_token.Player2:
                    color =  ConsoleColor.Yellow;
                    break;
            }
            return color;
        }

        public bool Confirm(string message)
        {
            ConsoleKey key;
            bool result = true;
            Console.SetCursorPosition(ZONE_GRID_X, ZONE_GRID_Y + Puissance4.HEIGHT_LIMIT);
            Console.Write(message);
            do
            {
                Console.SetCursorPosition(ZONE_GRID_X, ZONE_GRID_Y + Puissance4.HEIGHT_LIMIT+1);
                Console.Write($"    {((result)?"[OUI]":" OUI ")}    {((!result) ? "[NON]" : " NON ")}");
                key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.RightArrow:
                        result = !result;
                        break;
                }
            } while (key != ConsoleKey.Enter);
            return result;
        }
    }
}
