using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public TextMeshPro ratingText;

    private void OnValidate()
    {
        if (!Application.isEditor) return;
        // In editor every time the editor updates
        // It'll gen the bars, did this so we can see how the board looks before playing
        if (transform.childCount > 0) return;

        GenerateBars(4);
    }

    #region Delete
    // Different executions to delete bars
    private void DeleteAllBars()
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

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
        RhythmManager.Instance.ratingText = ratingText;
        RhythmManager.Instance.sm.frequencyAveraging.OnSpawnBeat.RemoveAllListeners();
        RhythmManager.Instance.sm.frequencyAveraging.OnSpawnBeat.AddListener(OnBeatUpdate);

        DeleteAllBars();
        GenerateBars(4);

        Debug.Log(transform.childCount);

        // Subscribes the inputs to the player actions
        SubscribeActions();
    }

    // Input related methods
    #region Input

    private void SubscribeActions()
    {
        var action = InputManager.Instance.PlayerInput.actions.FindActionMap("Player").FindAction("Hit Note1");
        
        action.Enable();
        action.performed += HitNote1;

        action = InputManager.Instance.PlayerInput.actions.FindActionMap("Player").FindAction("Hit Note2");
        action.Enable();
        action.performed += HitNote2;

        action = InputManager.Instance.PlayerInput.actions.FindActionMap("Player").FindAction("Hit Note3");
        action.Enable();
        action.performed += HitNote3;

        action = InputManager.Instance.PlayerInput.actions.FindActionMap("Player").FindAction("Hit Note4");
        action.Enable();
        action.performed += HitNote4;
    }

    private void ChangeRing(int i, InputAction.CallbackContext context)
    {
        //GameObject ring = bars[i].GetComponent<BoardBar>().EndPos.gameObject;

            //Debug.Log(String.Format("Bar Press {0}", i));
            // Checks collision of first bar
            
        //ring.GetComponent<SpriteRenderer>().sortingOrder = 0;

        if (context.performed)
        {
            CheckCollision(i);
        }

        //if (context.canceled)
        //{
        //    //GameObject ring = bars[i].GetComponent<BoardBar>().EndPos.gameObject;
        //    ring.GetComponent<SpriteRenderer>().sortingOrder = 2;

        //}

        bars[i].GetComponent<BoardBar>().ChangeRing(i, context);
    }

    public void HitNote1(InputAction.CallbackContext context)
    {
        ChangeRing(0, context);
    }

    public void HitNote2(InputAction.CallbackContext context)
    {
        ChangeRing(1, context);
    }

    public void HitNote3(InputAction.CallbackContext context)
    {
        ChangeRing(2, context);
    }

    public void HitNote4(InputAction.CallbackContext context)
    {
        ChangeRing(3, context);
    }

    #endregion

    // Will Doyle: Temporarily reworked this method so that it only checks the bar at a certain index
    private void CheckCollision(int index)
    {
        bars[index].GetComponent<BoardBar>().CheckNoteCollision();
        /*foreach (GameObject bar in bars)
        {
            bar.GetComponent<BoardBar>().CheckNoteCollision();
        }*/
    }


    /// <summary>
    /// Generates bars
    /// </summary>
    /// <param name="numBars"></param>
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
