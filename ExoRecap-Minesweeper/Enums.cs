using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoRecap_Minesweeper
{
    public enum CellState { None, Activated, Flagged }
    public enum MinesweeperAction { GoUp, GoDown, GoLeft, GoRight, Active, Flag, Quit}
}
