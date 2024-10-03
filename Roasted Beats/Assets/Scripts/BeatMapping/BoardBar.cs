using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBar : MonoBehaviour
{
    [SerializeField] private List<GameObject> NotePrefabs;

    [SerializeField] private Transform StartPos;
    [SerializeField] private Transform EndPos;

    private float noteCooldown;
    private float speed;
    private float travelDistance;

    private List<GameObject> notes;

    private void Start()
    {
        notes = new List<GameObject>();

        // We get travel distance to know exactly how far we need the note to go before it gets pressed perfectly
        travelDistance = Vector3.Distance(StartPos.position, EndPos.position);
    }

    private void Update()
    {
        //noteCooldown -= Time.deltaTime;

        // We calculate speed each frame just because its easier if we need to change it in global
        if (travelDistance == 0) return;

        speed = travelDistance / GlobalVar.Instance.noteSpdInSec;
    }

    public void OnUpdate()
    {
        foreach (GameObject item in notes)
        {
            item.GetComponent<Note>().OnUpdate();
        }

        // Check collision for each note
    }

    public void CreateNote(int prefabIndex)
    {
        //if (noteCooldown > 0) return; 
        notes.Add(Instantiate(NotePrefabs[prefabIndex], StartPos.position, transform.rotation, transform));

        Vector3 direction = Vector3.Normalize(EndPos.position - StartPos.position);

        notes[notes.Count - 1].GetComponent<Note>().CreateNote(speed, direction);
        //noteCooldown = GlobalVar.Instance.noteCoolDown;
    }

    public void CheckNoteCollision()
    {
        for (int i = 0; i < notes.Count; i++)
        {
            float distance = Vector3.Distance(notes[i].transform.position, EndPos.position);

            // Joe's notes
            // Try using Vector3.Distance between note and the goal to get a float and use (boolean value) ? (if true) : (if false),
            // this can be chained to look something like this 

            // (boolean value) ? (action if true) :
            // (boolean value) ? (action if true) :
            // (boolean value) ? (action if true) :
            // (boolean value) ? (action if true) :
            // (boolean value) ? (action if true) :
            // (action if false);

            // If you want more accurate than just distance since we don't want to substract points from the player if they are just pressing them with no notes in front
            // You could add an extra layer of checking then like if "y > (some threshhold) { Don't count this as a miss; } 

            // Checks if the note should be scored
            if (notes[i].transform.position.y >= -3.5f && notes[i].transform.position.y <= -1.5f)
            {
                // Perfect
                if (notes[i].transform.position.y >= -2.6f && notes[i].transform.position.y <= -2.4f)
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
                else if (notes[i].transform.position.y > -1.75 && notes[i].transform.position.y <= -1.5 ||
                    notes[i].transform.position.y < -3.25 && notes[i].transform.position.y >= -3.5)
                {
                    Debug.Log("Miss");
                }


            }
        }
    }
}
