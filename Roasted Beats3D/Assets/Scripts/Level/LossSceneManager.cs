using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LossSceneManager : MonoBehaviour
{

    public Button ReturnToSelect;

    // Start is called before the first frame update
    void Start()
    {


        ReturnToSelect.onClick.RemoveAllListeners();
        ReturnToSelect.onClick.AddListener(() => SceneManaging.Instance.OpenLvl("RestaurantSelect"));
        ReturnToSelect.onClick.AddListener(()=>ReturnToSelect.interactable = false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
