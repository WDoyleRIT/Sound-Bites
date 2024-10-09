using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RoomLayout : MonoBehaviour
{
    [SerializeField] private List<GameObject> roomLayers;

    [SerializeField] private float layerOffset;

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

            float color = (0 + i * .075f);

            instantiatedLayers[instantiatedLayers.Count - 1].GetComponent<SpriteRenderer>().color = new Color(color,color,color,1);
        }
    }
}
