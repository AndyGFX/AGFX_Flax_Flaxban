using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game;

/// <summary>
/// Chest Script.
/// </summary>
public class TChest
{
    public Actor actor;
    public Actor locker;
    public Vector3 worldPosition;
    public Vector2 gridPosition;
    

    public void LockChest()
    {
        this.locker.IsActive = true;
        Debug.Log("Chest Locked");
    }

    public void UnLockChest()
    {
        this.locker.IsActive = false;
        Debug.Log("Chest UN-Locked");
    }

}
