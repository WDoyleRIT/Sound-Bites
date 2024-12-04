using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlText : MonoBehaviour
{
    // References to text objects
    [SerializeField] private TextMeshProUGUI input1;
    [SerializeField] private TextMeshProUGUI input2;
    [SerializeField] private TextMeshProUGUI input3;
    [SerializeField] private TextMeshProUGUI input4;

    // Start is called before the first frame update
    void Start()
    {
        // Empty for now
    }

    // Update is called once per frame
    void Update()
    {
        // Changes input indicates based on control scheme
        if (GlobalVar.Instance.isControllerConnected)
        {
            input1.text = "LT";
            input1.fontSize = 60;
            input2.text = "LB";
            input2.fontSize = 60;
            input3.text = "RB";
            input3.fontSize = 60;
            input4.text = "RT";
            input4.fontSize = 60;
        }
        else
        {
            input1.fontSize = 72;
            input2.fontSize = 72;
            input3.fontSize = 72;
            input4.fontSize = 72;
            if(GlobalVar.Instance.controlScheme == 0)
            {
                input1.text = "D";
                input2.text = "F";
                input3.text = "J";
                input4.text = "K";
            }
            else if(GlobalVar.Instance.controlScheme == 1)
            {
                input1.text = "A";
                input2.text = "S";
                input3.text = "D";
                input4.text = "F";
            }
        }
    }
}
