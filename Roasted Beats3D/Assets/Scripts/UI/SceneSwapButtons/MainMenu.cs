using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button continueB;
    public Button newGameB;
    public Button quitB;

    // Start is called before the first frame update
    void Start()
    {
        continueB.onClick.RemoveAllListeners();
        continueB.onClick.AddListener(() => SceneManaging.Instance.OpenLvl("RestaurantSelect"));
        newGameB.onClick.RemoveAllListeners();
        newGameB.onClick.AddListener(() => SceneManaging.Instance.OpenLvl("RestaurantSelect"));
        newGameB.onClick.AddListener(() => TutorialSaveInfo.Instance.SaveBasicData());
        quitB.onClick.RemoveAllListeners();
        quitB.onClick.AddListener(() => Application.Quit());
    }
}
