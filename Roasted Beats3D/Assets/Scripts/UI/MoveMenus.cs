using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveMenus : MonoBehaviour
{
    public int moved = 0;

    private Spring xSpring;

    [SerializeField] float dampingRatio;
    [SerializeField] float angularFrequency;
    [SerializeField] float offsetDistance;

    [SerializeField] Button openLvlButton;
    [SerializeField] GameObject settingsHUD;

    private void Start()
    {
        xSpring = new Spring(angularFrequency, dampingRatio, transform.position.x / offsetDistance, true);

        //canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        xSpring.Update();

        transform.position = new Vector3(xSpring.Position * offsetDistance, transform.position.y, transform.position.z);

        // Was causing issues with settings menu, commenting out for now -Will D
        if (moved == 0 && !settingsHUD.activeSelf)
        {
            openLvlButton.gameObject.SetActive(true);
        }
        else
        {
            openLvlButton.gameObject.SetActive(false);
        }
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
