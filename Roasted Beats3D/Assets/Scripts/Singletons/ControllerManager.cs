using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerManager : Singleton<ControllerManager>
{
    // Input Actions
    InputAction controllerStick;
    InputAction aButton;

    // Start is called before the first frame update
    void Start()
    {
        // Setting up left stick
        controllerStick = InputManager.Instance.PlayerInput.actions.FindActionMap("Player").FindAction("ControllerStick");
        controllerStick.Enable();
        controllerStick.performed += LeftStick;

        // Setting up A Button
        aButton = InputManager.Instance.PlayerInput.actions.FindActionMap("Player").FindAction("ControllerAButton");
        aButton.Enable();
        aButton.performed += AButtonPressed;
        aButton.canceled += AButtonReleased;
    }

    // Update is called once per frame
    void Update()
    {
        // Detects if controller is connected or not
        if(Input.GetJoystickNames()[0] == "")
        {
            GlobalVar.Instance.isControllerConnected = false;
        }
        else
        {
            GlobalVar.Instance.isControllerConnected = true;
        }
        //Debug.Log(GlobalVar.Instance.isControllerConnected);
        //Debug.Log(GlobalVar.Instance.aButtonPressed);
    }

    // Reads value of the left stick
    public void LeftStick(InputAction.CallbackContext context)
    {
        //Debug.Log(context.ReadValue<Vector2>());
        GlobalVar.Instance.stickX = Mathf.Round(context.ReadValue<Vector2>().x * 100f) / 100f;
        GlobalVar.Instance.stickY = Mathf.Round(context.ReadValue<Vector2>().y * 100f) / 100f;
        Debug.Log("(" + GlobalVar.Instance.stickX + ", " + GlobalVar.Instance.stickY + ")");
    }

    // A Button Pressed
    public void AButtonPressed(InputAction.CallbackContext context)
    {
        // While true, A Button is held down
        GlobalVar.Instance.aButtonPressed = true;
        //Only goes off once when button is pressed!
        //Debug.Log("A Button Pressed");
    }

    // A Button Released
    public void AButtonReleased(InputAction.CallbackContext context)
    {
        // While false, A Button is not being held down
        GlobalVar.Instance.aButtonPressed = false;
    }
}
