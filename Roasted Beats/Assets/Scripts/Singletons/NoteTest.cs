using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NoteTest : MonoBehaviour
{
    private GameObject note;
    private bool shiftPressed;

    // Start is called before the first frame update
    void Start()
    {
        // Initializes note object
        // NOTE: Not scalable, talk with Joe about merging his code with yours.
        foreach(Transform child in transform)
        {
            Note noteObject = child.GetComponent<Note>();
            if (noteObject == null) { 
                continue;
            }
            else
            {
                note = noteObject.gameObject;
                Debug.Log(note.name);
            }
        }

        shiftPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Working currently on Input system, trying to get each button press to only call the action once. (Currently executes twice)
    public void CreateNote(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            shiftPressed = true;
        }
        
        if (context.canceled)
        {
            shiftPressed = false;
            Debug.Log("Shift Released");
        }

        if (shiftPressed)
        {
            Debug.Log("Note Created");
        }
    }
}
