using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBar : MonoBehaviour
{
    [SerializeField] private List<GameObject> NotePrefabs;

    [SerializeField] private Transform StartPos;
    [SerializeField] private Transform EndPos;

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
        // We calculate speed each frame just because its easier if we need to change it in global
        if (travelDistance == 0) return;

        speed = GlobalVar.Instance.noteSpdInSec / travelDistance;
    }

    public void OnUpdate()
    {
        foreach (var item in notes)
        {
            // notes.Update();
        }

        // Check collision for each note
    }
}
