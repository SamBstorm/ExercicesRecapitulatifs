using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoRecap_Minesweeper
{
    public struct Minesweeper
    {
        public static Random RNG = new Random();
        public const int WIDTH = 20;
        public const int HEIGHT = 10;
        public const int MINES = 25;
        public Cell[,] grid;
        public bool isFinished;
        public int minesFound;

        public void Start() {
            isFinished = false;
            minesFound = 0;
            grid = new Cell[WIDTH, HEIGHT];
            for (ushort i = 0; i < WIDTH; i++)
            {
                for (ushort j = 0; j < HEIGHT; j++)
                {
                    grid[i, j].positionX = i;
                    grid[i, j].positionY = j;
                }
            }
            setMines(MINES);
        }

        public void ActiveCell(uint x, uint y)
        {
            if ((x < 0 || x >= WIDTH) || (y < 0 || y >= HEIGHT)) return; //Utiliser une exception (vu en Orienté Objet)
            if (grid[x, y].isMined)
            {
                isFinished = true;
                return;
            }
            if (grid[x, y].state == CellState.None) grid[x, y].Active();
            List<Cell> cellsAround = new List<Cell>();
            for (uint i = (x == 0)?0 : x-1; i <= ((x == WIDTH-1)?WIDTH-1: x + 1) ; i++)
            {
                for (uint j = (y == 0) ? 0 : y - 1; j <= ((y == HEIGHT - 1) ? HEIGHT - 1 : y + 1 ); j++)
                {
                    cellsAround.Add(grid[i, j]);
                }
            }
            cellsAround.Remove(grid[x, y]);
            grid[x, y].CheckMinesCount(cellsAround.ToArray());
            if (grid[x, y].minesCount == 0)
            {
                foreach (Cell cell in cellsAround)
                {
                    if(cell.state==CellState.None)ActiveCell(cell.positionX, cell.positionY);
                }
            }
        }
        public void FlagCell(uint x, uint y)
        {
            if ((x < 0 || x >= WIDTH) || (y < 0 || y >= HEIGHT)) return;
            if (grid[x, y].state == CellState.None)
            {
                grid[x, y].Flag();
                if (grid[x, y].isMined) minesFound++;
            }
            else if (grid[x, y].state == CellState.Flagged)
            { 
                grid[x, y].state = CellState.None; 
                if (grid[x, y].isMined) minesFound--;
            }
            if (minesFound == MINES) isFinished = true;
        }

        public void setMines(ushort limit) {
            for (int i = 0; i < limit; i++)
            {
                int x,y;
                do
                {
                    x = RNG.Next(WIDTH);
                    y = RNG.Next(HEIGHT);
                } while (grid[x, y].isMined);
                grid[x, y].isMined = true;
            }
        }
    }
}
