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

            // setup camera
            GLOBAL.CAMERA = Scene.FindActor(this.CAMERA_object_name).GetScript<CameraControl>();
            GLOBAL.CAMERA.PrepareCameraOnStart();
            

            // build level from texture
            GLOBAL.LEVEL.BuildLevel();
            
            // Set player position            
            GLOBAL.PLAYER.SetGridPosition(GLOBAL.LEVEL.homePosition);            
            GLOBAL.PLAYER.UpdatePosition();
            GLOBAL.CAMERA.PrepareZoom();
         

        }

        public override void OnUpdate()
        {            
            GLOBAL.PLAYER.UpdatePlayerControl();
            GLOBAL.CAMERA.UpdateCameraControl();


            if (Input.GetKeyDown(KeyboardKeys.R))
            {
                GLOBAL.LEVEL.ResetLevelData();
                GLOBAL.LEVEL.BuildLevel();
            
                // Set player position            
                GLOBAL.PLAYER.SetGridPosition(GLOBAL.LEVEL.homePosition);            
                GLOBAL.PLAYER.UpdatePosition();
            }
        }
    }
}   