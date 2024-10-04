using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows.Speech;

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
    bool noteCountReset = false;

    int notesPassed = 0;
    GlobalVar gv;
    [SerializeField] TextMeshPro textPrefab;



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
        charPosition.y = 0.5f;
        transform.position= charPosition;
        speed = 2;
        gv = GlobalVar.Instance;
        textPrefab = Instantiate(textPrefab, textPrefab.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        notesPassed = gv.notesPassed;
        //Debug.Log(notesPassed);
        velocity = direction * speed * Time.deltaTime;
        charPosition += velocity;
        transform.position = charPosition;



        // Joe's notes
        // I suggest using an animator for character movement up and down
        // while just moving x with code

        if (charPosition.x >= -5.5 && !exit)
        {
            textPrefab.text = "Make me some food please! (Have " + (30 - notesPassed) + " notes pass)";
            if (!noteCountReset)
            {
                gv.notesPassed = 0;
                notesPassed = 0;
                noteCountReset = true;
                //Debug.Log("resetting");
            }
            if (notesPassed < 30 && noteCountReset)
            {
                speed = 0;
            }
            else
            {
                exit = true;
                speed= 2;
                noteCountReset= false;
                //Debug.Log("exiting");
            }
        }
        else
        {
            textPrefab.text = "";
            speed = 2;
        }
        if(charPosition.y >= 0)
        {
            vertical = Vector3.down;
            if (!exit)
            {
                direction = Vector3.right + vertical;
            }
            else
            {
                direction = Vector3.left + vertical;
            }
        }
        else if (charPosition.y <= -1)
        {
            vertical = Vector3.up;
            if (!exit)
            {
                direction = Vector3.right + vertical;
            }
            else
            {
                direction = Vector3.left + vertical;
            }
        }

        if (charPosition.x < -(width/2)-2.5)
        {
            exit = false;
        }
    }
}
