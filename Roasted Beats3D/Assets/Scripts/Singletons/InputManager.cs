using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : Singleton<InputManager>
{
    private PlayerInput playerInput;

    public PlayerInput PlayerInput
    {
        get
        {
            if (playerInput == null)
            {
                LoadInputs();
            }

            return playerInput;
        }
    }

    public void LoadInputs()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInput.actions = (Resources.Load<InputActionAsset>("Inputs/Roasted Beats")) as InputActionAsset;
        playerInput.notificationBehavior = PlayerNotifications.InvokeUnityEvents;
    }
}
