using System.Collections;
using System.Collections.Generic;
using System;
//using System.Diagnostics; Commented out because of weird Debug error
using UnityEngine;
using UnityEngine.InputSystem;

public class NoteTest : MonoBehaviour
{
    // NOTE: Instatiating and destroying objects repeatedly is very costly!
    // Consider just instantiate a set number of notes and repeatedly disabling
    // and enabling them using SetActive()

    // Note Prefab
    [SerializeField] private GameObject notePrefab;
    // Circle at bottom of HUD
    [SerializeField] private GameObject hitMarker;

    // List of notes
    [SerializeField] private List<GameObject> notes = new List<GameObject>();
    // Number of notes hit each time the button is pressed
    private int notesHit;

    // Start is called before the first frame update
    void Start()
    {
        notesHit = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Check all notes and destroy any that fall under the map
        for(int i = 0; i < notes.Count; i++)
        {
            if(notes[i].transform.position.y <= -4)
            {
                Destroy(notes[i].gameObject);
                notes.RemoveAt(i);
                Debug.Log("Miss");
            }
        }
    }

    // Working currently on Input system, trying to get each button press to only call the action once. (Currently executes twice)
    public void CreateNote(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            // Create a note
            GameObject note = Instantiate(notePrefab, new Vector3(0,2.5f,-0.2f), Quaternion.identity);
            notes.Add(note);
            //Debug.Log(notes.Count);
            //Debug.Log("Note Created");
        }
    }

    // Currently only changes color of marker at bottom when pressed or held down
    public void HitNote(InputAction.CallbackContext context)
    {
        // NOTES: -1.5 Y or higher is a complete miss, as is any Y value -3.5 or lower.
        // Anything between -2.4 and -2.6 is considered a Perfect hit (90% Accuracy)
        // Great hits are > -2.4 but <= -2.25 OR < -2.6 or >= -2.75 (75% Accuracy)
        // Good hits are > -2.25 but <= -2 OR < -2.75 but >= -3 (50% Accuracy)
        // "Meh" hits are > -2 but <= -1.75 OR < -3 but >= -3.25 (25% Accuracy)
        // Anything else is considered a miss, but these values are subject to change

        if (context.started)
        {
            hitMarker.SetActive(false);
            // Determines the quality of the hit, if there was one
            for (int i = 0; i < notes.Count; i++)
            {
                // Checks if the note should be scored
                if(notes[i].transform.position.y >= -3.5f && notes[i].transform.position.y <= -1.5f)
                {
                    // Perfect
                    if(notes[i].transform.position.y >= -2.6f && notes[i].transform.position.y <= -2.4f)
                    {
                        Debug.Log("Perfect!!");
                    }
                    // Great
                    else if (notes[i].transform.position.y > -2.4f && notes[i].transform.position.y <= -2.25f ||
                        notes[i].transform.position.y < -2.6f && notes[i].transform.position.y >= -2.75f)
                    {
                        Debug.Log("Great!");
                    }
                    // Good
                    else if (notes[i].transform.position.y > -2.25f && notes[i].transform.position.y <= -2 ||
                        notes[i].transform.position.y < -2.75f && notes[i].transform.position.y >= -3)
                    {
                        Debug.Log("Good");
                    }
                    // Meh
                    else if (notes[i].transform.position.y > -2 && notes[i].transform.position.y <= -1.75 ||
                        notes[i].transform.position.y < -3 && notes[i].transform.position.y >= -3.25) 
                    {
                        Debug.Log("Meh");
                    }
                    // On near miss (Between 0 and 25 accuracy), destroy the note anyway (Like everything else is subject to change)
                    else if(notes[i].transform.position.y > -1.75 && notes[i].transform.position.y <= -1.5 ||
                        notes[i].transform.position.y < - 3.25  && notes[i].transform.position.y >= -3.5)
                    {
                        Debug.Log("Miss");
                    }
                    // No matter what, destroy the note and increment the counter
                    notesHit++;
                    Destroy(notes[i].gameObject);
                    notes.RemoveAt(i);
                }
            }

            if(notesHit == 0)
            {
                Debug.Log("Miss");
            }
            //Debug.Log("Note Hit Attempt");
        }

        if (context.canceled)
        {
            // Reserts hit counter and hides hit marker
            notesHit = 0;
            hitMarker.SetActive(true);
        }
    }
}
