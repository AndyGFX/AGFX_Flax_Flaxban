using System;
using System.Collections.Generic;
using FlaxEngine;
using Game.Sokoban;

namespace Game;

/// <summary>
/// PlayerControl Script.
/// </summary>
public class PlayerControl : Script
{
    public float moveSpeed = 5.0f;
    public bool showDebug = true;


    private Vector2[] direction = new Vector2[4] { new Vector2(1, 0), new Vector2(0, 1), new Vector2(-1, 0), new Vector2(0,-1) };
    private int currentDirection = 0;
    private Vector2 gridPosition = new Vector2(0, 0);
    private bool controlLocked = false;
    private float forwardStep = 0f;
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
        if (Input.GetKeyDown(KeyboardKeys.ArrowLeft) && !this.controlLocked) {  this.TurnLeft(); }

        if (Input.GetKeyDown(KeyboardKeys.ArrowRight) && !this.controlLocked) {  this.TurnRight(); }

        if (Input.GetAction("NavigateUp") && !this.controlLocked) { this.controlLocked = true; }

        if (this.controlLocked){ this.MoveForward(); }        

        if (Input.GetAction("ZoomUp")) { GLOBAL.CAMERA.ZoomUp(); }
        if (Input.GetAction("ZoomDown")) { GLOBAL.CAMERA.ZoomDown(); }

    }



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
        this.controlLocked = false;

        this.DebugDraw();
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

        // Check free space in front of viewport
        //Vector2 checkCell = gridPosition + direction[currentDirection]*2;
        

        if (this.forwardStep >= 100)
        {
            this.forwardStep = 0;
            this.controlLocked = false;

            gridPosition += direction[currentDirection];
            
            this.DebugDraw();
        }
    }

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
    }

    public bool MoveNotBlocked()
    {
        return GLOBAL.LEVEL.IsObstacle(this.gridPosition + direction[currentDirection]);
    }
}
