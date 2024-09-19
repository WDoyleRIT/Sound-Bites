using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempSetBeat : MonoBehaviour
{

    [SerializeField] private List<GameObject> beatObjs;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBeatActive(List<bool> bools)
    {
        for (int i = 0; i < beatObjs.Count; i++)
        {
            beatObjs[i].SetActive(bools[i]);
        }
    }
}
