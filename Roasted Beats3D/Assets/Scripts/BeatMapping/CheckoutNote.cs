using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class CheckoutNote : MonoBehaviour
{
    [SerializeField] private float xScaleSpeed;
    [SerializeField] private float yScaleSpeed;
    private Vector3 scale;


    public void CreateNote(float _xScaleSpeed,float _yScaleSpeed, Vector3 _scale)
    {
        xScaleSpeed = _xScaleSpeed;
        yScaleSpeed = _yScaleSpeed;
        scale = _scale;
        transform.localScale = scale;
    }

    public void OnUpdate()
    {
        scale=transform.localScale;
        scale.x += xScaleSpeed * Time.deltaTime;
        scale.y += yScaleSpeed * Time.deltaTime;
        transform.localScale = scale;
    }



}
