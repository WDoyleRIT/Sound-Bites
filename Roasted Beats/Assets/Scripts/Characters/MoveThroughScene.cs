using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveIntoScene : MonoBehaviour
{

    float width;
    float height;
    float speed = 0;

    Vector3 velocity = Vector3.zero;
    Vector3 direction = Vector3.right + Vector3.up;
    Vector3 charPosition= Vector3.zero;
    Vector3 vertical = Vector3.up;

    bool exit = false;

    int notesPassed = 0;
    GlobalVar gv;


    public void HitNote(InputAction.CallbackContext context)
    {
        if(context.started && charPosition.x >= 0)
        {
            exit = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Camera mainCam = FindObjectOfType<Camera>();
        height= 2f * mainCam.orthographicSize;
        width = height * mainCam.aspect;
        charPosition.x = -(width / 2);
        charPosition.y = 4;
        transform.position= charPosition;
        speed = 2;
        gv = GlobalVar.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        notesPassed = gv.notesPassed;
        velocity = direction * speed * Time.deltaTime;
        charPosition += velocity;
        transform.position = charPosition;

        // Joe's notes
        // I suggest using an animator for character movement up and down
        // while just moving x with code

        if (charPosition.x >= 0 && notesPassed<10)
        {
            speed = 0;
        }
        else
        {
            speed = 2;
        }
        if(charPosition.y >= 4.5)
        {
            vertical = Vector3.down;
            direction = Vector3.right + vertical;
        }
        else if (charPosition.y <= 3.5)
        {
            vertical = Vector3.up;
            direction = Vector3.right + vertical;
        }
    }
}
