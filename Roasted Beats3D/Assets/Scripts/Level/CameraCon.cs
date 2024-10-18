using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CameraCon : MonoBehaviour
{
    public UnityEvent<Vector3> CameraEvent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = GetMouseScreenPos();
        offset.z = 0;

        // Clamps and inverts values
        offset.x = -Mathf.Clamp(offset.x, -.5f, .5f);
        offset.y = -Mathf.Clamp(offset.y, -.5f, .5f);

        // Sends offset off to the room layout
        CameraEvent.Invoke(offset);
    }

    private Vector3 GetMouseScreenPos()
    {
        Vector2 offset = Input.mousePosition;

        // Returns a value between -.5f and .5f for x and y based on where mouse is 

        offset = new Vector2 ((offset.x - Screen.width / 2) / Screen.width, (offset.y - Screen.height / 2) / Screen.height);

        //Debug.Log(offset);
        return offset;
    }
}
