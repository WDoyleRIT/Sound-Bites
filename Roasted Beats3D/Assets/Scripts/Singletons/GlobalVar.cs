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
    public int notesPassed = 0;
    public bool songIsPlaying = false;

    public int checkoutNotesPassed = 0;

    public int currentLvlPoints = 0;
}
