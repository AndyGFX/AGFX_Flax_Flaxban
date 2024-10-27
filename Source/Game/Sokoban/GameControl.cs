using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game.Sokoban
{
    /// <summary>
    /// GameControl Script.
    /// </summary>
    public class GameControl : Script
    {
        
        public string LEVEL_object_name = "undefined";
        public string CAMERA_object_name = "undefined";

        public string PLAYER_object_name = "undefined";
        public Prefab PLAYER_prefab = null;

        
        /// <inheritdoc/>
        /// 


        public override void OnStart()
        {
            
            GLOBAL.LEVEL = Scene.FindActor(this.LEVEL_object_name).GetScript<LevelBuilder>();            
            
            GLOBAL.PLAYER = Scene.FindActor(this.PLAYER_object_name).GetScript<PlayerControl>();
            PrefabManager.SpawnPrefab(this.PLAYER_prefab, GLOBAL.PLAYER.Actor);

            GLOBAL.CAMERA = Scene.FindActor(this.CAMERA_object_name).GetScript<CameraControl>();
            GLOBAL.CAMERA.UpdateCameraPosition();
            

            // build level from texture
            GLOBAL.LEVEL.BuildLevel();
            
            // Set player position            
            GLOBAL.PLAYER.SetGridPosition(GLOBAL.LEVEL.homePosition);            
            GLOBAL.PLAYER.UpdatePosition();
            GLOBAL.CAMERA.PrepareZoom();
            


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
}   