using System;
using System.Collections.Generic;
using FlaxEngine;
using Game.Sokoban;

using FTween;
using System.Threading.Tasks;

namespace Game;

/// <summary>
/// PlayerControl Script.
/// </summary>
public class PlayerControl : Script
{
    public float moveSpeed = 5.0f;
    public eMoveType moveType = eMoveType.FORWARD;
    public bool showDebug = true;

    
    private Vector2[] direction = new Vector2[4] { new Vector2(1, 0), new Vector2(0, 1), new Vector2(-1, 0), new Vector2(0,-1) };
    private int currentDirection = 0;
    private Vector2 gridPosition = new Vector2(0, 0);
    private bool controlLocked = false;
    private float forwardStep = 0f;

        private TChest chest = null;

    /// <inheritdoc/>
    public override void OnStart()
    {
        // Here you can add code that needs to be called when script is created, just before the first game update

        if (this.moveType == eMoveType.FORWARD)
        {
            direction = new Vector2[4] { new Vector2(1, 0), new Vector2(0, 1), new Vector2(-1, 0), new Vector2(0,-1) };
        }
        if (this.moveType == eMoveType.DIRECT)
        {
            direction = new Vector2[4] { new Vector2(1, 0), new Vector2(0, -1), new Vector2(-1, 0), new Vector2(0,1) };
        }
        
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

        if (this.moveType == eMoveType.FORWARD)
        {
            if (Input.GetKeyDown(KeyboardKeys.ArrowLeft) && !this.controlLocked) {  this.TurnLeft(); }

            if (Input.GetKeyDown(KeyboardKeys.ArrowRight) && !this.controlLocked) {  this.TurnRight(); }

            if (Input.GetAction("NavigateUp") && !this.controlLocked) { this.controlLocked = true; }
       
        }

        if (this.moveType == eMoveType.DIRECT)
        {
            if (Input.GetAction("NavigateLeft") && !this.controlLocked) {  this.MoveLeft(); }
            if (Input.GetAction("NavigateRight") && !this.controlLocked) {  this.MoveRight(); }
            if (Input.GetAction("NavigateUp") && !this.controlLocked) {  this.MoveUp(); }
            if (Input.GetAction("NavigateDown") && !this.controlLocked) {  this.MoveDown(); }
        }

        if (this.controlLocked){ this.MoveForward(); } 

        if (Input.GetAction("ZoomUp")) { GLOBAL.CAMERA.ZoomUp(); }
        if (Input.GetAction("ZoomDown")) { GLOBAL.CAMERA.ZoomDown(); }

    }


#region Player Movement - FORWARD

    public void TurnLeft()
    {
        this.controlLocked = true;

        // update direction vector
        this.currentDirection--;

        if (this.currentDirection < 0)
        {
            this.currentDirection = 3;
        }

        // rotate player
        this.Actor.RotateAround(this.Actor.Position, Vector3.UnitY, -90);
        this.chest = GLOBAL.LEVEL.GetChest(this.gridPosition+direction[currentDirection]);

        this.controlLocked  = false;

        this.DebugDraw();

    }

    public void TurnRight()
    {
         
        this.controlLocked = true;
        
        // update direction vector
        this.currentDirection++;    

        if (this.currentDirection > 3)
        {
            this.currentDirection = 0;
        }

        // rotate player
        this.Actor.RotateAround(this.Actor.Position, Vector3.UnitY, 90);        
        this.chest = GLOBAL.LEVEL.GetChest(this.gridPosition+direction[currentDirection]);

        this.DebugDraw();

        this.controlLocked = false;
    }

    private void MoveForward()
    {
        if (this.MoveNotBlocked()) 
        { 
            this.controlLocked = false;
            return; 
        }
        
        this.forwardStep += this.moveSpeed;
        this.Actor.Position += this.Actor.LocalTransform.Forward * this.moveSpeed;
        GLOBAL.CAMERA.Move(this.Actor.LocalTransform.Forward * this.moveSpeed);
        

        // shift chest if needed
        this.chest = GLOBAL.LEVEL.GetChest(this.gridPosition+direction[currentDirection]);
        if (this.chest != null)
        {
            this.chest.actor.Position += this.Actor.LocalTransform.Forward * this.moveSpeed;
            GLOBAL.LEVEL.SetChestAt(this.gridPosition+direction[currentDirection],eCellType.FLOOR,false);
            
        }

        // Check free space in front of viewport
        //Vector2 checkCell = gridPosition + direction[currentDirection]*2;
        

        if (this.forwardStep >= 100)
        {
            this.forwardStep = 0;
            this.controlLocked = false;

            gridPosition += direction[currentDirection];
            
            if (this.chest != null)
            {
                this.chest.gridPosition += direction[currentDirection];
                GLOBAL.LEVEL.SetChestAt(this.gridPosition+direction[currentDirection],eCellType.CHEST,false);
            }

            this.DebugDraw();
        }
    }

#endregion

#region Player Movement - DIRECT

    public void MoveLeft()
    {
        this.Actor.EulerAngles = new Vector3(0,180,0);
        this.currentDirection  = 2;
        this.controlLocked  = true;
        this.DebugDraw();
    }
    public void MoveRight()
    {
        this.Actor.EulerAngles = new Vector3(0,0,0);
        this.currentDirection  = 0;
        this.controlLocked  = true;
        this.DebugDraw();        
    }
    public void MoveUp()
    {
        this.Actor.EulerAngles = new Vector3(0,-90,0);
        this.currentDirection  = 1;
        this.controlLocked  = true;
        this.DebugDraw();
    }
    public void MoveDown()
    {
        this.Actor.EulerAngles = new Vector3(0,90,0);
        this.currentDirection  =3;
        this.controlLocked  = true;
        this.DebugDraw();
    }

#endregion


    public void SetGridPosition(Vector2 position)
    {
        this.gridPosition = position;        
    }
    
    public void UpdatePosition()
    {        
        this.Actor.Position = new Float3(this.gridPosition.Y * GLOBAL.LEVEL.definition.gridCellSpacing, 0, this.gridPosition.X * GLOBAL.LEVEL.definition.gridCellSpacing);
    }

    public void DebugDraw()
    {
        if (this.showDebug == false) { return; }

        GLOBAL.HUD.label_0.Text = "GPos: "+gridPosition.ToString();
        GLOBAL.HUD.label_1.Text = "Cell: "+GLOBAL.LEVEL.GetCellType(this.gridPosition).ToString();
        GLOBAL.HUD.label_2.Text = "Cell front: "+GLOBAL.LEVEL.GetCellType(this.gridPosition+direction[currentDirection]).ToString();
        GLOBAL.HUD.label_3.Text = "Obstacle: "+GLOBAL.LEVEL.IsObstacle(this.gridPosition+direction[currentDirection]).ToString();
        if (this.chest == null)
        GLOBAL.HUD.label_4.Text = "Chest: NONE";  
        else
        GLOBAL.HUD.label_4.Text = "Chest: "+this.chest.ToString();  
    }

    public bool MoveNotBlocked()
    {
        this.chest = GLOBAL.LEVEL.GetChest(this.gridPosition+direction[currentDirection]);

        if (this.chest == null)
        {
           
            return GLOBAL.LEVEL.IsObstacle(this.gridPosition + direction[currentDirection]);            
        }

        return GLOBAL.LEVEL.IsObstacle(this.gridPosition + direction[currentDirection]*2);
    }
}
