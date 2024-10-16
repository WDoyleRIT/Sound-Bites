using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckoutManager : MonoBehaviour
{

    [SerializeField] private List<GameObject> NotePrefabs;

    [SerializeField] private Transform buttonPos;

    [SerializeField] private Transform MissedScale;


    private float scaleSpeed;

    List<GameObject> notes=new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        notes = new List<GameObject>();

        scaleSpeed = (0.5f - 0.1f) / 100;

        CreateNote(0);

    }

    // Update is called once per frame
    void Update()
    {
        scaleSpeed = 1f;

        for(int i = 0; i < notes.Count; i++)
        {
            notes[i].GetComponent<CheckoutNote>().OnUpdate();
        }
    }


    public void CreateNote(int prefabIndex)
    {

        Debug.Log("Creating note");
        notes.Add(Instantiate(NotePrefabs[prefabIndex], new Vector3(buttonPos.position.x,buttonPos.position.y,-1.1f), transform.rotation, transform));

        notes[notes.Count - 1].GetComponent<CheckoutNote>().CreateNote(scaleSpeed,new Vector3(0.1f,0.12f,1f));
        notes[notes.Count - 1].GetComponent<CheckoutNote>().rotation.x = -90; 
    }
}
