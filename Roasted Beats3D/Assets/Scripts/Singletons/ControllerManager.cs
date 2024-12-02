using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : Singleton<ControllerManager>
{
    
    // Start is called before the first frame update
    void Start()
    {
        GlobalVar.Instance.controllerStatus = Input.GetJoystickNames().Length;
        Debug.Log(GlobalVar.Instance.controllerStatus);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
