using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine;

public class ControllerHandler : MonoBehaviour
{
    public InputDevice InputDevice;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var device in InputManager.Devices) {
            InputDevice = device;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float[] getAxis() {
        float[] axis = new float[2];
        axis[0] = 0;
        axis[1] = 0;

        if (InputDevice != null)
        {
            axis[0] = InputDevice.LeftStickX;
            axis[1] = InputDevice.LeftStickY;
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
        return InputDevice.Action1.WasPressed;
    }

    public bool jumpGetupReleased()
    {
        return InputDevice.Action1.WasReleased;
    }

    public bool punchRightPressed()
    {
        return InputDevice.RightTrigger.WasPressed;
    }

    public bool punchRightReleased()
    {
        return InputDevice.RightTrigger.WasReleased;
    }

    public bool punchLeftPressed()
    {
        return InputDevice.LeftTrigger.WasPressed;
    }

    public bool punchLeftReleased()
    {
        return InputDevice.LeftTrigger.WasReleased;
    }

    public bool reachRightPressed()
    {
        return InputDevice.RightBumper.IsPressed;
    }

    public bool reachRightReleased()
    {
        return InputDevice.RightBumper.WasReleased;
    }

    public bool reachLeftPressed()
    {
        return InputDevice.LeftBumper.IsPressed;
    }

    public bool reachLeftReleased()
    {
        return InputDevice.LeftBumper.WasReleased;
    }

    public bool pickupThrowPressed()
    {
        return InputDevice.Action4.WasPressed;
    }

    public bool pickupThrowReleased()
    {
        return InputDevice.Action4.WasReleased;
    }
}
