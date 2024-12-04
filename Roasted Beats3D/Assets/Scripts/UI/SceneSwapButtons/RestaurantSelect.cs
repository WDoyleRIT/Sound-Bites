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

    [SerializeField] private MoveMenus moveMenus;

    [SerializeField] GameObject settingsHUD;

    [SerializeField] private TextMeshProUGUI controllerText;

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
        levelButton.onClick.AddListener(() => levelButton.GetComponent<SquishEffect>().OnClick());
    }

    // Update is called once per frame
    void Update()
    {
        // Was causing issues with settings menu, commenting out for now -Will D
        //if (moveMenus.moved != 0 && settingsHUD.activeSelf)
        //{
        //    levelButton.gameObject.SetActive(false);
        //}
        //else
        //{
        //    levelButton.gameObject.SetActive(true);
        //}

        if (!GlobalVar.Instance.isControllerConnected)
        {
            controllerText.text = "No Controller Connected";
        }
        else
        {
            controllerText.text = "Controller Connected";
        }
    }
}
