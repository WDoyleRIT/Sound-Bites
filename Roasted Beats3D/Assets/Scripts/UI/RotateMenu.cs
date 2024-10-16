using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class RotateMenu : MonoBehaviour
{
    public void RotateRight()
    {
        Quaternion currentRotate = transform.rotation;
        Vector3 currentPosition = transform.position;

        float rotateAmount = 45.0f;
        float moveAmount = -6.0f;

        currentPosition.x += moveAmount;
        currentRotate.y += rotateAmount;

        transform.rotation = currentRotate;
        transform.position = currentPosition;
    }

    public void RotateLeft()
    {
        Quaternion currentRotate = transform.rotation;
        Vector3 currentPosition = transform.position;

        float rotateAmount = -45.0f;
        float moveAmount = 6.0f;

        currentPosition.x += moveAmount;
        currentRotate.y += rotateAmount;

        transform.rotation = currentRotate;
        transform.position = currentPosition;
    }
}
