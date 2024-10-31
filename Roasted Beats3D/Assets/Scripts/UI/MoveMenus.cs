using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMenus : MonoBehaviour
{
    int moved = 0;

    public void MoveRight()
    {
        if (moved < 2)
        {
            Vector3 currentPos = transform.position;
            currentPos.x -= 19.25f;
            transform.position = currentPos;
            moved++;
        }
    }

    public void MoveLeft()
    {
        if (moved > 0)
        {
            Vector3 currentPos = transform.position;
            currentPos.x += 19.25f;
            transform.position = currentPos;
            moved--;
        }
    }
}
