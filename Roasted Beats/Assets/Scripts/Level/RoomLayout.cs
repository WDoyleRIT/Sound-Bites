using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class RoomLayout : MonoBehaviour
{
    [SerializeField] private List<GameObject> roomLayers;

    [SerializeField] private float layerOffset;
    [SerializeField] private float roomOffsetMax;

    private Vector3 cameraOffset;

    private List<GameObject> instantiatedLayers;

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
        for (int i = 0; i < instantiatedLayers.Count; i++)
        {
            GameObject layer = instantiatedLayers[i];
            // Applies camera mouse offset
            layer.transform.position = (Vector3)((Vector2)transform.position + new Vector2(cameraOffset.x * roomOffsetMax * i, cameraOffset.y * roomOffsetMax * i)) + new Vector3(0, 0, layer.transform.position.z);
        }
    }

    public void SetCameraOffset(Vector3 offset)
    {
        cameraOffset = offset;
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

        for (int i = 0; i < roomLayers.Count; i++)
        {
            instantiatedLayers.Add(Instantiate(roomLayers[i], transform.position + new Vector3(0, 0, layerOffset * i), Quaternion.identity, transform));

            float color = (1 - i * .075f);

            GameObject layer = instantiatedLayers[instantiatedLayers.Count - 1];

            layer.GetComponent<SpriteRenderer>().color = new Color(color,color,color,1);
            layer.transform.localScale = Vector3.one - (Vector3.one * .025f * i);
        }
    }
}
