using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMenus : MonoBehaviour
{
    int moved = 0;

    private Spring xSpring;

    [SerializeField] float dampingRatio;
    [SerializeField] float angularFrequency;
    [SerializeField] float offsetDistance;

    private void Start()
    {
        xSpring = new Spring(angularFrequency, dampingRatio, transform.position.x / offsetDistance, true);
    }

    private void Update()
    {
        xSpring.Update();

        transform.position = new Vector3(xSpring.Position * offsetDistance, transform.position.y, transform.position.z);
    }

    public void MoveRight()
    {
        if (moved < 2)
        {
            //Vector3 currentPos = transform.position;
            //currentPos.x -= 19.25f;
            //transform.position = currentPos;

            xSpring.RestPosition--;

            moved++;
        }
    }

    public void MoveLeft()
    {
        if (moved > 0)
        {
            //Vector3 currentPos = transform.position;
            //currentPos.x += 19.25f;
            //transform.position = currentPos;

            xSpring.RestPosition++;

            moved--;
        }
    }
}
