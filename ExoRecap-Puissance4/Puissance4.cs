using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoRecap_Puissance4
{
    public enum P4_token { None, Player1, Player2 }
    public struct Puissance4
    {
        public const int COLUMNS = 7;
        public const int HEIGHT_LIMIT = 6;
        public bool gameFinished;
        public P4_token current_Token;

        public P4_token[,] grid;

        public void Start()
        {
            grid = new P4_token[COLUMNS, HEIGHT_LIMIT];
            gameFinished = false;
            current_Token = P4_token.None;
        }

        public int AddToken(int column)
        {
            if (column < 0 || column > COLUMNS) return -1; //Gérer avec une exception (vu en Orienté Object)
            bool stop = false;
            int height;
            for (height = 0; height < HEIGHT_LIMIT && !stop; height++)
            {
                if (grid[column, height] == P4_token.None) stop = true;
            }
            height--;
            grid[column, height] = current_Token;
            return height;
        }

        public void CheckNewGrid(int column, int height)
        {
            if (column < 0 || column > COLUMNS) return; //Gérer avec une exception (vu en Orienté Object)
            if (height < 0 || height > HEIGHT_LIMIT) return; //Gérer avec une exception (vu en Orienté Object)

            //Vertical
            bool sameColor = true;
            if (height >= 3)
            {
                for (int i = 1; i < 4 && sameColor; i++)
                {
                    if (grid[column, height - i] != current_Token) sameColor = false;
                }
                if (sameColor)
                {
                    gameFinished = true;
                    return;
                }
            }
            //Horizontal
            int countColor = 0;

            sameColor = true;
            for (int i = column; i >= 0 && sameColor && countColor < 4; i--)
            {
                if (grid[i, height] != current_Token) sameColor = false;
                else countColor++;
            }
            sameColor = true;
            for (int i = column + 1; i < COLUMNS && sameColor && countColor < 4; i++)
            {
                if (grid[i, height] != current_Token) sameColor = false;
                else countColor++;
            }
            if (countColor == 4)
            {
                gameFinished = true;
                return;
            }
            // Oblique /
            countColor = 1;
            for (int i = 1; i < 4; i++)
            {
                if (height + i < HEIGHT_LIMIT && column + i < COLUMNS)
                    if (grid[column + i, height + i] == current_Token) countColor++;
                if (height - i >= 0 && column - i >= 0)
                    if (grid[column - i, height - i] == current_Token) countColor++;
            }
            if (countColor == 4)
            {
                gameFinished = true;
                return;
            }
            // Oblique \
            countColor = 1;
            for (int i = 1; i < 4; i++)
            {
                if (height - i >= 0 && column + i < COLUMNS)
                    if (grid[column + i, height - i] == current_Token) countColor++;
                if (height + i < HEIGHT_LIMIT && column - i >= 0)
                    if (grid[column - i, height + i] == current_Token) countColor++;
            }
            if (countColor == 4)
            {
                gameFinished = true;
                return;
            }
        }

        public void TogglePlayer()
        {
            switch (current_Token)
            {
                case P4_token.None:
                case P4_token.Player2:
                    current_Token = P4_token.Player1;
                    break;
                case P4_token.Player1:
                    current_Token = P4_token.Player2;
                    break;
            }
        }
    }
}
