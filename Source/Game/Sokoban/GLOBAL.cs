using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game.Sokoban
{
    public enum eCellType { FLOOR=0, TARGET=1, CHEST=2, WALL=3, HOME=4 };
    public enum eMoveType { FORWARD=0, DIRECT=1}
    
    /// <summary>
    /// GLOBAL Script.
    /// </summary>
    public static class GLOBAL 
    {
        public static LevelBuilder LEVEL { get; set; }        
        public static PlayerControl PLAYER { get; set; }
        public static CameraControl CAMERA { get; set; }
        public static HUD HUD { get; set; }

        public static int chestCount = 0;
        public static int chestCountOnPlace = 0;

        
    }
}