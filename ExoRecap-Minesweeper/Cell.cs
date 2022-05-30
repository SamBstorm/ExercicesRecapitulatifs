using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoRecap_Minesweeper
{
    public struct Cell
    {
        public CellState state;
        public ushort positionX;
        public ushort positionY;
        public bool isMined;
        public int minesCount;

        public void CheckMinesCount(Cell[] aroundCells)
        {
            minesCount = 0;
            foreach (Cell cell in aroundCells)
            {
                if (cell.isMined) minesCount++;
            }
        }
        public void Active() {
            if(state == CellState.None) state = CellState.Activated;
        }
        public void Flag()
        {
            if (state == CellState.None) state = CellState.Flagged;
        }
        public void Unflag()
        {
            if (state == CellState.Flagged) state = CellState.None;
        }
    }
}
