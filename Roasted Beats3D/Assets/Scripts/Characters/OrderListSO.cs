using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

[CreateAssetMenu]
public class OrderListSO : ScriptableObject
{
    public List<FoodItem> items;
}

[Serializable]
public class FoodItem
{
    public string name;

    [Header("Food Items")]
    public GameObject Raw;
    public GameObject Medium;
    public GameObject Cooked;
    public GameObject Burned;
}
