

**CameraControl Class Documentation**
=====================================

**Summary**
-----------

The `CameraControl` class is a script responsible for controlling the camera's position and orientation in the game. It provides methods for updating the camera's position, handling zoom input, and setting up the camera's initial position and orientation.

**Methods**
-----------

### `UpdateCameraControl`

Updates the camera's position and handles zoom input.

### `PrepareCameraOnStart`

Sets up the camera's initial position and orientation.

* Sets the camera position to the player's position, adds the specified offset to it.
* Sets the camera's Euler angles to the specified orientation.
* Makes the camera look at the player.

### `PrepareZoom`

Prepares the camera for zooming.

* Sets the camera position to the maximum target distance.
* Sets the start and end camera positions for the zoom tween.
* Calculates the current target distance to the player.

### `SetDeltaMove`

Sets the delta move vector for the camera.

### `ZoomUp`

Zooms the camera up by the specified amount.

* Subtracts the zoom speed from the current target distance.
* Checks if the target distance is less than the minimum allowed.

### `ZoomDown`

Zooms the camera down by the specified amount.

**Fields**
----------

* `offset`: The offset vector applied to the camera's position.
* `orientation`: The initial Euler angles of the camera.
* `maxTargetDistance`: The maximum distance the camera can zoom out to.
* `minTargetDistance`: The minimum distance the camera can zoom in to.
* `zoomKeySpeed`: The speed at which the camera zooms in or out.
* `startCameraPosition`: The starting position of the camera for the zoom tween.
* `endCameraPosition`: The ending position of the camera for the zoom tween.
* `currentTargetDistance`: The current distance between the camera and the player.
* `deltaMove`: The delta move vector for the camera.

**Usage**
---------

To use the `CameraControl` class, simply attach it to a camera game object in your scene. You can then call the various methods to control the camera's position and orientation.

For example, to set up the camera's initial position and orientation, you can call the `PrepareCameraOnStart` method:
```csharp
public class CameraControl : Script
{
    public void Start()
    {
        PrepareCameraOnStart();
    }
}
```
To zoom the camera in or out, you can call the `ZoomUp` or `ZoomDown` methods:
```csharp
public class CameraControl : Script
{
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ZoomUp();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            ZoomDown();
        }
    }
}
```