using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Note : MonoBehaviour
{
    // NOTE: Programming the movement of the note based on a timer
    // instead of using Update() might be smart in the long run
    // Maybe use deltatime or fixedUpdate or both!

    // Float to determine how fast the note moves
    [SerializeField] private float noteSpeed;

    public void CreateNote(float speed)
    {
        noteSpeed = speed;
    }

    // Update is called once per fixed frame
    public void OnUpdate()
    {
        // Lower the note from its starting point
        transform.position = new Vector3(transform.position.x, transform.position.y - (noteSpeed * Time.deltaTime), transform.position.z);
    }


}
