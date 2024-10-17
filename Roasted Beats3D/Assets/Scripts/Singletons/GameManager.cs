using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private RhythmManager currentLevel;

    public RhythmManager CurrentLevel { get { return currentLevel; } set { currentLevel = value; } }
}
