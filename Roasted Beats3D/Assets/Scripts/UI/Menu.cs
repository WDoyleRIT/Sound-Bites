using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(RotateMenu))]
public class Menu : MonoBehaviour
{
    [SerializeField] private RotateMenu rotateMenu;

    [SerializeField] private Transform pivot;

    [SerializeField] private int rotationAngle;

    [SerializeField] private TextMeshProUGUI controlText;

    private void Start()
    {
        SetupMenuItems();
    }

    private void SetupMenuItems()
    {
        rotationAngle = (360 / rotateMenu.menuItems.Count);

        rotateMenu.SetValues(pivot, rotationAngle);

        for (int i = 0; i < rotateMenu.menuItems.Count; i++)
        {
            rotateMenu.menuItems[i].transform.RotateAround(pivot.position, Vector3.up, rotationAngle * i);   
        }
    }

    public void ChangeControls()
    {
        if(GlobalVar.Instance.controlScheme == 1)
        {
            GlobalVar.Instance.controlScheme = 0;
            controlText.text = "Controls: D/F/J/K";
        }
        else if(GlobalVar.Instance.controlScheme == 0)
        {
            GlobalVar.Instance.controlScheme = 1;
            controlText.text = "Controls: A/S/D/F";
        }
        
    }
}
