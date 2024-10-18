using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Will hold globally accessible variables
/// </summary>
public class GlobalVar : Singleton<GlobalVar>
{
    public float noteCoolDown = .5f;
    public int songDifficulty;
    //public float noteSpeed = 0.1f;
    public float noteSpdInSec = 5f;
    public int notesPassed = 0;
    public bool songIsPlaying = false;

    public int checkoutNotesPassed = 0;

    public int currentLvlPoints = 0;
}
