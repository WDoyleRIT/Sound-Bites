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

    private float noteCooldown;
    private bool noteOnButton;


    private float xScaleSpeed;
    private float yScaleSpeed;

    List<GameObject> notes = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        notes = new List<GameObject>();

        xScaleSpeed = (0.5f - 0.1f) / GlobalVar.Instance.noteSpdInSec;
        yScaleSpeed = (0.6f - 0.12f) / GlobalVar.Instance.noteSpdInSec;


        xMaxScale = 0.5f;
        yMaxScale = 0.6f;
    }

    // Update is called once per frame
    void Update()
    {
        noteCooldown -= Time.deltaTime;
    }

    public void DestroyNotes()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        notes = new List<GameObject>();
    }

    public void CreateNote(int prefabIndex)
    {
        xScaleSpeed = (0.5f - 0.1f) / GlobalVar.Instance.noteSpdInSec;
        yScaleSpeed = (0.6f - 0.12f) / GlobalVar.Instance.noteSpdInSec;


        xMaxScale = 0.5f;
        yMaxScale = 0.6f;

        if (noteCooldown > 0 || noteOnButton) return;

        noteOnButton = true;
        //Debug.Log("Creating note");
        notes.Add(Instantiate(NotePrefabs[prefabIndex], new Vector3(buttonPos.position.x,buttonPos.position.y,buttonPos.position.z - buttonPos.localScale.y - .1f), Quaternion.Euler(0,0,0), transform));

       

        notes[notes.Count - 1].GetComponent<CheckoutNote>().CreateNote(xScaleSpeed,yScaleSpeed,new Vector3(0.1f,0.12f,1f));
        noteCooldown = GlobalVar.Instance.noteCoolDown;
    }

    public void OnUpdate()
    {

        for (int i = 0; i < notes.Count; i++)
        {
            //RhythmManager currentLvl = GameManager.Instance.CurrentLevel;

            notes[i].GetComponent<CheckoutNote>().OnUpdate();


            if (notes[i].transform.localScale.x > xMaxScale && notes[i].transform.localScale.y>yMaxScale)
            {
                Destroy(notes[i]);
                notes.RemoveAt(i);
                i--;
                noteOnButton = false;
                GlobalVar.Instance.checkoutNotesPassed++;
                GlobalVar.Instance.lifePercent -= 5;
            }
        }

    }


    public void OnClick()
    {
        if (notes.Count <= 0) return;
        Destroy(notes[0]);
        notes.RemoveAt(0);
        noteOnButton = false;
        GlobalVar.Instance.checkoutNotesPassed++;

        GlobalVar.Instance.lifePercent += 2;
    }
}
