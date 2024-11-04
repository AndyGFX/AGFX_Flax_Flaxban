using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game;

/// <summary>
/// LevelDefinition Script.
/// </summary>
public class LevelDefinition : Script
{

        // -------------------------------------------------------- LEVEL BUILDER
        // MAP
        
        public Texture map;

        // HOME
        [EditorDisplay("Blocks")]
        public Prefab prefabHome; // home
        [EditorDisplay("Color")]
        public Color colorHome = Color.Green;

        // FLOOR
        [EditorDisplay("Blocks")]
        public Prefab prefabFloor;
        [EditorDisplay("Color")]
        public Color colorFloor = Color.White;

        // FLOOR - CHEST
        [EditorDisplay("Blocks")]
        public Prefab prefabFloorChest;
        

        // WALL
        [EditorDisplay("Blocks")]
        public Prefab prefabWall;
        [EditorDisplay("Color")]
        public Color colorWall = Color.Black;

        // TARGET
        [EditorDisplay("Blocks")]
        public Prefab prefabTarget;
        [EditorDisplay("Color")]
        public Color colorTarget = Color.Red;

        // CHEST
        [EditorDisplay("Blocks")]
        public Prefab prefabChest;
        [EditorDisplay("Color")]
        public Color colorChest = Color.Blue;

        // CHEST
        [EditorDisplay("Blocks")]
        public Prefab prefabOuterZone;
        [EditorDisplay("Color")]
        public Color colorOuterZone = Color.Yellow;

        [EditorDisplay("Grid properties")]
        public float gridCellSpacing = 100.0f;

    /// <inheritdoc/>
    public override void OnStart()
    {
        // Here you can add code that needs to be called when script is created, just before the first game update
    }
    
    /// <inheritdoc/>
    public override void OnEnable()
    {
        // Here you can add code that needs to be called when script is enabled (eg. register for events)
    }

    /// <inheritdoc/>
    public override void OnDisable()
    {
        // Here you can add code that needs to be called when script is disabled (eg. unregister from events)
    }

    /// <inheritdoc/>
    public override void OnUpdate()
    {
        // Here you can add code that needs to be called every frame
    }
}
