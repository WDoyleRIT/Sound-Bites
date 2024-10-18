using System.Collections;
using System.Collections.Generic;
using UnityEditor;
//using System.Numerics;
using UnityEngine;

public class RotateMenu : MonoBehaviour
{
    [SerializeField] public List<GameObject> menuItems;

    private bool isRotating;
    public float currentRotation;
    private int rotationAmount;
    private float rotationSpeed = 10;

    public Vector3 pivot;

    private void Update()
    {
        for (int i = 0; i < menuItems.Count; i++)
        {
            menuItems[i].transform.rotation = Quaternion.LookRotation(menuItems[i].transform.position - Camera.main.transform.position, Vector3.up);
        }
    }

    public void SetValues(Transform pivot, int rotateAmount)
    {
        this.pivot = pivot.position;
        this.rotationAmount = rotateAmount;
    }

    private IEnumerator Rotate(int direction, int degrees)
    {
        for (int i = 0; i < degrees / menuItems.Count / 2; i++)
        {
            for (int j = 0; j < menuItems.Count; j++)
            {
                menuItems[j].transform.RotateAround(pivot, Vector3.up, menuItems.Count * direction * 2);
            }
            
            yield return new WaitForFixedUpdate();
        }
    }

    public void RotateThang(int direction)
    {
        StartCoroutine(Rotate(direction, rotationAmount));
    }
}
