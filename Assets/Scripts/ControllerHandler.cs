using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine;

public class ControllerHandler : Player
{
    protected InputDevice Device;
    public bool IsInitialized = false;

    public void SetDevice(InputDevice device)
    {
        Device = device; 
    }

    public InputDevice GetDevice()
    {
        return Device;
    }

    public void Initialize()
    {
        IsInitialized = true;
    }

    public float[] getAxis() {
        float[] axis = new float[2];
        axis[0] = 0;
        axis[1] = 0;

        if (Device != null)
        {
            axis[0] = Device.LeftStickX;
            axis[1] = Device.LeftStickY;
        }

        return axis;
    }

    public bool moveForward()
    {
        var axis = getAxis();
        return axis[1] > 0;
    }

    public bool moveForwardReleased()
    {
        var axis = getAxis();
        return axis[1] == 0;
    }

    public bool moveBackward()
    {
        var axis = getAxis();
        return axis[1] < 0;
    }

    public bool moveBackwardReleased()
    {
        var axis = getAxis();
        return axis[1] == 0;
    }

    public bool turnRight()
    {
        var axis = getAxis();
        return axis[0] > 0;
    }

    public bool turnRightReleased()
    {
        var axis = getAxis();
        return axis[0] == 0;
    }

    public bool turnLeft()
    {
        var axis = getAxis();
        return axis[0] < 0;
    }

    public bool turnLeftReleased()
    {
        var axis = getAxis();
        return axis[0] == 0;
    }

    public bool jumpGetupPressed()
    {
        return Device.Action1.WasPressed;
    }

    public bool jumpGetupReleased()
    {
        return Device.Action1.WasReleased;
    }

    public bool punchRightPressed()
    {
        return Device.RightTrigger.WasPressed;
    }

    public bool punchRightReleased()
    {
        return Device.RightTrigger.WasReleased;
    }

    public bool punchLeftPressed()
    {
        return Device.LeftTrigger.WasPressed;
    }

    public bool punchLeftReleased()
    {
        return Device.LeftTrigger.WasReleased;
    }

    public bool reachRightPressed()
    {
        return Device.RightBumper.IsPressed;
    }

    public bool reachRightReleased()
    {
        return Device.RightBumper.WasReleased;
    }

    public bool reachLeftPressed()
    {
        return Device.LeftBumper.IsPressed;
    }

    public bool reachLeftReleased()
    {
        return Device.LeftBumper.WasReleased;
    }

    public bool pickupThrowPressed()
    {
        return Device.Action4.WasPressed;
    }

    public bool pickupThrowReleased()
    {
        return Device.Action4.WasReleased;
    }
}
