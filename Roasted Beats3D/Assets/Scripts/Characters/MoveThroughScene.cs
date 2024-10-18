using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows.Speech;

public class MoveIntoScene : MonoBehaviour
{
    [SerializeField] private Transform standPos;
    [SerializeField] private Transform endPos;

    public Transform StandPos { get { return standPos; } set { standPos = value; } }
    public Transform EndPos { get { return endPos; } set { endPos = value; } }

    public Vector3 currentTargetPos;

    public bool IsMoving { get; private set; }
    public bool IsReadyToOrder {  get; private set; }
    public bool WalkedOut { get; private set; }
    public bool OrderRecieved {  get; set; }

    private bool up;

    private float count = 0;

    private void Start()
    {
        IsMoving = false;
        IsReadyToOrder = false;
        WalkedOut = false;
        OrderRecieved = false;

        currentTargetPos = standPos.position;
    }

    public void SetTargetPosition(Vector3 position)
    {
        Debug.Log(string.Format("Set position to {0}", position));
        currentTargetPos = position;
    }

    private void Update()
    {
        float speed = 10;
        Vector3 direction = Vector3.Normalize(currentTargetPos - (transform.position + (Vector3.up * (IsMoving ? (up ? 1 : -1) : 0))));

        // Moves position towards current target position
        if (IsMoving)
            transform.position += direction * Time.deltaTime * speed;

        // Simple Clock ----------
        if (count < 0)
        {
            up = !up;
            count = .25f;
        }

        //Debug.Log(count);

        count -= Time.deltaTime;
        // -----------------------

        // If the position is close enough we set it to the target
        if (Vector3.Distance(currentTargetPos, transform.position) < 0.05f)
        {
            transform.position = currentTargetPos;
            IsMoving = false;
        }
        else
            IsMoving = true;

        if (transform.position == standPos.position)
            IsReadyToOrder = true;

        if (transform.position == endPos.position)
            WalkedOut = true;

        if (OrderRecieved)
        {
            currentTargetPos = endPos.position;
            IsMoving = true;
        }
            
    }

    /*
    public bool IsReadyToOrder {  get; private set; }

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
        IsReadyToOrder = false;

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
            IsReadyToOrder = true;

            textPrefab.text = "Make me some food please! (" + (30 - notesPassed) + " more notes to pass)";
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
    }*/
}
