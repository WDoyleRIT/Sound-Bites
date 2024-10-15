using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class RoomLayout : MonoBehaviour
{
    [SerializeField] private List<GameObject> roomLayers;
    [SerializeField] private GameObject shadeLayer;

    [SerializeField] private float layerOffset;
    [SerializeField] private float roomOffsetMax;

    [Header("Camera Variables")]
    [SerializeField] private float camLerpSpeed;

    private Vector3 cameraOffset;
    private Vector3 targetOffset;

    private List<GameObject> instantiatedLayers;
    private List<GameObject> shadeLayers;

    private void OnValidate()
    {
        if (!Application.isEditor) return;
        if (transform.childCount > 0) return;

        GenerateLayers();
    }

    private void Start()
    {
        GenerateLayers();
    }

    private void Update()
    {
        cameraOffset = Vector3.Lerp(targetOffset, cameraOffset, Mathf.Pow(.5f, Time.deltaTime * camLerpSpeed));

        for (int i = 0; i < instantiatedLayers.Count; i++)
        {
            GameObject layer = instantiatedLayers[i];
            // Applies camera mouse offset
            layer.transform.position = (Vector3)((Vector2)transform.position + new Vector2(cameraOffset.x * roomOffsetMax * i, cameraOffset.y * roomOffsetMax * i)) + new Vector3(0, 0, layer.transform.position.z);
        }
    }

    public void SetCameraOffset(Vector3 offset)
    {
        targetOffset = offset;
    }

    private void GenerateLayers()
    {
        if (transform.childCount > 0)
        {
            for (int i = transform.childCount - 1; i >= 0 ; i--)
            {
                if (Application.isEditor)
                {
                    Destroy(transform.GetChild(i).gameObject);
                } 
                else
                {
                    Destroy(transform.GetChild(i).gameObject);
                } 
                    
            }
        }
    
        instantiatedLayers = new List<GameObject>();
        shadeLayers = new List<GameObject>();

        for (int i = 0; i < roomLayers.Count; i++)
        {
            instantiatedLayers.Add(Instantiate(roomLayers[i], transform.position + new Vector3(0, 0, layerOffset * i), Quaternion.identity, transform));

            GameObject layer = instantiatedLayers[instantiatedLayers.Count - 1];

            //shadeLayers.Add(Instantiate(shadeLayer, transform.position + new Vector3(0, 0, layerOffset * i - .01f), Quaternion.identity, layer.transform));
            float color = (1 - i * .055f);

            

            //shadeLayers[shadeLayers.Count - 1].transform.localScale = Vector3.one * 2;

            //layer.GetComponent<SpriteRenderer>().color = new Color(color,color,color,1);
            layer.transform.localScale = Vector3.one - (Vector3.one * .025f * i);
        }
    }
}
