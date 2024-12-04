using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(RotateMenu))]
public class Menu : MonoBehaviour
{
    [SerializeField] private RotateMenu rotateMenu;

    [SerializeField] private Transform pivot;

    [SerializeField] private int rotationAngle;

    [SerializeField] private TextMeshProUGUI controlText;

    [SerializeField] private TextMeshProUGUI settingsText;

    private bool isSettings;
    // Game objects to hide in settings menu
    [SerializeField] private GameObject openLvl;
    [SerializeField] private GameObject moveRight;
    [SerializeField] private GameObject moveLeft;
    [SerializeField] private GameObject cafeMenu;
    [SerializeField] private GameObject fancyMenu;
    [SerializeField] private GameObject fastFoodMenu;
    // Game object for Settings HUD
    [SerializeField] private GameObject settingsHUD;

    private void Start()
    {
        SetupMenuItems();
        isSettings = false;
    }

    public void Update()
    {
       // Still nothing here
    }

    private void SetupMenuItems()
    {
        rotationAngle = (360 / rotateMenu.menuItems.Count);

        rotateMenu.SetValues(pivot, rotationAngle);

        for (int i = 0; i < rotateMenu.menuItems.Count; i++)
        {
            rotateMenu.menuItems[i].transform.RotateAround(pivot.position, Vector3.up, rotationAngle * i);   
        }
    }

    public void ChangeControls()
    {
        if(GlobalVar.Instance.controlScheme == 1)
        {
            GlobalVar.Instance.controlScheme = 0;
            controlText.text = "Controls: D/F/J/K";
        }
        else if(GlobalVar.Instance.controlScheme == 0)
        {
            GlobalVar.Instance.controlScheme = 1;
            controlText.text = "Controls: A/S/D/F";
        }
    }

    public void MasterVolume(float value)
    {
        GlobalVar.Instance.masterVol = value;
        Debug.Log(GlobalVar.Instance.masterVol);
    }

    public void MusicVolume(float value)
    {
        GlobalVar.Instance.musicVol = value;
        Debug.Log(GlobalVar.Instance.musicVol);
    }

    public void SFXVolume(float value)
    {
        GlobalVar.Instance.SFXVol = value;
        Debug.Log(GlobalVar.Instance.SFXVol);
    }

    public void ChangeWindow()
    {
        // Switches between main menu elements and settings elements
        isSettings = !isSettings;
        if (isSettings)
        {
            // Hide all main menu elements
            openLvl.SetActive(false);
            moveRight.SetActive(false);
            moveLeft.SetActive(false);
            cafeMenu.SetActive(false);
            fancyMenu.SetActive(false);
            fastFoodMenu.SetActive(false);
            // Enable settings HUD
            settingsHUD.SetActive(true);
            settingsText.text = "Back";
            Debug.Log("Settings Menu Opened");
        }
        else
        {
            // Show all main menu elements
            openLvl.SetActive(true);
            moveRight.SetActive(true);
            moveLeft.SetActive(true);
            cafeMenu.SetActive(true);
            fancyMenu.SetActive(true);
            fastFoodMenu.SetActive(true);
            // Disable settings HUD
            settingsHUD.SetActive(false);
            settingsText.text = "Settings";
            Debug.Log("Settings Menu Closed");
        }
    }
}
