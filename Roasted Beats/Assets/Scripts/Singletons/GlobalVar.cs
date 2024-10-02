using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Will hold globally accessible variables
/// </summary>
public class GlobalVar : Singleton<GlobalVar>
{
    public int songDifficulty;
    //public float noteSpeed = 0.1f;
    public float noteSpdInSec = 5;
    public int notesPassed = 0;
    public bool songIsPlaying = false;

    public uint currentLvlPoints = 0;
}
