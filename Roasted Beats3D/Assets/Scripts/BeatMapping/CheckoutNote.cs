using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class CheckoutNote : MonoBehaviour
{
    [SerializeField] private float xScaleSpeed;
    [SerializeField] private float yScaleSpeed;
    private Vector3 scale;

    Color noteColor;

    public void CreateNote(float _xScaleSpeed,float _yScaleSpeed, Vector3 _scale, float _opacity)
    {
        xScaleSpeed = _xScaleSpeed;
        yScaleSpeed = _yScaleSpeed;
        scale = _scale;
        transform.localScale = scale;
        noteColor = Color.white;
        noteColor.a = _opacity;
        gameObject.GetComponent<SpriteRenderer>().color = noteColor;
        //noteColor = gameObject.GetComponent<SpriteRenderer>().color;
        //noteColor.a = _opacity;
    }

    public void OnUpdate()
    {
        scale=transform.localScale;
        scale.x -= xScaleSpeed * Time.deltaTime;
        scale.y -= yScaleSpeed * Time.deltaTime;
        transform.localScale = scale;
        noteColor.a += (0.99f / GlobalVar.Instance.noteSpdInSec) * Time.deltaTime;
        gameObject.GetComponent<SpriteRenderer>().color = noteColor;
        //noteColor.a += (0.7f / GlobalVar.Instance.noteSpdInSec) * Time.deltaTime;
    }



}
