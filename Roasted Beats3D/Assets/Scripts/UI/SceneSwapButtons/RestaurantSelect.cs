using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RestaurantSelect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buttonText;

    [SerializeField] private Button levelButton;

    private string[] levelNames = new string[]
    {
        "Cafe_Orders",
        "FastFood_Orders",
        "Fancy_Orders"
    };
    private int currentLevel = 0;

    public void ChangeLevel(int value)
    {
        currentLevel += value;
        currentLevel %= levelNames.Length;
    }

    // Start is called before the first frame update
    void Start()
    { 
        levelButton.onClick.RemoveAllListeners();
        levelButton.onClick.AddListener(() => SceneManaging.Instance.OpenLvl(levelNames[currentLevel]));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
