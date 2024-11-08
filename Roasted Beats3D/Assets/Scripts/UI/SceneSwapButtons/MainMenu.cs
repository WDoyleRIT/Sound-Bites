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
        continueB.onClick.AddListener(() => GameSave.Instance.Continue());
        continueB.onClick.AddListener(() => continueB.GetComponent<SquishEffect>().OnClick());

        newGameB.onClick.RemoveAllListeners();
        newGameB.onClick.AddListener(() => SceneManaging.Instance.OpenLvl("RestaurantSelect"));
        newGameB.onClick.AddListener(() => TutorialSaveInfo.Instance.SaveBasicData());
        newGameB.onClick.AddListener(() => newGameB.GetComponent<SquishEffect>().OnClick());

        quitB.onClick.RemoveAllListeners();
        quitB.onClick.AddListener(() => Application.Quit());
        quitB.onClick.AddListener(() => quitB.GetComponent<SquishEffect>().OnClick());
    }
}
