using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerManager : Singleton<ControllerManager>
{
    // Input Actions
    private InputAction controllerStick;
    private InputAction aButton;

    // Controller Cursor
    private GameObject gameCursor;
    private float cursorX;
    private float cursorY;
    private float stickX;
    private float stickY;
    // Value that determines speed of cursor
    public float cursorSpeed;
    // Value that determines sensitivity of cursor to user input
    // Lower value equals higher sensitivity
    public float cursorSensitivity;

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

        // Sets cursor at the origin
        cursorX = 0f;
        cursorY = 0f;
        stickX = 0f;
        stickY = 0f;

        // Set speed of cursor
        cursorSpeed = 3.0f;

        // Set sensitivity of cursor
        cursorSensitivity = 0.5f;

        // Locates the Cursor object in the scene
        FindCursor();

        // Hides the cursor
        gameCursor.SetActive(false);
    }

    // Finds the cursor object in the scene
    private void FindCursor()
    {
        // Break out of method if cursor is already found
        if(gameCursor)
        {
            return;
        }

        gameCursor = GameObject.Find("Cursor");
        gameCursor.GetComponent<RectTransform>().anchoredPosition = new Vector2(cursorX, cursorY);
    }

    // Updates position of cursor when necessary
    private void UpdateCursor()
    {
        // Ensures the cursor doesn't leave the screen
        if(cursorX >= 910f)
        {
            cursorX = 910f;
        }
        else if(cursorX <= -910f)
        {
            cursorX = -910f;
        }

        if(cursorY >= 480f)
        {
            cursorY = 480f;
        }
        else if(cursorY <= -480f)
        {
            cursorY = -480f;
        }

        gameCursor.GetComponent<RectTransform>().anchoredPosition = new Vector2(cursorX, cursorY);
    }

    // Notes!
    // In main menu, Min X is -910, Min Y is -480
    // Max is 910, 480

    // Update is called once per frame
    void Update()
    {
        // Relocate the cursor
        FindCursor();

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

        if (GlobalVar.Instance.isControllerConnected)
        {
            gameCursor.SetActive(true);
        }
        else
        {
            gameCursor.SetActive(false);
        }
        
        // Reads movement from X value of stick
        if(stickX >= cursorSensitivity)
        {
            cursorX += cursorSpeed;
            UpdateCursor();
        }
        else if(stickX <= -cursorSensitivity)
        {
            cursorX -= cursorSpeed;
            UpdateCursor();
        }

        // Reads movement from Y value of stick
        if(stickY >= cursorSensitivity)
        {
            cursorY += cursorSpeed;
            UpdateCursor();
        }
        else if(stickY <= -cursorSensitivity)
        {
            cursorY -= cursorSpeed;
            UpdateCursor();
        }
    }

    // Reads value of the left stick
    public void LeftStick(InputAction.CallbackContext context)
    {
        //Debug.Log(context.ReadValue<Vector2>());
        stickX = Mathf.Round(context.ReadValue<Vector2>().x * 100f) / 100f;
        stickY = Mathf.Round(context.ReadValue<Vector2>().y * 100f) / 100f;
        //Debug.Log("(" + stickX + ", " + stickY + ")");
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
