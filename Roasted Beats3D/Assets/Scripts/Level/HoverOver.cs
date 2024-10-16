using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverOver : MonoBehaviour
{
    // Temp HoverOver code
    // ========================================================================

    [SerializeField] private GameObject hoverMask;

    public void OnHover(bool hoverStatus)
    {
        hoverMask.SetActive(hoverStatus);
    }

    public void OnClick()
    {
        hoverMask.SetActive(false);
    }

    // ========================================================================
}
