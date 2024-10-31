using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ObjectInteraction : MonoBehaviour
{
    [SerializeField] private LayerMask interactionLayer;

    public UnityEvent OnInteraction;
    public UnityEvent<bool> OnHover;

    public static bool isPaused;

    public bool HoveredOver { get; private set; }

    private void Start()
    {
        //AddInput();
    }

    //private void AddInput()
    //{
    //    InputActionMap input = InputManager.Instance.PlayerInput.actions.FindActionMap("Player");

    //    if (input != null)
    //    {
    //        InputAction action = input.FindAction("LMB");

    //        if (action != null)
    //        {
    //            action.Enable();
    //            action.performed += CheckCollision;
    //        }
    //    }
    //}

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Update()
    {
        if (Time.timeScale == 0) isPaused = true;
        else if (Time.timeScale == 1) isPaused = false;

        if (isPaused)
            return;

        // Send hover status to any listeners
        OnHover.Invoke(HoveredOver = CheckHoverStatus());

        CheckCollision();
    }

    /// <summary>
    /// Checks for ray collision using an input
    /// </summary>
    /// <param name="context"></param>
    void CheckCollision()
    {
        if (HoveredOver && Input.GetMouseButtonDown(0))
       {
            Debug.Log(String.Format("Clicked {0}", transform.name));
            OnInteraction.Invoke();
       }
    }

    /// <summary>
    /// Sends ray out from camera mouse position to check for collision
    /// </summary>
    /// <returns></returns>
    private bool CheckHoverStatus()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;

        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, interactionLayer))
        {
            GameObject objInt = hit.collider.gameObject;

            while (objInt.transform.parent != null && objInt.name != this.gameObject.name)
            {
                objInt = objInt.transform.parent.gameObject;
            }

            //Debug.Log(string.Format("hovering over {0}", transform.name));

            return this.gameObject == objInt;
        }

        return false;
    }
}
