using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class CheckoutNote : MonoBehaviour
{
    [SerializeField] private float scaleSpeed;
    private Vector3 scale;

    public Quaternion rotation;

    public void CreateNote(float _scaleSpeed, Vector3 _scale)
    {
        scaleSpeed = _scaleSpeed;
        scale = _scale;
        rotation = transform.rotation;
        transform.localScale = scale;
    }

    public void OnUpdate()
    {
        transform.rotation = rotation;
        scale=transform.localScale;
        scale.x += scaleSpeed;
        scale.y += scaleSpeed;
        transform.localScale = scale;
    }


}
