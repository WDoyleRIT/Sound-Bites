using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckoutButton : MonoBehaviour
{

    [SerializeField] private List<GameObject> NotePrefabs;

    [SerializeField] private Transform buttonPos;

    [SerializeField] private Transform MissedScale;
    [SerializeField] private float xMaxScale;
    [SerializeField] private float yMaxScale;


    private float xScaleSpeed;
    private float yScaleSpeed;

    List<GameObject> notes=new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        notes = new List<GameObject>();

        xScaleSpeed = (0.5f - 0.1f) / 1000;
        yScaleSpeed = (0.6f - 0.12f) / 1000;

        CreateNote(0);

        xMaxScale = 0.5f;
        yMaxScale = 0.6f;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void CreateNote(int prefabIndex)
    {

        Debug.Log("Creating note");
        notes.Add(Instantiate(NotePrefabs[prefabIndex], new Vector3(buttonPos.position.x,buttonPos.position.y,-1.1f), Quaternion.Euler(0,0,0), transform));

        notes[notes.Count - 1].GetComponent<CheckoutNote>().CreateNote(xScaleSpeed,yScaleSpeed,new Vector3(0.1f,0.12f,1f)); 
    }

    public void OnUpdate()
    {

        for (int i = 0; i < notes.Count; i++)
        {
            notes[i].GetComponent<CheckoutNote>().OnUpdate();


            if (notes[i].transform.localScale.x > xMaxScale && notes[i].transform.localScale.y>yMaxScale)
            {
                Destroy(notes[i]);
                notes.RemoveAt(i);
                i--;
                
            }
        }

    }
}
