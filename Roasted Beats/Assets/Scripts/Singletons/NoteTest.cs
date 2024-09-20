using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteTest : MonoBehaviour
{
    private GameObject note;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
