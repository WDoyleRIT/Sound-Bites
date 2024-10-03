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
    [SerializeField] private Vector3 direction;

    public void CreateNote(float speed, Vector3 dir)
    {
        noteSpeed = speed;
        direction = dir;
    }

    // Update is called once per fixed frame
    public void OnUpdate()
    {
        // Lower the note from its starting point
        transform.position = transform.position + (direction * noteSpeed * Time.deltaTime);
    }


}
