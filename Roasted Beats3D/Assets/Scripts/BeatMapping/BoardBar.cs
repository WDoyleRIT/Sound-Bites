using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoardBar : MonoBehaviour
{
    [SerializeField] private List<GameObject> NotePrefabs;

    [SerializeField] private Transform StartPos;
    [SerializeField] public Transform EndPos;
    [SerializeField] private Transform MissedPos;

    private float noteCooldown;
    private float speed;
    private float travelDistance;

    private List<GameObject> notes = new List<GameObject>();

    private void Start()
    {
        notes = new List<GameObject>();

        // We get travel distance to know exactly how far we need the note to go before it gets pressed perfectly
        travelDistance = Vector3.Distance(StartPos.position, EndPos.position);
    }

    private void Update()
    {
        noteCooldown -= Time.deltaTime;

        // We calculate speed each frame just because its easier if we need to change it in global
        if (travelDistance == 0) return;

        speed = travelDistance / GlobalVar.Instance.noteSpdInSec;
    }

    public void ChangeRing(int i, InputAction.CallbackContext context)
    {
        GameObject ring = EndPos.gameObject;

        //Debug.Log(String.Format("Bar Press {0}", i));

        //ring.GetComponent<SpriteRenderer>().sortingOrder = 0;

        //Debug.Log(ring.transform.name);
        //Debug.Log(ring);

        if (context.performed)
        {
            ring.SetActive(false);
            StartCoroutine(SetRingTrue(.1f));
        }
            
        Debug.Log(ring.activeSelf);
    }

    /// <summary>
    /// Will set ring to true after a set amount of time
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    private IEnumerator SetRingTrue(float time)
    {
        yield return new WaitForSeconds(time);

        EndPos.gameObject.SetActive(true);
    }

    public void OnUpdate()
    {
        for (int i = 0; i < notes.Count; i++)
        {
            RhythmManager currentLvl = GameManager.Instance.CurrentLevel;

            notes[i].GetComponent<Note>().OnUpdate();

            // Checking direction of note to the missed position, and if it isn't the way the note is traveling, the note will count as missed
            Vector3 dirToMiss = Vector3.Normalize(MissedPos.position - notes[i].transform.position);
            //string miss = dirToMiss.ToString();
            Vector3 dirToEnd = Vector3.Normalize(MissedPos.position - StartPos.position);
            //string end = dirToEnd.ToString();

            if (dirToMiss.y > 0)
            {
                //Debug.Log("Missed!");
                currentLvl.ChangeScoreBy(-1000);
                Destroy(notes[i]);
                notes.RemoveAt(i);
                i--;
                GlobalVar.Instance.notesPassed++;
            }
        }

        // Check collision for each note
    }

    public void CreateNote(int prefabIndex)
    {
        if (noteCooldown > 0) return;
        notes.Add(Instantiate(NotePrefabs[prefabIndex], StartPos.position + new Vector3(0,0,-.4f), transform.rotation, transform));

        Vector3 direction = Vector3.Normalize(EndPos.position - StartPos.position);

        notes[notes.Count - 1].GetComponent<Note>().CreateNote(speed, direction);
        noteCooldown = GlobalVar.Instance.noteCoolDown;
    }

    public void CheckNoteCollision()
    {
        //for (int i = 0; i < notes.Count; i++)
        //{
        if (notes.Count <= 0) return;

            float distance = Vector2.Distance(notes[0].transform.position, EndPos.position);

            RhythmManager currentLvl = GameManager.Instance.CurrentLevel;

            
            int score =
                (distance <= 0.05) ? 1000 :
                (distance < .15) ? 500 :
                (distance < .35) ? 100 :
                (distance < .5) ? 50 :
                (distance < .75) ? 10 :
                (distance < .95) ? 5 :
                -100;

            if (score > 0)
            {
                Destroy(notes[0]);
                notes.RemoveAt(0);
                GlobalVar.Instance.notesPassed++;
            }

            currentLvl.ChangeScoreBy(score);
        //}
    }
}
