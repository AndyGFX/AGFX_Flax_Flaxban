using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game.Sokoban
{

    public class GridCell
    {
        
        public eCellType cellType = eCellType.FLOOR;
        public bool collision = false;
        
        public GridCell(eCellType type)
        {
            this.cellType = type;
        }
    }
}