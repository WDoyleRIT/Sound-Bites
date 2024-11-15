using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GenerateCheckout : MonoBehaviour
{

    [SerializeField] private GameObject buttonPrefab;

    private List<GameObject> buttons;
    private List<float> noteCooldowns;

    [SerializeField] private Guide guide;

    [SerializeField] private TextMeshPro RatingText;

    public void CheckTutorial()
    {
        if (!TutorialSaveInfo.Instance.GetDictValue("Checkout"))
        {
            StartCoroutine(guide.SetActive(true));
            guide.SetDialogueList(1);
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        RhythmManager.Instance.ratingText = RatingText;
        RhythmManager.Instance.sm.frequencyAveraging.OnSpawnBeat.RemoveAllListeners();
        RhythmManager.Instance.sm.frequencyAveraging.OnSpawnBeat.AddListener(OnBeatUpdate);

        //GenerateButtons();

        noteCooldowns = new List<float>();

        for (int i = 0; i < 4; i++)
        {
            noteCooldowns.Add(0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i=0;i<buttons.Count;i++)
        {
            buttons[i].GetComponent<CheckoutButton>().OnUpdate();

        }
        for(int j = 0; j < noteCooldowns.Count; j++)
        {
            noteCooldowns[j] -= Time.deltaTime;
        }

 
    }

    public void DestroyNotes()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].GetComponent<CheckoutButton>().DestroyNotes();
        }
    }

    public void GenerateButtons()
    {
        buttons= new List<GameObject>();

        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                buttons.Add(Instantiate(buttonPrefab,new Vector3(-3f+2f*i,-1.5f+1.5f*j,transform.position.z),Quaternion.Euler(90,0,0), transform));
            }
        }
    }


    public void DeleteButtons()
    {
        for(int i=0;i<buttons.Count;i++)
        {
            Destroy(buttons[i]);
        }
        buttons = new List<GameObject> ();
    }



    public void OnBeatUpdate(List<bool> notes)
    {
        for(int i=0;i<4; i++)
        {
            if (notes[i] && noteCooldowns[i]<=0)
            {
                noteCooldowns[i] = GlobalVar.Instance.noteCoolDown;
                int randButton = (int)(Random.value * 3); 
                randButton=i*3+randButton;
                buttons[randButton].GetComponent<CheckoutButton>().CreateNote(i);
            }
        }
    }
}
