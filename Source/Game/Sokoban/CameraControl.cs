using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using FlaxEngine;
using Game.Sokoban;

namespace Game;

/// <summary>
/// CameraControl Script.
/// </summary>
public class CameraControl : Script
{
    public Vector3 offset = new Vector3(400f,820f,40f);
    public Vector3 orientation = new Vector3(0f,-90f,0f);
    public float minTargetDistance = 300f;
    public float maxTargetDistance = 1000f;
    public float zoomKeySpeed = 5f;
    public float zoomWheelSpeed = 5f;


    public Vector3 startCameraPosition = new Vector3(0, 0, 0);
    public Vector3 endCameraPosition = new Vector3(0, 0, 0);
    public float currentTargetDistance = 0;

    private Vector3 targetPosition = new Vector3(0, 0, 0);
    
    /// <inheritdoc/>
    /// 
    public override void OnStart()
    {
        
    }
    
    /// <inheritdoc/>
    public override void OnEnable()
    {
        
    }

    /// <inheritdoc/>
    public override void OnDisable()
    {
        // Here you can add code that needs to be called when script is disabled (eg. unregister from events)
    }

    /// <inheritdoc/>
    public override void OnUpdate()
    {
        this.Actor.Position = GLOBAL.PLAYER.Actor.Position - this.Actor.LocalTransform.Forward * this.currentTargetDistance;
    }


    public void UpdateCameraPosition()
    {
        // set camera position
        this.Actor.Position = GLOBAL.PLAYER.Actor.Position;

        // set camera offset
        this.Actor.Position = GLOBAL.PLAYER.Actor.Position + this.offset;

        // set startup camera angles
        this.Actor.EulerAngles = this.orientation;

        // look at player
        this.Actor.LookAt(GLOBAL.PLAYER.Actor.Position);
        
    }


    public void PrepareZoom()
    {
        // set at max zoom distance
        this.Actor.Position = GLOBAL.PLAYER.Actor.Position - this.Actor.LocalTransform.Forward * this.maxTargetDistance;

        // store start camera position
        this.startCameraPosition = this.Actor.Position;

        // set at max zoom distance
        this.endCameraPosition = GLOBAL.PLAYER.Actor.Position - this.Actor.LocalTransform.Forward * this.minTargetDistance;
        
        //calc startup distance to player
        
        this.currentTargetDistance = Vector3.Distance(this.Actor.Position, GLOBAL.PLAYER.Actor.Position);
    }

    public void Move(Vector3 delta)
    {
        this.Actor.Position += delta;
    }

    /// <summary>
    /// Zooms the camera up by the specified amount
    /// </summary>
    public void ZoomUp()
    {
        // subtract the zoom speed from the current target distance
        this.currentTargetDistance -= this.zoomKeySpeed;

        // check if the target distance is less than the minimum allowed
        if (this.currentTargetDistance <= this.minTargetDistance)
        {
            // set the target distance to the minimum allowed
            this.currentTargetDistance = this.minTargetDistance;
        }

    }

    /// <summary>
    /// Zooms the camera down by the specified amount
    /// </summary>
    public void ZoomDown()
    {
        // add the zoom speed to the current target distance
        this.currentTargetDistance += this.zoomKeySpeed;

        // check if the target distance is greater than or equal to the maximum allowed
        if (this.currentTargetDistance >= this.maxTargetDistance)
        {
            // set the target distance to the maximum allowed
            this.currentTargetDistance = this.maxTargetDistance;
        }

    }

}
