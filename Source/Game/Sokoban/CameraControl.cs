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

    private Vector3 deltaMove = new Vector3(0, 0, 0);
    /// <summary>
    /// Updates the camera's position and handles zoom input.
    /// </summary>
    public void UpdateCameraControl()
    {
         this.Actor.Position += this.deltaMove;
        this.Actor.Position = GLOBAL.PLAYER.Actor.Position - this.Actor.LocalTransform.Forward * this.currentTargetDistance;

        if (Input.GetAction("ZoomUp")) { GLOBAL.CAMERA.ZoomUp(); }
        if (Input.GetAction("ZoomDown")) { GLOBAL.CAMERA.ZoomDown(); }
    }

    /// <summary>
    /// Sets up camera's initial position and orientation.
    /// </summary>
    /// <remarks>
    /// Sets camera position to the player's position, adds the specified offset to it,
    /// sets the camera's Euler angles to the specified orientation, and makes the camera look at the player.
    /// </remarks>
    public void PrepareCameraOnStart()
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


    /// <summary>
    /// Prepares camera for zooming.
    /// </summary>
    /// <remarks>
    /// Sets camera position to the maximum target distance, sets the start and end camera positions
    /// for the zoom tween, and calculates the current target distance to the player.
    /// </remarks>
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

 
    public void SetDeltaMove(Vector3 delta)
    {
        this.deltaMove = delta;
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
