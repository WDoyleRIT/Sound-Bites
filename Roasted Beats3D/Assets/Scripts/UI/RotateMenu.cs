using System.Collections;
using System.Collections.Generic;
using UnityEditor;
//using System.Numerics;
using UnityEngine;

public class RotateMenu : MonoBehaviour
{
    public void RotateThang(float direction)
    {
        Vector3 pivot = new Vector3(0.0f, 0.0f, 15.0f);

        transform.RotateAround(pivot, Vector3.up, 45.0f * direction);
    }
}
