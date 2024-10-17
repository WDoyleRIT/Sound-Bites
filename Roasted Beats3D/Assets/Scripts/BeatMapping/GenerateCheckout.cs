using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCheckout : MonoBehaviour
{

    [SerializeField] private GameObject buttonPrefab;

    private List<GameObject> buttons;



    // Start is called before the first frame update
    void Start()
    {
        GenerateButtons();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0;i<buttons.Count;i++)
        {
            buttons[i].GetComponent<CheckoutButton>().OnUpdate();
        }
    }



    private void GenerateButtons()
    {
        buttons= new List<GameObject>();

        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                buttons.Add(Instantiate(buttonPrefab,new Vector3(-1.5f+1.5f*i,-1.5f+1.5f*j,transform.position.z),Quaternion.Euler(90,0,0)));
            }
        }
    }
}
