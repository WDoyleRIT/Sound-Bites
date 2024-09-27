using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    // NOTE: Programming the movement of the note based on a timer
    // instead of using Update() might be smart in the long run
    // Maybe use deltatime or fixedUpdate or both!

    // Float to determine how fast the note moves
    [SerializeField] private float noteSpeed;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize note speed
        this.noteSpeed = GlobalVar.Instance.noteSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Lower the note from its starting point
        transform.position = new Vector3(transform.position.x, transform.position.y - noteSpeed, transform.position.z);

        // If the note goes too low, destroy it
        /*if(transform.position.y <= -4)
        {
            Destroy(gameObject);
            Debug.Log("Miss");
        }*/
    }
}
