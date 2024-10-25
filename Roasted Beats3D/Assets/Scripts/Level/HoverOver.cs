using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverOver : MonoBehaviour
{
    // Temp HoverOver code
    // ========================================================================

    [SerializeField] private GameObject hoverMask;
    [SerializeField] private GameObject popUp;
    [SerializeField] private GenerateCheckout checkout;

    public void OnHover(bool hoverStatus)
    {
        hoverMask.SetActive(hoverStatus);
    }

    private void Update()
    {
        if (GlobalVar.Instance.checkoutNotesPassed > 15)
        {
            OnClickFalse();
            GlobalVar.Instance.Ordering = false;
            GlobalVar.Instance.checkoutNotesPassed = 0; 
            checkout.DestroyNotes();
            //checkout.DeleteButtons();
            SceneManaging.Instance.OpenLvl("Cafe_Cooking");

        }
    }

    public void OnClickFalse()
    {
        popUp.SetActive(false);
    }

    public void OnClickTrue()
    {
        if (GlobalVar.Instance.Ordering)
        {
            popUp.SetActive(true);
            checkout.GenerateButtons();
        }
        RhythmManager.Instance.ChangeVolume(.5f);
    }

    // ========================================================================
}
