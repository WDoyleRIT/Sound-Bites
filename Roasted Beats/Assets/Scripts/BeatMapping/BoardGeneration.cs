using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoardGeneration : MonoBehaviour
{
    // Prefab for note bar
    [SerializeField] private GameObject barPrefab;
    //[SerializeField] private NoteTest gameManager;

    // List of bar objects
    private List<GameObject> bars;
    // Make a list of notes!!!!

    private void OnValidate()
    {
        // In editor every time the editor updates
        // It'll gen the bars, did this so we can see how the board looks before playing
        if (transform.childCount > 0) return;

        GenerateBars(4);
    }

    #region Delete
    // Different executions to delete bars
    private void DeleteAllBars()
    {
        if (Application.isEditor)
        {
            Debug.Log($"Deleting {transform.childCount} bars.");
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                GameObject.DestroyImmediate(transform.GetChild(i).gameObject);
            }
            Debug.Log("Deletion complete.");
        }
        else
        {
            if (bars != null)
            {
                for (int i = 0; i < bars.Count; i++)
                {
                    GameObject.Destroy(bars[i]);
                }

                bars.Clear();
            }
        }

        bars = new List<GameObject>();
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        DeleteAllBars();
        GenerateBars(4);

        //InputManager.Instance.LoadInputs();

        //var input = InputManager.Instance.playerInput.actions.FindActionMap("Player");

        /*if (input != null )
        {
            var action = input.FindAction("Hit Note1");

            if (action != null)
            {
                action.Enable();
                action.performed += HitNotes;
            }
        }*/
    }

    public void HitNote1(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Bar 1 Press");
            // CheckCollision();
        }
    }

    public void HitNote2(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Bar 2 Press");
            // CheckCollision();
        }
    }

    public void HitNote3(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Bar 3 Press");
            // CheckCollision();
        }
    }

    public void HitNote4(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Bar 4 Press");
            // CheckCollision();
        }
    }

    // To consider: change Check Collision to only check the appropriate bar instead of all bars every time
    private void CheckCollision()
    {
        foreach (GameObject bar in bars)
        {
            bar.GetComponent<BoardBar>().CheckNoteCollision();
        }
    }



    private void GenerateBars(int numBars) {

        bars = new List<GameObject>();

        // Caps the number of bars at 5
        if (numBars >= 5)
        {
            numBars = 5;
        }

        // Float that represents half the width of entire notebar
        // This will have to be changed when we use actual assets for the notebar
        float half = barPrefab.transform.localScale.x  * (float)numBars / 2 - (barPrefab.transform.localScale.x / 2);

        // Create bars equal to the parameter
        for (int i = 0; i < numBars; i++)
        {
            float barX = barPrefab.transform.localScale.x * (float)i - half;
            bars.Add(Instantiate(barPrefab, new Vector3(transform.position.x + barX, transform.position.y, transform.position.z), transform.rotation, transform));
        }
    }

    private void Update()
    {
        for (int i = 0;i < bars.Count;i++)
        {
            bars[i].GetComponent<BoardBar>().OnUpdate();
        }
    }

    // Update is called once per frame
    public void OnBeatUpdate(List<bool> notes)
    {
        for (int i = 0; i < bars.Count; i++)
        {
            if (notes[i])
            {
                bars[i].GetComponent<BoardBar>().CreateNote(i);
            }
        } 
    }
}
