using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game.Sokoban
{
    public enum eCellType { FLOOR=0, TARGET=1, BLOCK=2, WALL=3 };
    /// <summary>
    /// GLOBAL Script.
    /// </summary>
    public static class GLOBAL 
    {
        public static LevelBuilder LEVEL { get; set; }        
        public static PlayerControl PLAYER { get; set; }
        public static CameraControl CAMERA { get; set; }

        
    }
}