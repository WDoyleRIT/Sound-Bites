using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Will hold globally accessible variables
/// </summary>
public class GlobalVar : Singleton<GlobalVar>
{
    public float noteCoolDown = .45f;
    public int songDifficulty;
    public int streak = 0;
    public float noteSpdInSec = 5f;
    public float noteSpdInSec2 = 2f;
    public int notesPassed = 0;
    public bool songIsPlaying = false;

    public int checkoutNotesPassed = 0;

    public int currentLvlPoints = 0;

    public bool Ordering = false;

    public float lifePercent = 100;

    // Used to determine which control scheme to use
    public int controlScheme = 0;

    public float sumAccuracy = 0f;

    public int customersServed = 0;

    //public int highScore = 0;

    public int[] scores= new int[5];

    // Volume Variables
    public float masterVol = 1.0f;
    public float musicVol = 1.0f;
    public float SFXVol = 1.0f;

    private void Update()
    {
        lifePercent = GlobalVar.Instance.lifePercent;
        if (lifePercent > 100)
        {
            GlobalVar.Instance.lifePercent = 100;
        }
        if (lifePercent <= 0)
        {
            SceneManaging.Instance.OpenLvl("LossScene");
        }
        if (customersServed >= 2)
        {
            customersServed = 0;
            SceneManaging.Instance.OpenLvl("WinScene");
        }


    }


    public void ResetLevel()
    {
        currentLvlPoints = 0;

    }

}
