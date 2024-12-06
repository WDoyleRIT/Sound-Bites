using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerManager : Singleton<ControllerManager>
{
    // Main Input Actions
    private InputAction controllerStick;
    private InputAction stickPress;
    private InputAction aButton;

    // DPad Input Actions
    private InputAction padUp;
    private InputAction padDown;
    private InputAction padLeft;
    private InputAction padRight;

    // DPad Bools
    private bool isPadUp;
    private bool isPadDown;
    private bool isPadLeft;
    private bool isPadRight;

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

    // The mouse
    private Mouse mouse = Mouse.current;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize booleans
        isPadUp = false;
        isPadDown = false;
        isPadLeft = false;
        isPadRight = false;

        // Setting up left stick
        controllerStick = InputManager.Instance.PlayerInput.actions.FindActionMap("Player").FindAction("ControllerStick");
        controllerStick.Enable();
        controllerStick.performed += LeftStick;

        stickPress = InputManager.Instance.PlayerInput.actions.FindActionMap("Player").FindAction("StickDown");
        stickPress.Enable();
        stickPress.performed += StickPressed;

        // Setting up A Button
        aButton = InputManager.Instance.PlayerInput.actions.FindActionMap("Player").FindAction("ControllerAButton");
        aButton.Enable();
        aButton.performed += AButtonPressed;
        //aButton.canceled += AButtonReleased;

        // Setting up D Pad
        padUp = InputManager.Instance.PlayerInput.actions.FindActionMap("Player").FindAction("DPadUp");
        padUp.Enable();
        padUp.performed += UpPressed;
        padUp.canceled += UpReleased;

        padDown = InputManager.Instance.PlayerInput.actions.FindActionMap("Player").FindAction("DPadDown");
        padDown.Enable();
        padDown.performed += DownPressed;
        padDown.canceled += DownReleased;

        padLeft = InputManager.Instance.PlayerInput.actions.FindActionMap("Player").FindAction("DPadLeft");
        padLeft.Enable();
        padLeft.performed += LeftPressed;
        padLeft.canceled += LeftReleased;

        padRight = InputManager.Instance.PlayerInput.actions.FindActionMap("Player").FindAction("DPadRight");
        padRight.Enable();
        padRight.performed += RightPressed;
        padRight.canceled += RightReleased;

        // Sets cursor at the origin
        //cursorX = 0f;
        //cursorY = 0f;
        stickX = 0f;
        stickY = 0f;

        // Set speed of cursor
        cursorSpeed = 2.5f;

        // Set sensitivity of cursor
        cursorSensitivity = 0.6f;

        // Locates the Cursor object in the scene
        //FindCursor();

        // Hides the cursor
        //gameCursor.SetActive(false);

        Debug.Log(Screen.width + " " + Screen.height);
    }

    // Finds the cursor object in the scene
    /*private void FindCursor()
    {
        // Break out of method if cursor is already found
        if(gameCursor)
        {
            return;
        }

        gameCursor = GameObject.Find("Cursor");
        gameCursor.GetComponent<RectTransform>().anchoredPosition = new Vector2(cursorX, cursorY);  
        gameCursor.SetActive(false);
    }*/

    // Updates position of cursor when necessary
    private void UpdateCursor()
    {
        // Ensures the cursor doesn't leave the screen
        if(cursorX >= Screen.width)
        {
            cursorX = Screen.width;
        }
        else if(cursorX <= 0)
        {
            cursorX = 0;
        }

        if(cursorY >= Screen.height)
        {
            cursorY = Screen.height;
        }
        else if(cursorY <= 0)
        {
            cursorY = 0;
        }

        //gameCursor.GetComponent<RectTransform>().anchoredPosition = new Vector2(cursorX, cursorY);
        mouse.WarpCursorPosition(new Vector2(cursorX, cursorY));
        //Debug.Log(Input.mousePosition);
    }

    // Update is called once per frame
    void Update()
    {
        // Relocate the cursor
        //FindCursor();

        // Known Bug! Will stay false until controller is turned off and on again
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

        /*if (GlobalVar.Instance.isControllerConnected)
        {
            gameCursor.SetActive(true);
        }
        else
        {
            gameCursor.SetActive(false);
        }*/
        
        // Reads movement from X value of stick
        if(stickX >= cursorSensitivity || isPadRight)
        {
            cursorX += cursorSpeed;
            UpdateCursor();
        }
        
        if(stickX <= -cursorSensitivity || isPadLeft)
        {
            cursorX -= cursorSpeed;
            UpdateCursor();
        }

        // Reads movement from Y value of stick
        if(stickY >= cursorSensitivity || isPadUp)
        {
            cursorY += cursorSpeed;
            UpdateCursor();
        }

        //Debug.Log(new Vector2(stickX, stickY));
        
        if(stickY <= -cursorSensitivity || isPadDown)
        {
            cursorY -= cursorSpeed;
            UpdateCursor();
        }
    }

    // Reads value of the left stick
    // NOTE: Values don't always reset when stick is reset, which causes cursor to keep moving
    public void LeftStick(InputAction.CallbackContext context)
    {
        //Debug.Log(context.ReadValue<Vector2>());
        stickX = Mathf.Round(context.ReadValue<Vector2>().x * 100f) / 100f;
        stickY = Mathf.Round(context.ReadValue<Vector2>().y * 100f) / 100f;
        //Debug.Log("(" + stickX + ", " + stickY + ")");
    }

    public void StickPressed(InputAction.CallbackContext context)
    {
        cursorX = Screen.width / 2;
        cursorY = Screen.height / 2;
        UpdateCursor();
        //mouse.WarpCursorPosition(new Vector2(cursorX, cursorY));
    }

    // A Button Pressed
    public void AButtonPressed(InputAction.CallbackContext context)
    {
        // While true, A Button is held down
        GlobalVar.Instance.aButtonPressed = true;
        //Only goes off once when button is pressed!
        //Debug.Log("A Button Pressed");

        // Casts a ray from the camera to see what mouse has collided with
        var rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider) return;

        var item = rayHit.collider.gameObject;
        // Big switch statement for all interactable objects
        Debug.Log(item.name);
        switch (item.name)
        {
            case "NewGame":
                SceneManaging.Instance.OpenLvl("RestaurantSelect");
                TutorialSaveInfo.Instance.SaveBasicData();
                item.GetComponent<SquishEffect>().OnClick();
                break;
            case "Quit":
                Application.Quit();
                item.GetComponent<SquishEffect>().OnClick();
                break;
            // Doesn't work (Think its an issue with the large canvas)
            case "OpenLvl":
                // Hardcoded to open level 1
                SceneManaging.Instance.OpenLvl("Cafe_Orders");
                item.GetComponent<SquishEffect>().OnClick();
                break;
            // Same as the open level button
            case "Settings":
                GameObject menu = GameObject.Find("Menu");
                menu.GetComponent<Menu>().ChangeWindow();
                item.GetComponent<SquishEffect>().OnClick();
                break;
            case "BrennanCheckoutNote(Clone)":
                item.GetComponentInParent<CheckoutButton>().OnClick();
                break;
        }
    }

    // A Button Released
    /*public void AButtonReleased(InputAction.CallbackContext context)
    {
        // While false, A Button is not being held down
        GlobalVar.Instance.aButtonPressed = false;
    }*/

    // Work inconsistently, for some reason inputs are cancelled right as they are pressed
    #region DPad Inputs
    // DPad Up
    public void UpPressed(InputAction.CallbackContext context)
    {
        isPadUp = true;
        //Debug.Log("Up Pressed");
    }

    public void UpReleased(InputAction.CallbackContext context)
    {
        isPadUp = false;
        //Debug.Log("Up Released");
    }

    // DPad Down
    public void DownPressed(InputAction.CallbackContext context)
    {
        isPadDown = true;
        //Debug.Log("Down Pressed");
    }

    public void DownReleased(InputAction.CallbackContext context)
    {
        isPadDown = false;
        //Debug.Log("Down Released");
    }

    // DPadLeft
    public void LeftPressed(InputAction.CallbackContext context)
    {
        isPadLeft = true;
        //Debug.Log("Left Pressed");
    }

    public void LeftReleased(InputAction.CallbackContext context)
    {
        isPadLeft = false;
        //Debug.Log("Left Released");
    }

    // DPad Right
    public void RightPressed(InputAction.CallbackContext context)
    {
        isPadRight = true;
        //Debug.Log("Right Pressed");
    }

    public void RightReleased(InputAction.CallbackContext context)
    {
        isPadRight = false;
        //Debug.Log("Right Released");
    }
    #endregion
}
