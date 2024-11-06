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

    //CanvasGroup canvasGroup;

    private void Start()
    {
        xSpring = new Spring(angularFrequency, dampingRatio, transform.position.x / offsetDistance, true);

        //canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        xSpring.Update();

        transform.position = new Vector3(xSpring.Position * offsetDistance, transform.position.y, transform.position.z);

        /*
        if (gameObject.name == "ManageButtons")
        {
            if (moved != 0)
            {
                canvasGroup.alpha = 0;
            }
            else
            {
                canvasGroup.alpha = 1;
            }
        }
        */
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
