using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverOver : MonoBehaviour
{
    // Temp HoverOver code
    // ========================================================================

    [SerializeField] private GameObject hoverMask;
    [SerializeField] private GameObject popUp;

    public void OnHover(bool hoverStatus)
    {
        hoverMask.SetActive(hoverStatus);
    }

    public void OnClickFalse()
    {
        popUp.SetActive(false);
    }

    public void OnClickTrue()
    {
        popUp.SetActive(true);
    }

    // ========================================================================
}
