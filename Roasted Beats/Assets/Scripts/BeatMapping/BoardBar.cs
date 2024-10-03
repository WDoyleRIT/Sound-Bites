using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BoardBar : MonoBehaviour
{
    [SerializeField] private List<GameObject> NotePrefabs;

    [SerializeField] private Transform StartPos;
    [SerializeField] private Transform EndPos;
    [SerializeField] private Transform MissedPos;

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
        noteCooldown -= Time.deltaTime;

        // We calculate speed each frame just because its easier if we need to change it in global
        if (travelDistance == 0) return;

        speed = travelDistance / GlobalVar.Instance.noteSpdInSec;
    }

    public void OnUpdate()
    {
        for (int i = 0; i < notes.Count; i++)
        {
            LevelManager currentLvl = GameManager.Instance.currentLevel;

            notes[i].GetComponent<Note>().OnUpdate();

            // Checking direction of note to the missed position, and if it isn't the way the note is traveling, the note will count as missed
            Vector3 dirToMiss = Vector3.Normalize(MissedPos.position - notes[i].transform.position);
            //string miss = dirToMiss.ToString();
            Vector3 dirToEnd = Vector3.Normalize(MissedPos.position - StartPos.position);
            //string end = dirToEnd.ToString();

            if (dirToMiss.y > 0)
            {
                Debug.Log("Missed!");
                currentLvl.ChangeScoreBy(500);
                Destroy(notes[i]);
                notes.RemoveAt(i);
                i--;
            }
        }

        // Check collision for each note
    }

    public void CreateNote(int prefabIndex)
    {
        if (noteCooldown > 0) return;
        notes.Add(Instantiate(NotePrefabs[prefabIndex], StartPos.position, transform.rotation, transform));

        Vector3 direction = Vector3.Normalize(EndPos.position - StartPos.position);

        notes[notes.Count - 1].GetComponent<Note>().CreateNote(speed, direction);
        noteCooldown = GlobalVar.Instance.noteCoolDown;
    }

    public void CheckNoteCollision()
    {
        for (int i = 0; i < notes.Count; i++)
        {
            float distance = Vector3.Distance(notes[i].transform.position, EndPos.position);

            LevelManager currentLvl = GameManager.Instance.currentLevel;

            int score =
                (distance <= 0.05) ? 1000 :
                (distance < .15) ? 500 :
                (distance < .35) ? 100 :
                (distance < .5) ? 50 :
                (distance < .75) ? 10 :
                (distance < 1) ? 5 :
                0;

            currentLvl.ChangeScoreBy(score);
        }
    }
}
