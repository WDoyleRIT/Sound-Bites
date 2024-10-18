using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RotateMenu))]
public class Menu : MonoBehaviour
{
    [SerializeField] private RotateMenu rotateMenu;

    [SerializeField] private Transform pivot;

    [SerializeField] private int rotationAngle;

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

}
